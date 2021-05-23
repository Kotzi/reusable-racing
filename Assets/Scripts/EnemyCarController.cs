using UnityEngine;

public class EnemyCarController: CarController
{
    const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5f;

    private int waypointTarget = 0;
    private Transform mainTarget;
    
    void Start()
    {
        this.waypointTarget = this.nextWaypoint;
    }
    
	void FixedUpdate() 
    {
        if (this.gameController.carsCanMove())
        {
            if (this.mainTarget != null) 
            {
                this.moveTo(this.mainTarget);
                this.lookAt(this.mainTarget, Time.deltaTime);
            } 
            else if (this.waypointTarget <= this.waypoints.Length - 1)
            {
                var waypoint = this.waypoints[this.waypointTarget];
                this.lookAt(waypoint.transform, Time.deltaTime);
                this.moveTo(waypoint.transform);
                
                //Debug.Log($"this.transform.position {this.transform.position} waypoint.position {waypoint.position}");

                if (waypoint.getBounds().Contains(this.transform.position))
                {
                    this.waypointTarget += 1;
                }
            }
            else 
            {
                this.waypointTarget = 0;
            }

            print(this.waypointTarget);
        }
        else
        {
            this.rb.velocity = Vector2.zero;
            this.rb.angularVelocity = 0f;
        }
	}

    public void updateMainTarget(Transform mainTarget)
    {
        this.mainTarget = mainTarget;

        if (this.mainTarget == null)
        {
            this.waypointTarget = this.nextWaypoint;
        }
    }

    private void moveTo(Transform target)
    {
        float speed = this.rb.velocity.magnitude;
        if (speed > MAX_SPEED)
        {
            float brakeSpeed = speed - MAX_SPEED;
        
            Vector3 normalisedVelocity = this.rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
        
            this.rb.AddForce(-brakeVelocity); 
        }
        else
        {
            this.rb.AddForce((target.position - transform.position).normalized * (10f + this.turbo));
        }
    }

    private void lookAt(Transform target, float time)
    {
        var diffVector = target.position - this.transform.position;
        var direction = this.transform.rotation * Vector2.up;
        var angleDiff = Vector2.SignedAngle(direction, diffVector);
        var clampedDiff = Mathf.Clamp(
            angleDiff,
            -100 * time,
            100 * time
        );

        this.transform.rotation = Quaternion.AngleAxis(
            this.transform.eulerAngles.z + clampedDiff,
            Vector3.forward
        );
    }
}

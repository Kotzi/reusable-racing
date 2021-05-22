using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5f;

    public Transform[] waypoints;

    private int waypointIndex = 0;
    private Rigidbody2D rb;
    private GameController gameController;
    private Transform mainTarget;

	void Awake() 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
	}
    
    void Start()
    {
        this.forceUpdateWaypointIndex();
    }
    
	void FixedUpdate() 
    {
        if (!this.gameController.isFighting)
        {
            if (this.mainTarget != null) 
            {
                Debug.Log("hay this.mainTarget");
                this.moveTo(this.mainTarget);
                this.lookAt(this.mainTarget, Time.deltaTime);
            } 
            else if (this.waypointIndex <= this.waypoints.Length - 1)
            {
                var waypoint = this.waypoints[this.waypointIndex];
                this.moveTo(waypoint);
                
                Debug.Log($"this.transform.position {this.transform.position} waypoint.position {waypoint.position}");
                this.lookAt(waypoint, Time.deltaTime);

                if (this.isNearWaypoint(waypoint))
                {
                    this.waypointIndex += 1;
                }
            }
            else 
            {
                this.waypointIndex = 0;
            }
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
            this.forceUpdateWaypointIndex();
        }
    }

    private void forceUpdateWaypointIndex()
    {
        var closestWaypoint = 0;
        float distanceToClosestWaypoint = Mathf.Abs(Vector2.Distance(this.transform.position, this.waypoints[closestWaypoint].position));
        for (int i = 0; i < this.waypoints.Length; i++)
        {
            var waypoint = this.waypoints[i];
            float distanceToWaypoint = Mathf.Abs(Vector2.Distance(this.transform.position, waypoint.position));
            if (distanceToWaypoint <= distanceToClosestWaypoint)
            {
                closestWaypoint = i;
                distanceToClosestWaypoint = distanceToWaypoint;
            }
        }

        this.waypointIndex = closestWaypoint + 1;
    }

    private bool isNearWaypoint(Transform waypoint)
    {
        var rect = new Rect(waypoint.position.x, waypoint.position.y, 75, 75);
        return rect.Contains(this.transform.position);
    }

    private void moveTo(Transform target)
    {
        this.rb.AddForce((target.position - transform.position).normalized * 5f);
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

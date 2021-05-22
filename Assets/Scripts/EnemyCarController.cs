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

	void Awake () 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
	}
    
	void Update () 
    {
        if (!this.gameController.isFighting)
        {
            if (this.mainTarget != null) {
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                    this.mainTarget.position,
                    2f * Time.deltaTime);
            } 
            else if (this.waypointIndex <= this.waypoints.Length - 1)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                this.waypoints[this.waypointIndex].position,
                2f * Time.deltaTime);

                if (this.transform.position == this.waypoints[this.waypointIndex].position)
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
    }
}

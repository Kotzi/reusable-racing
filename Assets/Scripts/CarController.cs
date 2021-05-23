using UnityEngine;

public class CarController: MonoBehaviour
{
    public Waypoint[] waypoints;
    public int nextWaypoint { get; private set; } = 0;
    public float distanceToNextWaypoint { get; private set; } = 0f;

    internal Rigidbody2D rb;
    internal GameController gameController;

    private DriverController driver;
    private int lap = -1;

	void Awake() 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
        this.driver = this.GetComponent<DriverController>();
        this.forceUpdateWaypointIndex();
	}

    void Update()
    {
        this.distanceToNextWaypoint = Vector2.Distance(this.transform.position, this.waypoints[this.nextWaypoint].transform.position);
        if (this.distanceToNextWaypoint > 3f) 
        {
            this.forceUpdateWaypointIndex();
        } 
        else if (this.waypoints[this.nextWaypoint].getBounds().Contains(this.transform.position))
        {
            if (this.nextWaypoint < this.waypoints.Length - 1)
            {
                this.nextWaypoint += 1;
            }
            else
            {
                this.nextWaypoint = 0;
            }

            this.distanceToNextWaypoint = Vector2.Distance(this.transform.position, this.waypoints[this.nextWaypoint].transform.position);
        }

        this.gameController.currentTrack.updatePosition(this.driver.id, this.lap, this.nextWaypoint, this.distanceToNextWaypoint);
    }

    public bool shouldGetNewLap()
    {
        return this.rb.velocity.y > 0; // Improve this
    }

    public void newLap()
    {
        this.lap += 1;
        print("NEW LAP!");
    }

    private void forceUpdateWaypointIndex()
    {
        this.nextWaypoint = 0;
        this.distanceToNextWaypoint = Mathf.Abs(Vector2.Distance(this.transform.position, this.waypoints[this.nextWaypoint].transform.position));
        for (int i = 0; i < this.waypoints.Length; i++)
        {
            var waypoint = this.waypoints[i];
            float distanceToWaypoint = Mathf.Abs(Vector2.Distance(this.transform.position, waypoint.transform.position));
            if (distanceToWaypoint <= this.distanceToNextWaypoint)
            {
                this.nextWaypoint = i;
                this.distanceToNextWaypoint = distanceToWaypoint;
            }
        }

        if (this.nextWaypoint < this.waypoints.Length - 1)
        {
            this.nextWaypoint += 1;
        }
        else
        {
            this.nextWaypoint = 0;
        }
        this.distanceToNextWaypoint = Vector2.Distance(this.transform.position, this.waypoints[this.nextWaypoint].transform.position);
    }
}
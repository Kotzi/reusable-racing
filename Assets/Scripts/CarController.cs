using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CarController: MonoBehaviour
{
    public Waypoint[] waypoints;
    public int nextWaypoint { get; private set; } = 0;
    public float distanceToNextWaypoint { get; private set; } = 0f;

    internal Rigidbody2D rb;
    internal GameController gameController;
    internal float turbo = 0f;
    internal int lap = -1;

    private SpriteRenderer spriteRenderer;
    private DriverController driver;
    private Collider2D mainCollider;

	void Awake() 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.driver = this.GetComponent<DriverController>();
        this.mainCollider = this.GetComponent<Collider2D>();
        this.forceUpdateWaypointIndex();
	}

    void Update()
    {
        if (this.turbo > 0)
        {
            this.turbo = Mathf.Lerp(this.turbo, 0, Time.deltaTime);
        }

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

        this.gameController.currentTrack.updatePosition(this.driver.id, this.driver.driverName, this.lap, this.nextWaypoint, this.distanceToNextWaypoint);
    }

    public bool shouldGetNewLap()
    {
        return this.rb.velocity.y > 0; // Improve this
    }

    public virtual void newLap()
    {
        this.lap += 1;
    }

    public void fightFinished(bool youWon)
    {
        if (youWon)
        {
            this.turbo += 5f;
        }

        DOTween.Sequence()
            .Append(this.spriteRenderer.DOFade(0.5f, 0.25f))
            .Append(this.spriteRenderer.DOFade(1f, 0.25f))
            .SetLoops(3);

        this.StartCoroutine(this.toggleCollider());
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

    private IEnumerator toggleCollider()
    {
        var others = Object.FindObjectsOfType<CarController>();

        for (int i = 0; i < others.Length; i++)
        {
            Physics2D.IgnoreCollision(this.mainCollider, others[i].mainCollider, true);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0; i < others.Length; i++)
        {
            if (others[i] != null && others[i].mainCollider != null)
            {
                Physics2D.IgnoreCollision(this.mainCollider, others[i].mainCollider, false);
            }
        }

        this.turbo = 0f;
    }
}
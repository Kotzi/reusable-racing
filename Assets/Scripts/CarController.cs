using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CarController: MonoBehaviour
{
    const float MIN_PITCH = 0.05f;

    public Waypoint[] waypoints = null;
    public AudioSource engineAudio;
    public int nextWaypoint { get; private set; } = 0;
    public float distanceToNextWaypoint { get; private set; } = 0f;
    public float maxSpeed = 0f;

    internal Rigidbody2D rb;
    internal GameController gameController;
    internal float turbo = 0f;
    internal int lap = -1;

    private SpriteRenderer spriteRenderer;
    private DriverController driver;
    private ParticleSystem dustParticles;
    private Collider2D mainCollider;
    private float audioPitch = 0f;

	void Awake() 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.driver = this.GetComponent<DriverController>();
        this.mainCollider = this.GetComponent<Collider2D>();
        this.dustParticles = this.GetComponentInChildren<ParticleSystem>();
	}

    void Update()
    {
        if (this.waypoints != null && this.nextWaypoint < this.waypoints.Length - 1)
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

            if(this.audioPitch < MIN_PITCH)
            {
                this.engineAudio.pitch = MIN_PITCH;
            }
            else
            {
                this.engineAudio.pitch = this.audioPitch;
            }
        }
    }

    void LateUpdate()
    {
        this.dustParticles.gameObject.SetActive(this.rb.velocity != Vector2.zero);
        this.audioPitch = (this.rb.velocity.magnitude * 3.6f) / this.maxSpeed;
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

    public void forceUpdateWaypointIndex()
    {
        if (this.waypoints.Length > 0)
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
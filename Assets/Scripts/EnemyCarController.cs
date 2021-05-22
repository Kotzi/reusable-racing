using UnityEngine;

public class EnemyCarController : MonoBehaviour
{
    const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5f;

    public Transform[] waypoints;

    private int waypointIndex = 0;
    private Rigidbody2D rb;
    private GameController gameController;

	void Awake () 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
	}
    
	void Update () 
    {
        if (!this.gameController.isFighting)
        {
            if (this.waypointIndex <= this.waypoints.Length - 1)
            {
                this.transform.position = Vector2.MoveTowards(this.transform.position,
                this.waypoints[this.waypointIndex].transform.position,
                2f * Time.deltaTime);

                if (this.transform.position == this.waypoints[this.waypointIndex].transform.position)
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
}

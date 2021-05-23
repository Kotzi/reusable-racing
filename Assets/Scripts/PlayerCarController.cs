using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
	const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5f;
	const float TORQUE_FORCE = -100f;
	const float DRIFT_FACTOR_STICKY = 0.4f;
	const float DRIFT_FACTOR_SLIPPY = 0.5f;
 	const float MAX_STICKY_VELOCITY = 1.5f;
	const float MAX_SLIPPY_VELOCITY = 0.5f; // ???

    private Rigidbody2D rb;
    private GameController gameController;

	void Awake () 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
        this.gameController = Object.FindObjectOfType<GameController>();
	}

	void FixedUpdate () 
    {
        if (!this.gameController.isFighting)
        {
            var driftFactor = DRIFT_FACTOR_STICKY;
            var rightVelocity = this.getRightVelocity();
            if(rightVelocity.magnitude > MAX_STICKY_VELOCITY) 
            {
                driftFactor = DRIFT_FACTOR_SLIPPY;
            }

            this.rb.velocity = this.getForwardVelocity() + rightVelocity * driftFactor;

            float speed = this.rb.velocity.magnitude;
            if (speed > MAX_SPEED)
            {
                float brakeSpeed = speed - MAX_SPEED;
            
                Vector3 normalisedVelocity = this.rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
            
                this.rb.AddForce(-brakeVelocity); 
            } 
            else if(Input.GetButton("Gas")) 
            {
                this.rb.AddForce(this.transform.up * SPEED_FORCE);
            }

            if(Input.GetButton("Brake")) 
            {
                this.rb.AddForce(this.transform.up * -SPEED_FORCE/2f);
            }

            float tf = Mathf.Lerp(0, TORQUE_FORCE, this.rb.velocity.magnitude / 2);
            this.rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
        }
        else
        {
            this.rb.velocity = Vector2.zero;
            this.rb.angularVelocity = 0f;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<RPGEnemyController>();
        if (enemy != null)
        {
            this.gameController.fight(enemy);
        }
    }

	Vector2 getForwardVelocity() {
		return this.transform.up * Vector2.Dot(this.rb.velocity, this.transform.up);
	}

	Vector2 getRightVelocity() {
		return this.transform.right * Vector2.Dot(this.rb.velocity, this.transform.right);
	}
}

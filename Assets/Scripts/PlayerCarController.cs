using UnityEngine;

public class PlayerCarController: CarController
{
	const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5.25f;
	const float TORQUE_FORCE = -120f;
	const float DRIFT_FACTOR_STICKY = 0.4f;
	const float DRIFT_FACTOR_SLIPPY = 0.5f;
 	const float MAX_STICKY_VELOCITY = 1.5f;

    public override void Awake()
    {
        base.Awake();

        var carProperties = this.GetComponent<CarProperties>();
        this.maxSpeed = MAX_SPEED * carProperties.speedModifier;
        this.speedForce = SPEED_FORCE * carProperties.accelerationModifier;
        this.torqueForce = TORQUE_FORCE * carProperties.torqueModifier;
    }

	void FixedUpdate () 
    {
        if (this.gameController.carsCanMove())
        {
            var driftFactor = DRIFT_FACTOR_STICKY;
            var rightVelocity = this.getRightVelocity();
            if(rightVelocity.magnitude > MAX_STICKY_VELOCITY) 
            {
                driftFactor = DRIFT_FACTOR_SLIPPY;
            }

            this.rb.velocity = this.getForwardVelocity() + rightVelocity * driftFactor;

            float speed = this.rb.velocity.magnitude;
            if (speed > this.maxSpeed)
            {
                float brakeSpeed = speed - this.maxSpeed;
            
                Vector3 normalisedVelocity = this.rb.velocity.normalized;
                Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;
            
                this.rb.AddForce(-brakeVelocity); 
            } 
            else if(Input.GetButton("Gas")) 
            {
                this.rb.AddForce(this.transform.up * (this.speedForce + this.turbo));
            }

            float torqueFactor = Mathf.Lerp(0, this.torqueForce, this.rb.velocity.magnitude / 2);
            if(Input.GetButton("Brake")) 
            {
                var force = this.transform.up * -this.speedForce/2f;
                if (Vector2.Dot(force, this.rb.velocity) == 1f) {
                    torqueFactor *= -1;
                }

                this.rb.AddForce(force);
            }

            this.rb.angularVelocity = Input.GetAxis("Horizontal") * torqueFactor;
        }
        else
        {
            this.rb.velocity = Vector2.zero;
            this.rb.angularVelocity = 0f;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.collider.GetComponent<DriverController>();
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

    public override void newLap()
    {
        base.newLap();
        this.gameController.playerCompletedLap(this.lap);
    }
}

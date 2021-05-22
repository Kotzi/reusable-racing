using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	const float SPEED_FORCE = 10f;
    const float MAX_SPEED = 5f;
	const float TORQUE_FORCE = -100f;
	const float DRIFT_FACTOR_STICKY = 0.4f;
	const float DRIFT_FACTOR_SLIPPY = 0.5f;
 	const float MAX_STICKY_VELOCITY = 1.5f;
	const float MAX_SLIPPY_VELOCITY = 0.5f; // ???

    private Rigidbody2D rb;

	void Awake () 
    {
        this.rb = this.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () 
    {
		var driftFactor = DRIFT_FACTOR_STICKY;
        var rightVelocity = this.getRightVelocity();
        if(rightVelocity.magnitude > MAX_STICKY_VELOCITY) 
        {
			driftFactor = DRIFT_FACTOR_SLIPPY;
		}

		this.rb.velocity = this.getForwardVelocity() + rightVelocity * driftFactor;

        float speed = this.rb.velocity.magnitude;
        print(speed);
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

			// Consider using rb.AddForceAtPosition to apply force twice, at the position
			// of the rear tires/tyres
		}

		if(Input.GetButton("Brake")) {
			this.rb.AddForce(this.transform.up * -SPEED_FORCE/2f);

			// Consider using rb.AddForceAtPosition to apply force twice, at the position
			// of the rear tires/tyres
		}

		// If you are using positional wheels in your physics, then you probably
		// instead of adding angular momentum or torque, you'll instead want
		// to add left/right Force at the position of the two front tire/types
		// proportional to your current forward speed (you are converting some
		// forward speed into sideway force)
		float tf = Mathf.Lerp(0, TORQUE_FORCE, this.rb.velocity.magnitude / 2);
		this.rb.angularVelocity = Input.GetAxis("Horizontal") * tf;
	}

	Vector2 getForwardVelocity() {
		return this.transform.up * Vector2.Dot(this.rb.velocity, this.transform.up);
	}

	Vector2 getRightVelocity() {
		return this.transform.right * Vector2.Dot(this.rb.velocity, this.transform.right);
	}
}

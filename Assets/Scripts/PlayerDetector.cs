using UnityEngine;

public class PlayerDetector: MonoBehaviour
{
    private EnemyCarController car;
    private float cooldown = 5f;
    private bool detectorEnabled = false;

    void Awake()
    {
        this.car = this.GetComponentInParent<EnemyCarController>();
    }

    void Update()
    {
        if (this.cooldown <= 0)
        {
            this.detectorEnabled = true;
        }
        else
        {
            this.cooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.detectorEnabled)
        {
            car.updateMainTarget(collider.transform);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        car.updateMainTarget(null);
    }
}

using UnityEngine;

public class PlayerDetector: MonoBehaviour
{
    private EnemyCarController car;
    private Collider2D mainCollider;
    private float cooldown = 5f;

    void Awake()
    {
        this.car = this.GetComponentInParent<EnemyCarController>();
        this.mainCollider = this.GetComponent<Collider2D>();
        this.mainCollider.enabled = false;
    }

    void Update()
    {
        if (this.cooldown <= 0)
        {
            this.mainCollider.enabled = true;
        }
        else
        {
            this.cooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        car.updateMainTarget(collider.transform);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        car.updateMainTarget(null);
    }
}

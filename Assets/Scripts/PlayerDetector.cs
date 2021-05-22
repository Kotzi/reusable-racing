using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private EnemyCarController car;

    void Awake()
    {
        this.car = this.GetComponentInParent<EnemyCarController>();
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

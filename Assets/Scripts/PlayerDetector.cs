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
        var player = collider.GetComponent<CarController>();
        if (player != null)
        {
            car.mainTarget = player.transform;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var player = collider.GetComponent<CarController>();
        if (player != null && car.mainTarget != null && player == car.mainTarget)
        {
            car.mainTarget = null;
        }
    }
}

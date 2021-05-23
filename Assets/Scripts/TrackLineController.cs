using UnityEngine;

public class TrackLineController : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D collider)
    {
        var driver = collider.GetComponent<CarController>();
        if (driver != null && driver.shouldGetNewLap())
        {
            driver.newLap();
        }
    }
}

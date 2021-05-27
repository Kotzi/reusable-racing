using UnityEngine;
using TMPro;

public class PauseCanvasController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text continueButtonText;

    public void onContinueButtonClicked()
    {
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
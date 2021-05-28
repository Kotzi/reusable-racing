using UnityEngine;
using TMPro;

public class PauseCanvasController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text continueButtonText;

    void Awake()
    {
        this.titleText.text = LanguageController.shared.getPauseText();
        this.continueButtonText.text = LanguageController.shared.getContinueButtonText();
    }

    public void onContinueButtonClicked()
    {
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}
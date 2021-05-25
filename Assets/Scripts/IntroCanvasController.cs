using TMPro;
using UnityEngine;
using DG.Tweening;

public class IntroCanvasController : MonoBehaviour
{
    public TMP_Text nameTitleText;
    public TMP_InputField nameInputTextField;
    public TMP_Text nameInputPlaceholderText;
    public TMP_Text nameContinueButtonText;
    public PersistentDataController persistentDataController;
    public SceneManagerController sceneManagerController;

    void Awake()
    {
        this.sceneManagerController.currentSceneIndex = 1;
    }

    public void onNameContinueButtonClicked()
    {
        if (this.nameInputTextField.text != "")
        {
            this.persistentDataController.userName = this.nameInputTextField.text;
            this.sceneManagerController.goToNexScene();
        }
        else
        {
            this.nameInputTextField.transform.DOPunchRotation(new Vector3(1f, 1f, 0f), 0.25f);
        }
    }
}

using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IntroCanvasController : MonoBehaviour
{
    public CanvasRenderer namePanel;
    public TMP_Text nameTitleText;
    public TMP_InputField nameInputTextField;
    public TMP_Text nameInputPlaceholderText;
    public TMP_Text nameContinueButtonText;
    public CanvasRenderer carPanel;

    public SceneManagerController sceneManagerController;

    void Awake()
    {
        if(PersistentDataController.shared == null)
        {
            var persistentDataController = new GameObject("PersistentDataController");
            persistentDataController.AddComponent<PersistentDataController>();
        }

        this.sceneManagerController.currentSceneIndex = 1;

        if (PersistentDataController.shared.level == 1) 
        {
            this.namePanel.gameObject.SetActive(true);
            this.carPanel.gameObject.SetActive(false);
        }
        else
        {
            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(true);
        }
    }

    public void onNameContinueButtonClicked()
    {
        if (this.nameInputTextField.text != "")
        {
            PersistentDataController.shared.userName = this.nameInputTextField.text;
            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(true);
        }
        else
        {
            this.nameInputTextField.transform.DOPunchRotation(new Vector3(1f, 1f, 0f), 0.25f);
        }
    }

    public void onRedCarButtonClicked()
    {
        this.pickCar(0);
    }

    public void onPurpleCarButtonClicked()
    {
        this.pickCar(1);
    }

    public void onBlueCarButtonClicked()
    {
        this.pickCar(2);
    }

    public void onGreenCarButtonClicked()
    {
        this.pickCar(3);
    }

    private void pickCar(int car)
    {
        PersistentDataController.shared.car = car;
        this.sceneManagerController.goToNextScene();
    }
}

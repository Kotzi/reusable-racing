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
    public TMP_Text carTitleText;
    public CanvasRenderer experiencePanel;
    public TMP_Text experienceTitleText;
    public TMP_Text experienceAmountText;
    public TMP_Text experienceLevelText;
    public TMP_Text experienceCongratsText;
    public SceneManagerController sceneManagerController;

    void Awake()
    {
        if(PersistentDataController.shared == null)
        {
            var persistentDataController = new GameObject("PersistentDataController");
            persistentDataController.AddComponent<PersistentDataController>();
        }

        this.sceneManagerController.currentSceneIndex = 1;
        PersistentDataController.shared.currentTrack = -1;

        if (PersistentDataController.shared.userName == null) 
        {
            this.namePanel.gameObject.SetActive(true);
            this.carPanel.gameObject.SetActive(false);
            this.experiencePanel.gameObject.SetActive(false);
        }
        else
        {
            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(false);
            this.experiencePanel.gameObject.SetActive(true);

            this.carTitleText.text = $"Hello {PersistentDataController.shared.userName}, what would you like to drive today?";

            this.experienceTitleText.text = $"Hello again {PersistentDataController.shared.userName}, lets take a look at that last performance";
            this.experienceAmountText.text = $"Wow, you earned {PersistentDataController.shared.experience} experience!";
            if (this.processExperience())
            {
                this.experienceLevelText.text = $"You reached level {PersistentDataController.shared.level}";
            }
            else
            {
                this.experienceLevelText.text = "";
            }
        }
    }

    public void onNameContinueButtonClicked()
    {
        if (this.nameInputTextField.text != "")
        {
            PersistentDataController.shared.userName = this.nameInputTextField.text;

            this.carTitleText.text = $"Hello {PersistentDataController.shared.userName}, what would you like to drive today?";

            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(true);
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

    public void onExperienceContinueButtonClicked()
    {
        this.experiencePanel.gameObject.SetActive(false);
        this.carPanel.gameObject.SetActive(true);
    }

    private void pickCar(int car)
    {
        PersistentDataController.shared.car = car;
        this.sceneManagerController.goToNextScene();
    }

    private bool processExperience()
    {
        var nextLevelExperience = this.experienceForLevel(PersistentDataController.shared.level + 1);
        if (PersistentDataController.shared.experience >= nextLevelExperience) 
        {
            PersistentDataController.shared.level += 1;
            PersistentDataController.shared.experience -= nextLevelExperience;
            PersistentDataController.shared.maxLives = Mathf.Min(5, PersistentDataController.shared.level);

            return true;
        }

        return false;
    }

    private int experienceForLevel(int level)
    {
        var exponent = 1.5f;
        var baseXP = 1000;
        return Mathf.FloorToInt(baseXP * (Mathf.Pow(level, exponent)));
    }
}

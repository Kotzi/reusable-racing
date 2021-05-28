using TMPro;
using UnityEngine;
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
    public TMP_Text carSubitleText;
    public CanvasRenderer experiencePanel;
    public TMP_Text experienceTitleText;
    public TMP_Text experienceAmountText;
    public TMP_Text experienceLevelText;
    public TMP_Text experienceCongratsText;
    public TMP_Text experienceContinueText;
    public CanvasRenderer introPanel;
    public TMP_Text introPanelText;
    public TMP_Text introContinueButtonText;
    public Image trophyImage;
    public TMP_Text trophyText;
    public SceneManagerController sceneManagerController;

    void Awake()
    {
        this.introPanelText.text = LanguageController.shared.getIntroText();
        this.introContinueButtonText.text = LanguageController.shared.getContinueButtonText();

        this.nameContinueButtonText.text = LanguageController.shared.getContinueButtonText();
        this.nameInputPlaceholderText.text = LanguageController.shared.getIntroInputPlaceholderText();
        this.nameTitleText.text = LanguageController.shared.getIntroNameText();

        this.experienceCongratsText.text = LanguageController.shared.getCongratsText();
        this.experienceContinueText.text = LanguageController.shared.getContinueButtonText();
        
        this.trophyText.text = LanguageController.shared.getIntroTrophyText();

        if(PersistentDataController.shared == null)
        {
            var persistentDataController = new GameObject("PersistentDataController");
            persistentDataController.AddComponent<PersistentDataController>();
        }

        this.sceneManagerController.currentSceneIndex = 2;
        PersistentDataController.shared.currentTrack = -1;

        if (PersistentDataController.shared.userName == null) 
        {
            this.introPanel.gameObject.SetActive(true);
            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(false);
            this.experiencePanel.gameObject.SetActive(false);
        }
        else
        {
            this.introPanel.gameObject.SetActive(false);
            this.namePanel.gameObject.SetActive(false);
            this.carPanel.gameObject.SetActive(false);
            this.experiencePanel.gameObject.SetActive(true);

            this.trophyImage.gameObject.SetActive(PersistentDataController.shared.wonTrophy);

            this.carTitleText.text = LanguageController.shared.getIntroCarTitleText(PersistentDataController.shared.userName);

            this.experienceTitleText.text = LanguageController.shared.getIntroExperienceTitleText(PersistentDataController.shared.userName);

            this.experienceAmountText.text = LanguageController.shared.getIntroExperienceGotExperienceText(PersistentDataController.shared.experience);
            if (this.processExperience())
            {
                this.experienceLevelText.text = LanguageController.shared.getIntroExperienceNewLevelText(PersistentDataController.shared.level);

            }
            else
            {
                this.experienceLevelText.text = "";
            }
        }
    }

    public void oIntroContinueButtonClicked()
    {
        this.introPanel.gameObject.SetActive(false);
        this.namePanel.gameObject.SetActive(true);
    }

    public void onNameContinueButtonClicked()
    {
        if (this.nameInputTextField.text != "")
        {
            PersistentDataController.shared.userName = this.nameInputTextField.text;

            this.carTitleText.text = LanguageController.shared.getIntroCarTitleText(PersistentDataController.shared.userName);

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
        PersistentDataController.shared.wonTrophy = false;
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

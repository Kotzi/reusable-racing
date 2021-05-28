using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public TMP_Text jamText;
    public TMP_Text genresText;
    public TMP_Text startButtonText;
    public SoundButton SoundButton;
    private LanguageController LanguageController;
    private SceneManagerController SceneManagerController;

    void Awake()
    {
        SoundButton.AudioController = GetComponent<AudioController>();
        SceneManagerController = Object.FindObjectOfType<SceneManagerController>();
        SceneManagerController.currentSceneIndex = 1;
        LanguageController = LanguageController.shared;
        this.reloadTexts();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) 
        {
            SceneManagerController.goToNextScene();
        }
    }

    public void OnClickStart() 
    {
        SceneManagerController.goToNextScene();
    }

    public void OnClickES()
    {
        LanguageController.currentLanguage = LanguageController.Language.ES;
        this.reloadTexts();
    }

    public void OnClickEN()
    {
        LanguageController.currentLanguage = LanguageController.Language.EN;
        this.reloadTexts();
    }

    private void reloadTexts()
    {
        this.startButtonText.text = LanguageController.shared.getStartButtonText();
        this.jamText.text = LanguageController.shared.getJamText();
        this.genresText.text = LanguageController.shared.getGenresText();
    }
}
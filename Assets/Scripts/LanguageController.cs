using UnityEngine;

class LanguageController : MonoBehaviour {
    public enum Language
    {
        EN = 0,
        ES
    }

    private static LanguageController _instance;
    public static LanguageController Shared {
        get {
            if (_instance != null) {
                return _instance;
            } else {
                _instance = Object.FindObjectOfType<LanguageController>();
                if (_instance != null) {
                    return _instance;
                } else {
                    _instance = (new GameObject()).AddComponent<LanguageController>();
                    return _instance;
                }
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public Language currentLanguage = Language.EN;

    public string getStartButtonText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Start";
            case Language.ES: return "Comenzar";
        }

        return "";
    }

    public string getYouWonText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "You won!";
            case Language.ES: return "Ganaste!";
        }

        return "";
    }

    public string getYouLostText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "You lost!";
            case Language.ES: return "Perdiste!";
        }

        return "";
    }
}
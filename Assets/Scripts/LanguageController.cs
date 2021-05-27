using UnityEngine;

class LanguageController : MonoBehaviour {
    public enum Language
    {
        EN = 0,
        ES
    }

    private static LanguageController _instance;
    public static LanguageController shared {
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

    public string getIntroText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return 
@"The Earth is suffering a lot from all the usage of that plastic that humans did in the past. 
<i>Nobody is gonna get hurt from using this plastic fork</i>, they said.
Now, on this arid wasteland, some cars found a <i>spark</i> of joy on their mechanical lives: racing and hitting themselves using reusable items (because they are nice folks, of course). 
There's even a misterious one who claims to be the trully leader, master and king of the <b>Reusable Cup</b>.
Could you beat them?";
            case Language.ES: return 
@"The Earth is suffering a lot from all the usage of that plastic that humans did in the past. ";
        }

        return "";
    }
}
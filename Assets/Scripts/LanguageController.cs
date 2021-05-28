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
There's even a misterious one who claims to be the true leader, master and king of the <b>Reusable Cup</b>.
Could you beat them?";
            case Language.ES: return 
@"La Tierra está sufriendo mucho por todo el uso de plásticos que los humanos hicieron en el pasado.
<i>Nadie saldrá herido por usar un tenedor de plástico</i>, decían ellos.
Ahora, en este árido terreno, algunos autos encontraron una <i>chispa</i> de alegría en sus vidas mecánicas: correr y golpearse entre ellos usando elementos reusables (porque ellos son buena gente, claramente).
Incluso hay uno misterioso que asegura ser el líder real y rey de la <b>Reusable Cup</b>.
Podrás vencerlos?";
        }

        return "";
    }

    public string getJamText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "A submission for the Genre Mash Jam";
            case Language.ES: return "Un juego para la Genre Mash Jam";
        }

        return "";
    }

    public string getGenresText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Genres: racing, RPG and roguelike";
            case Language.ES: return "Géneros: carreras, RPG y roguelike";
        }

        return "";
    }

    public string getIntroNameText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Hello newcomer, what is your name?";
            case Language.ES: return "Hola, ¿cuál es tu nombre?";
        }

        return "";
    }

    public string getIntroInputPlaceholderText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Enter your name...";
            case Language.ES: return "Ingresá tu nombre...";
        }

        return "";
    }

    public string getContinueButtonText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Continue";
            case Language.ES: return "Continuar";
        }

        return "";
    }

    public string getIntroExperienceTitleText(string name)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Hello again {name}, lets take a look at that last performance";
            case Language.ES: return $"Hola de nuevo {name}, veamos los resultados de esa última carrera";
        }

        return "";
    }

    public string getIntroExperienceGotExperienceText(int experience)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Wow, you earned {experience} experience points!";
            case Language.ES: return $"Wow, ganaste {experience} puntos de experiencia!";
        }

        return "";
    }

    public string getIntroExperienceNewLevelText(int level)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"You reached level {level}!";
            case Language.ES: return $"Alcanzaste el nivel {level}!";
        }

        return "";
    }

    public string getIntroTrophyText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"And you won the Reusable Cup!";
            case Language.ES: return $"Y ganaste la Reusable Cup!";
        }

        return "";
    }

    public string getCongratsText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Congrats!";
            case Language.ES: return $"Felicitaciones!";
        }

        return "";
    }

    public string getIntroCarTitleText(string name)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"{name}, what would you like to drive today?";
            case Language.ES: return $"Hola de nuevo {name}, veamos los resultados de esa última carrera";
        }

        return "";
    }

    public string getIntroCarSubtitleText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "They all have different properties!";
            case Language.ES: return "Todos tienen distintas propiedades!";
        }

        return "";
    }

    public string getPauseText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Pause";
            case Language.ES: return "Pausa";
        }

        return "";
    }

    public string getRaceFinishedTitleText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Race finished!";
            case Language.ES: return "Carrera terminada!";
        }

        return "";
    }

    public string getFightTitleText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "FIGHT!";
            case Language.ES: return "PELEA!";
        }

        return "";
    }

    public string getFightLivesText(int lives)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Lives: {lives}";
            case Language.ES: return $"Vidas: {lives}";
        }

        return "";
    }

    public string getFightLevelText(int level)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Level: {level}";
            case Language.ES: return $"Nivel: {level}";
        }

        return "";
    }

    public string getFightMissText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "MISS";
            case Language.ES: return "FALLÓ";
        }

        return "";
    }

    public string getFightCriticalText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "CRITICAL!";
            case Language.ES: return "CRÍTICO!";
        }

        return "";
    }

    public string getFightBagText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Reusable bag\n(with rocks)";
            case Language.ES: return "Bolsa reusable\n(con rocas)";
        }

        return "";
    }

    public string getFightChopsticksText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Reusable\nchopsticks";
            case Language.ES: return "Palillos\nreusable";
        }

        return "";
    }

    public string getFightStrawText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Reusable straw\n(with projectiles)";
            case Language.ES: return "Sorbete reusable\n(con proyectiles)";
        }

        return "";
    }

    public string getFightRazorText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Reusable razor";
            case Language.ES: return "Afeitadora\nreusable";
        }

        return "";
    }

    public string getFightLevelCoverText(int level)
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return $"Level {level}";
            case Language.ES: return $"Nivel {level}";
        }

        return "";
    }

    public string getYouDiedText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "You died!";
            case Language.ES: return "Moriste!";
        }

        return "";
    }

    public string getRaceAgainText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "Race again";
            case Language.ES: return "Correr de nuevo";
        }

        return "";
    }

    public string getYouWonCupText()
    {
        switch (this.currentLanguage) 
        {
            case Language.EN: return "You won the Reusable Cup!\nYou are the best!";
            case Language.ES: return "Ganaste la Reusable Cup!\nSos el mejor!";
        }

        return "";
    }
}
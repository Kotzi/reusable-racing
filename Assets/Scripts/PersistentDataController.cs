using UnityEngine;

public class PersistentDataController: MonoBehaviour
{
    public static PersistentDataController shared;

    public string userName = null;
    public int level = 1;
    public int experience = 0;
    public int maxLives = 1;
    public int currentTrack = -1;
    public int car = 0;
    public bool wonTrophy = false;

    void Awake()
    {
        if (PersistentDataController.shared == null)
        {
            DontDestroyOnLoad(this);
            PersistentDataController.shared = this;
        }
        else 
        {
            Debug.LogError("YOU SHOULDN'T CREATE ANOTHER PERSISTENT DATA CONTROLLER");
        }
    }
}
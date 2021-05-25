using UnityEngine;

public class PersistentDataController: MonoBehaviour
{
    public static PersistentDataController shared;

    public string userName = null;

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
using TMPro;
using UnityEngine;

public class RaceUICanvas : MonoBehaviour
{
    public TMP_Text debugText;

    public void updateDebugText(string text)
    {
        this.debugText.text = text;
    }
}

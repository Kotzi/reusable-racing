using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceFinishedCanvasController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text resultsText;
    public GameController gameController;

    private List<string> positions = new List<string>();

    public void addPosition(string name, List<(string, int, int, float, string)> fillWith)
    {
        this.positions.Add(name);

        if (fillWith != null)
        {
            foreach (var o in fillWith)
            {
                if (!this.positions.Contains(o.Item1)) {
                    this.positions.Add(o.Item1);
                }
            }
        }

        var text = "";
        foreach (var p in this.positions)
        {
            text += $"{p}\n";
        }

        this.resultsText.text = text;
    }

    public void onContinueButtonClicked()
    {
        this.gameController.raceFinished();
    }
}

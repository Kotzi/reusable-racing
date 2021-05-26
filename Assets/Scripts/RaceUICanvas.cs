using TMPro;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class RaceUICanvas : MonoBehaviour
{
    public GameController gameController;
    public TMP_Text debugText;
    public TMP_Text countdownText;
    public TMP_Text lapsText;

    void Start()
    {
        this.countdownText.text = "3";
        DOTween.Sequence()
                .Join(this.countdownText.transform.DOScale(new Vector3(5f, 5f, 5f), 0.5f))
                .Join(this.countdownText.DOFade(0.5f, 0.5f))
                .OnComplete(() => {
                    this.countdownText.text = "2";
                    this.countdownText.transform.localScale = Vector3.one;
                    this.countdownText.alpha = 1f;

                    DOTween.Sequence()
                            .Join(this.countdownText.transform.DOScale(new Vector3(5f, 5f, 5f), 0.5f))
                            .Join(this.countdownText.DOFade(0.5f, 0.5f))
                            .OnComplete(() => {
                                this.countdownText.text = "1";
                                this.countdownText.transform.localScale = Vector3.one;
                                this.countdownText.alpha = 1f;

                                DOTween.Sequence()
                                        .Join(this.countdownText.transform.DOScale(new Vector3(5f, 5f, 5f), 0.5f))
                                        .Join(this.countdownText.DOFade(0.5f, 0.5f))
                                        .OnComplete(() => {
                                            Destroy(this.countdownText);
                                            this.gameController.raceStarted = true;
                                        });
                            });
                });
    }

    public void updatePositions(List<(string, int, int, float, string)> positions)
    {
        var text = "";
        foreach (var p in positions)
        {
            text += $"{p.Item1}\n";
        }
        
        this.debugText.text = text;
    }

    public void updateLaps(int currentLap, int totalLaps)
    {
        var fixedCurrentLap = currentLap >= 0 ? currentLap : 0;
        this.lapsText.text = $"{fixedCurrentLap}/{totalLaps}";

        if (fixedCurrentLap != 0)
        {
            this.lapsText.transform.DOPunchScale(Vector3.one * 2f, 0.2f);
        }
    }
}

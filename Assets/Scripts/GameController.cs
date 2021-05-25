using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fightCanvasPrefab;
    public DriverController player;
    public RaceUICanvas raceUICanvas;
    public RaceFinishedCanvasController raceFinishedCanvas;
    public TrackController currentTrack;
    public bool isFighting { get; private set; } = false;
    public bool raceStarted = false;

    void Awake()
    {
        this.player.driverName = PersistentDataController.shared.userName;
    }

    public void fight(DriverController enemy)
    {
        if (!this.isFighting)
        {
            this.isFighting = true;

            FightCanvasController canvas = Instantiate(this.fightCanvasPrefab, this.transform.parent).GetComponent<FightCanvasController>();
            canvas.setup(this, this.player, enemy);
        }
    }

    public void fightFinished(bool youWin)
    {
        this.isFighting = false;
    }

    public bool carsCanMove()
    {
        return (this.raceStarted && !this.isFighting);
    }

    public void enemyDied(string id)
    {
        this.currentTrack.enemyDied(id);
    }

    public void playerCompletedLap(int currentLap)
    {
        this.raceUICanvas.updateLaps(currentLap, this.currentTrack.laps);
        
        if (currentLap == this.currentTrack.laps)
        {
            this.raceStarted = false;
            this.raceFinishedCanvas.addPosition(this.player.driverName, this.currentTrack.sortedPositions);
            this.raceFinishedCanvas.gameObject.SetActive(true);
        }
    }

    public void enemyCompletedLap(string name, int currentLap)
    {
        if (currentLap == this.currentTrack.laps)
        {
            this.raceFinishedCanvas.addPosition(name, null);
        }
    }
}

using UnityEngine;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public GameObject fightCanvasPrefab;
    public GameObject firstTrackPrefab;
    public GameObject secondTrackPrefab;
    public DriverController player;
    public GameObject redCar;
    public GameObject purpleCar;
    public GameObject blueCar;
    public GameObject greenCar; 
    public RaceUICanvas raceUICanvas;
    public RaceFinishedCanvasController raceFinishedCanvas;
    public TrackController currentTrack;
    public CinemachineVirtualCamera mainCamera;
    public SceneManagerController sceneManager;
    public bool isFighting { get; private set; } = false;
    public bool raceStarted = false;

    void Awake()
    {
        this.sceneManager.currentSceneIndex = 2;

        if(PersistentDataController.shared == null)
        {
            var persistentDataController = new GameObject("PersistentDataController");
            persistentDataController.AddComponent<PersistentDataController>();
            PersistentDataController.shared.userName = "Testing Scene";
        }

        PersistentDataController.shared.currentTrack += 1;

        switch (PersistentDataController.shared.currentTrack)
        {
            case 0: {
                this.currentTrack = Instantiate(this.firstTrackPrefab, this.transform.parent).GetComponent<TrackController>();
                break;
            }
            case 1: {
                this.currentTrack = Instantiate(this.secondTrackPrefab, this.transform.parent).GetComponent<TrackController>();
                break;
            }
        }
        this.currentTrack.gameController = this;

        switch (PersistentDataController.shared.car)
        {
            case 0: {
                Destroy(this.redCar.GetComponent<EnemyCarController>());
                Destroy(this.purpleCar.GetComponent<PlayerCarController>());
                Destroy(this.blueCar.GetComponent<PlayerCarController>());
                Destroy(this.greenCar.GetComponent<PlayerCarController>());

                this.purpleCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.blueCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.greenCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);

                this.redCar.transform.position = this.currentTrack.playerStartPosition.position;
                this.purpleCar.transform.position = this.currentTrack.enemy1StartPosition.position;
                this.blueCar.transform.position = this.currentTrack.enemy2StartPosition.position;
                this.greenCar.transform.position = this.currentTrack.enemy3StartPosition.position;

                this.redCar.layer = 6;
                this.purpleCar.layer = 7;
                this.blueCar.layer = 7;
                this.greenCar.layer = 7;

                this.player = this.redCar.GetComponent<DriverController>();
                break; 
            }
            case 1: {
                Destroy(this.redCar.GetComponent<PlayerCarController>());
                Destroy(this.purpleCar.GetComponent<EnemyCarController>());
                Destroy(this.blueCar.GetComponent<PlayerCarController>());
                Destroy(this.greenCar.GetComponent<PlayerCarController>());

                this.redCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.blueCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.greenCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);

                this.redCar.transform.position = this.currentTrack.enemy1StartPosition.position;
                this.purpleCar.transform.position = this.currentTrack.playerStartPosition.position;
                this.blueCar.transform.position = this.currentTrack.enemy2StartPosition.position;
                this.greenCar.transform.position = this.currentTrack.enemy3StartPosition.position;

                this.redCar.layer = 7;
                this.purpleCar.layer = 6;
                this.blueCar.layer = 7;
                this.greenCar.layer = 7;

                this.player = this.purpleCar.GetComponent<DriverController>();
                break;
            }
            case 2: {
                Destroy(this.redCar.GetComponent<PlayerCarController>());
                Destroy(this.purpleCar.GetComponent<PlayerCarController>());
                Destroy(this.blueCar.GetComponent<EnemyCarController>());
                Destroy(this.greenCar.GetComponent<PlayerCarController>());

                this.purpleCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.redCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.greenCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);

                this.redCar.transform.position = this.currentTrack.enemy1StartPosition.position;
                this.purpleCar.transform.position = this.currentTrack.enemy2StartPosition.position;
                this.blueCar.transform.position = this.currentTrack.playerStartPosition.position;
                this.greenCar.transform.position = this.currentTrack.enemy3StartPosition.position;

                this.redCar.layer = 7;
                this.purpleCar.layer = 7;
                this.blueCar.layer = 6;
                this.greenCar.layer = 7;

                this.player = this.blueCar.GetComponent<DriverController>();
                break;
            }
            case 3: {
                Destroy(this.redCar.GetComponent<PlayerCarController>());
                Destroy(this.purpleCar.GetComponent<PlayerCarController>());
                Destroy(this.blueCar.GetComponent<PlayerCarController>());
                Destroy(this.greenCar.GetComponent<EnemyCarController>());

                this.purpleCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.blueCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);
                this.redCar.GetComponentInChildren<PlayerDetector>(includeInactive: true).gameObject.SetActive(true);

                this.redCar.transform.position = this.currentTrack.enemy1StartPosition.position;
                this.purpleCar.transform.position = this.currentTrack.enemy2StartPosition.position;
                this.blueCar.transform.position = this.currentTrack.enemy3StartPosition.position;
                this.greenCar.transform.position = this.currentTrack.playerStartPosition.position;

                this.redCar.layer = 7;
                this.purpleCar.layer = 7;
                this.blueCar.layer = 7;
                this.greenCar.layer = 6;

                this.player = this.greenCar.GetComponent<DriverController>();
                break;
            }
        }

        var redCarController = this.redCar.GetComponent<CarController>();
        redCarController.waypoints = this.currentTrack.waypoints;
        redCarController.forceUpdateWaypointIndex();

        var purpleCarController = this.purpleCar.GetComponent<CarController>();
        purpleCarController.waypoints = this.currentTrack.waypoints;
        purpleCarController.forceUpdateWaypointIndex();

        var blueCarController = this.blueCar.GetComponent<CarController>();
        blueCarController.waypoints = this.currentTrack.waypoints;
        blueCarController.forceUpdateWaypointIndex();

        var greenCarController = this.greenCar.GetComponent<CarController>();
        greenCarController.waypoints = this.currentTrack.waypoints;
        greenCarController.forceUpdateWaypointIndex();

        this.mainCamera.Follow = this.player.transform;
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

    public void raceFinished()
    {
        if (PersistentDataController.shared.currentTrack < 2)
        {
            this.sceneManager.reloadCurrentScene();
        }
        else 
        {
            this.sceneManager.goToPreviousScene();
        }
    }
}

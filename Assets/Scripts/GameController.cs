using UnityEngine;
using Cinemachine;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject fightCanvasPrefab;
    public GameObject firstTrackPrefab;
    public GameObject secondTrackPrefab;
    public GameObject thirdTrackPrefab;
    public GameObject lastTrackPrefab;
    public GameObject youWonGame;
    public TMP_Text youWonText;
    public TMP_Text youWonButtonText;
    public DriverController player;
    public GameObject redCar;
    public GameObject purpleCar;
    public GameObject blueCar;
    public GameObject greenCar; 
    public RaceUICanvas raceUICanvas;
    public RaceFinishedCanvasController raceFinishedCanvas;
    public PauseCanvasController pauseCanvas;
    public TrackController currentTrack;
    public CinemachineVirtualCamera mainCamera;
    public SceneManagerController sceneManager;
    public bool isFighting { get; private set; } = false;
    public bool raceStarted = false;

    void Awake()
    {
        this.sceneManager.currentSceneIndex = 3;

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
            case 2: {
                this.currentTrack = Instantiate(this.thirdTrackPrefab, this.transform.parent).GetComponent<TrackController>();
                break;
            }
            case 3: {
                this.currentTrack = Instantiate(this.lastTrackPrefab, this.transform.parent).GetComponent<TrackController>();
                this.raceUICanvas.gameObject.SetActive(false);
                this.raceStarted = true;
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

        List<CarController> cars = new List<List<CarController>>()
                                            {
                                                new List<CarController>(this.redCar.GetComponents<CarController>()),
                                                new List<CarController>(this.purpleCar.GetComponents<CarController>()),
                                                new List<CarController>(this.blueCar.GetComponents<CarController>()),
                                                new List<CarController>(this.greenCar.GetComponents<CarController>()),
                                            }.SelectMany(x => x).ToList();

        foreach (var car in cars)
        {
            if (car.enabled)
            {
                car.waypoints = this.currentTrack.waypoints;
                car.forceUpdateWaypointIndex();
            }
        }

        this.mainCamera.Follow = this.player.transform;
        this.player.driverName = PersistentDataController.shared.userName;

        var enemyLives = PersistentDataController.shared.currentTrack + 1;
        this.redCar.GetComponent<DriverController>().lives = enemyLives;
        this.purpleCar.GetComponent<DriverController>().lives = enemyLives;
        this.blueCar.GetComponent<DriverController>().lives = enemyLives;
        this.greenCar.GetComponent<DriverController>().lives = enemyLives;

        this.player.lives = PersistentDataController.shared.maxLives;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0f)
            {
                this.unpauseGame();
            }
            else 
            {
                this.pauseGame();
            }
        }
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
        if (youWin)
        {
            PersistentDataController.shared.experience += 800;
        }

        this.isFighting = false;
    }

    public bool carsCanMove()
    {
        return (this.raceStarted && !this.isFighting);
    }

    public void enemyDied(string id)
    {
        // This is awful but I just found out this and requires a really quick fix
        if (this.player.id != id) 
        {
            if (PersistentDataController.shared.currentTrack != 0)
            {
                PersistentDataController.shared.experience += 1000;
            }

            this.currentTrack.enemyDied(id);
        }

        if (PersistentDataController.shared.currentTrack == 3) 
        {
            this.youWonGame.SetActive(true);
        }
    }

    public void playerCompletedLap(int currentLap)
    {
        this.raceUICanvas.updateLaps(currentLap, this.currentTrack.laps);

        var positions = this.currentTrack.sortedPositions;
        var playerPosition = 0;
        for (int i = 0; i < positions.Count; i++)
        {
            if (this.player.id == positions[i].Item5)
            {
                playerPosition = i;
                break;
            }
        }
        PersistentDataController.shared.experience += this.experienceForLap(playerPosition);

        if (currentLap == this.currentTrack.laps)
        {
            this.raceStarted = false;
            this.raceFinishedCanvas.addPosition(this.player.driverName, positions);
            this.raceFinishedCanvas.gameObject.SetActive(true);
            PersistentDataController.shared.experience += this.experienceForRace(playerPosition);
        }
    }

    public void enemyCompletedLap(string name, int currentLap)
    {
        if (currentLap == this.currentTrack.laps)
        {
            this.raceFinishedCanvas.addPosition(name, null);
        }
    }

    public void raceAgain()
    {
        this.sceneManager.goToPreviousScene();
    }

    public void raceFinished()
    {
        if (PersistentDataController.shared.currentTrack < 3)
        {
            this.sceneManager.reloadCurrentScene();
        }
        else 
        {
            PersistentDataController.shared.experience += 3000;
            this.sceneManager.goToPreviousScene();
        }
    }

    public void restartFromYouWonClicked()
    {
        PersistentDataController.shared.experience += 10000;
        this.sceneManager.goToPreviousScene();
    }

    private int experienceForLap(int position)
    {
        var exponent = 2.2f;
        var baseXP = 300;
        var factor = 13;
        return Mathf.FloorToInt(baseXP - factor * (Mathf.Pow(position, exponent)));
    }

    private int experienceForRace(int position)
    {
        var exponent = 3.1f;
        var baseXP = 2000;
        var factor = 12.5f;
        return Mathf.FloorToInt(baseXP - factor * (Mathf.Pow(position, exponent)));
    }

    private void pauseGame()
    {
        Time.timeScale = 0f;
        this.pauseCanvas.gameObject.SetActive(true);
    }

    private void unpauseGame()
    {
        Time.timeScale = 1f;
        this.pauseCanvas.gameObject.SetActive(false);
    }
}

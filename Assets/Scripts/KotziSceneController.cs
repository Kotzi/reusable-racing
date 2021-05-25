using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class KotziSceneController: MonoBehaviour
{
    private const float MAX_TIMER = 1f;

    public Image image;
    public SceneManagerController sceneManagerController;
    private float timer = 0f;
    private bool isTimerActive = true;

    void Awake()
    {
        this.sceneManagerController.currentSceneIndex = 0;
    }

    void Start()
    {
        this.image.DOFade(1f, 0.15f);
    }

    void Update()
    {
        if(this.isTimerActive)
        {
            this.timer += Time.deltaTime;

            if (this.timer >= MAX_TIMER) 
            {
                this.isTimerActive = false;
                this.sceneManagerController.goToNextScene();
            }
        }
    }
}
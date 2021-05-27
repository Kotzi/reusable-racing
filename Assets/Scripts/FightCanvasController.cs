using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FightCanvasController: MonoBehaviour
{
    const int BAG_DAMAGE = 30;
    const float BAG_ACCURACY = 0.75f;
    const int RAZOR_DAMAGE = 20;
    const float RAZOR_ACCURACY = 0.8f;
    const int STRAW_DAMAGE = 15;
    const float STRAW_ACCURACY = 0.9f;
    const int CHOPSTICKS_DAMAGE = 10;
    const float CHOPSTICKS_ACCURACY = 1f;
    const float ENEMY_MAX_COOLDOWN = 2f;

    public TMP_Text titleText;
    public TMP_Text resultText;
    public Image enemyImage;
    public TMP_Text enemyNameText;
    public TMP_Text enemyLivesText;
    public TMP_Text enemyDamageText;
    public Slider enemyHealthSlider;
    public Image playerImage;
    public TMP_Text playerNameText;
    public TMP_Text playerLivesText;
    public TMP_Text playerLevelText;
    public TMP_Text playerDamageText;
    public Slider playerHealthSlider;
    public Slider playerTimeSlider;
    public Image bagImage;
    public Image bagImageCover;
    public Image razorImage;
    public Image razorImageCover;
    public Image strawImage;
    public Image strawImageCover;
    public Image chopsticksImage;
    public CanvasRenderer playerDiedCanvas;

    private GameController gameController;
    private DriverController enemy;
    private DriverController player;
    private float playerWait = 1f;
    private float enemyAttackCooldown = ENEMY_MAX_COOLDOWN;
    private Sequence enemyHealthChangedSequence;
    private Sequence playerHealthChangedSequence;
    private bool battleActive = true;

    void Start()
    {
        this.enemyDamageText.text = "";
        this.playerDamageText.text = "";

        if (PersistentDataController.shared.level >= 4)
        {
            Destroy(this.bagImageCover.gameObject);
        }
        
        if (PersistentDataController.shared.level >= 3)
        {
            Destroy(this.razorImageCover.gameObject);
        }
        
        if (PersistentDataController.shared.level >= 2)
        {
            Destroy(this.strawImageCover.gameObject);
        }

        this.enemyLivesText.text = "Lives: " + this.enemy.lives;

        this.playerLivesText.text = "Lives: " + this.player.lives;
        this.playerLevelText.text = "Level: " + PersistentDataController.shared.level;

        this.enemyHealthChangedSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .Join(this.enemyDamageText.transform.DOScale(Vector3.one * 1.5f, 0.2f))
                .Join(this.enemyDamageText.DOFade(0.5f, 0.2f))
                .Join(this.enemyDamageText.transform.DOLocalMoveY(this.enemyDamageText.transform.localPosition.y + 10f, 0.2f))
                .Join(this.enemyImage.transform.DOPunchPosition(new Vector2(20f, 20f), 0.25f));
                
        this.enemyHealthChangedSequence.OnComplete(() => {
            this.enemyDamageText.text = "";
            this.enemyHealthChangedSequence.Rewind();
        });

        this.playerHealthChangedSequence = DOTween.Sequence()
                .SetAutoKill(false)
                .Join(this.playerDamageText.transform.DOScale(Vector3.one * 1.5f, 0.2f))
                .Join(this.playerDamageText.DOFade(0.5f, 0.2f))
                .Join(this.playerDamageText.transform.DOLocalMoveY(this.playerDamageText.transform.localPosition.y + 10f, 0.2f))
                .Join(this.playerImage.transform.DOPunchPosition(new Vector2(-20f, -20f), 0.25f));
                
        this.playerHealthChangedSequence.OnComplete(() => {
            this.playerDamageText.text = "";
            this.playerHealthChangedSequence.Rewind();
        });
    }

    void Update()
    {
        if (this.battleActive)
        {
            this.playerWait += Time.deltaTime;
            this.playerTimeSlider.value = Mathf.Clamp01(this.playerWait);

            this.enemyAttackCooldown -= Time.deltaTime;
            if (this.enemyAttackCooldown <= 0)
            {
                this.attackPlayer(Mathf.RoundToInt(10 * Random.Range(1f, 1.5f)), Random.Range(0.7f, 1f));
                this.enemyAttackCooldown = ENEMY_MAX_COOLDOWN;
            }
        }
    }

    public void setup(GameController gameController, DriverController player, DriverController enemy)
    {
        this.gameController = gameController;

        this.enemy = enemy;
        this.enemyNameText.text = enemy.driverName;
        this.enemyImage.sprite = enemy.getSprite();
        this.enemyHealthSlider.value = enemy.healthPercentage();
        this.enemyImage.transform.DOShakePosition(0.25f, 3f, 15).SetLoops(-1);

        this.player = player;
        this.playerNameText.text = player.driverName;
        this.playerImage.sprite = player.getSprite();
        this.playerHealthSlider.value = player.healthPercentage();
        this.playerImage.transform.DOShakePosition(0.25f, 3f, 15).SetLoops(-1);

        DOTween.Sequence()
                .Join(this.titleText.transform.DOScale(Vector3.one * 3f, 0.3f))
                .Join(this.titleText.DOFade(0.5f, 0.3f))
                .OnComplete(() => {
                    Destroy(this.titleText.gameObject);
                });   
    }
    
    public void bagAttack()
    {
        this.attackEnemy(BAG_DAMAGE, BAG_ACCURACY);
    }

    public void strawAttack()
    {
        this.attackEnemy(STRAW_DAMAGE, STRAW_ACCURACY);
    }

    public void razorAttack()
    {
        this.attackEnemy(RAZOR_DAMAGE, RAZOR_ACCURACY);
    }

    public void chopsticksAttack()
    {
        this.attackEnemy(CHOPSTICKS_DAMAGE, CHOPSTICKS_ACCURACY);
    }

    public void raceAgainButtonClicked()
    {
        this.gameController.raceAgain();
    }

    void attackEnemy(int damage, float accuracy)
    {
        if (this.playerWait >= 1f)
        {
            this.playerImage.transform.DOPunchPosition(new Vector2(-20f, 20f), 0.25f);
            this.playerWait = 0f;
            this.playerTimeSlider.value = 0f;

            if (Random.value <= accuracy)
            {
                var critical = false;
                if (Random.value >= 0.9)
                {
                    damage *= 2;
                    critical = true;
                }

                this.enemy.takeDamage(damage);

                this.enemyDamageText.text = damage.ToString();
                if (critical)
                {
                    this.enemyDamageText.text += "\nCRITICAL!";
                }

                this.enemyHealthSlider.DOValue(this.enemy.healthPercentage(), 0.25f);

                if (!this.enemy.isAlive())
                {
                    this.finishFight(true);
                }
            }
            else
            {
                this.enemyDamageText.text = "MISS";
            }

            this.enemyHealthChangedSequence.Play();
        }
    }

    void attackPlayer(int damage, float accuracy)
    {
        this.enemyImage.transform.DOPunchPosition(new Vector2(20f, 20f), 0.25f);

        if (Random.value <= accuracy)
        {
            var critical = false;
            if (Random.value >= 0.9)
            {
                damage *= 2;
                critical = true;
            }
            
            this.player.takeDamage(damage);

            this.playerDamageText.text = damage.ToString();
            if (critical)
            {
                this.playerDamageText.text += "\nCRITICAL!";
            }

            this.playerHealthSlider.DOValue(this.player.healthPercentage(), 0.25f);

            if (!this.player.isAlive())
            {
                this.finishFight(false);
            }
        }
        else
        {
            this.playerDamageText.text = "MISS";
        }

        this.playerHealthChangedSequence.Play();
    }

    void finishFight(bool youWon)
    {
        this.battleActive = false;
        
        this.resultText.text = youWon ? LanguageController.Shared.getYouWonText() : LanguageController.Shared.getYouLostText();
        this.resultText.gameObject.SetActive(true);

        DOTween.Sequence()
                .Join(this.resultText.transform.DOScale(Vector3.one * 3f, 0.3f))
                .Join(this.resultText.DOFade(0.5f, 0.3f))
                .OnComplete(() => {
                    if (youWon || this.player.lives > 0)
                    {
                        this.enemy.fightFinished(!youWon);
                        this.player.fightFinished(youWon);
                        this.gameController.fightFinished(youWon);
                        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
                    }
                    else 
                    {
                        this.playerDiedCanvas.gameObject.SetActive(true);
                    }
                });
    }
}

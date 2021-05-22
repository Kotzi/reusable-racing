using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FightCanvasController : MonoBehaviour
{
    const int BAG_DAMAGE = 30;
    const int STRAW_DAMAGE = 15;
    const int RAZOR_DAMAGE = 20;
    const int CHOPSTICKS_DAMAGE = 10;

    public TMP_Text titleText;
    public TMP_Text enemyNameText;
    public Slider enemyHealthSlider;
    public TMP_Text playerNameText;
    public Slider playerHealthSlider;
    private GameController gameController;
    private RPGEnemyController enemy;

    public void setup(GameController gameController, RPGEnemyController enemy)
    {
        this.gameController = gameController;
        this.enemy = enemy;
        this.enemyNameText.text = enemy.enemyName;
    }
    
    public void bagAttack()
    {
        this.attackEnemy(BAG_DAMAGE);
    }

    public void strawAttack()
    {
        this.attackEnemy(STRAW_DAMAGE);
    }

    public void razorAttack()
    {
        this.attackEnemy(RAZOR_DAMAGE);
    }

    public void chopsticksAttack()
    {
        this.attackEnemy(CHOPSTICKS_DAMAGE);
    }

    void attackEnemy(int damage)
    {
        this.enemy.takeDamage(damage);

        this.enemyHealthSlider.value = this.enemy.healthPercentage();

        if (!this.enemy.isAlive())
        {
            this.finishFight(true);
        }
    }

    void finishFight(bool youWin)
    {
        this.enemy.fightFinished();
        this.gameController.fightFinished(youWin);
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}

using TMPro;
using UnityEngine;

public class FightCanvasController : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text enemyNameText;
    private GameController gameController;
    private RPGEnemyController enemy;

    public void setup(GameController gameController, RPGEnemyController enemy)
    {
        this.gameController = gameController;
        this.enemy = enemy;
        this.enemyNameText.text = enemy.enemyName;
    }
    
    public void onFinishButtonClicked()
    {
        print("onFinishButtonClicked");
        this.gameController.fightFinished();
        Destroy(this.transform.gameObject.GetComponentInParent<Canvas>().gameObject);
    }
}

using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fightCanvasPrefab;
    public bool isFighting { get; private set; } = false;
    public void fight(RPGEnemyController enemy)
    {
        if (!this.isFighting)
        {
            this.isFighting = true;

            FightCanvasController canvas = Instantiate(this.fightCanvasPrefab, this.transform.parent).GetComponent<FightCanvasController>();
            canvas.setup(this, enemy);
            print("Fighting against " + enemy.enemyName);
        }
    }

    public void fightFinished()
    {
        this.isFighting = false;
    }
}

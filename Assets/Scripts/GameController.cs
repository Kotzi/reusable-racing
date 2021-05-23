using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject fightCanvasPrefab;
    public PlayerController player;
    public bool isFighting { get; private set; } = false;
    
    public void fight(RPGEnemyController enemy)
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
}

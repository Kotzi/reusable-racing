using UnityEngine;

public class RPGEnemyController : MonoBehaviour
{
    const int MAX_LIFE = 100;
    public string enemyName = "Default enemyName";
    private int health = MAX_LIFE;
    private int lives = 3;

    public void takeDamage(int amount)
    {
        this.health -= amount;

        if (!this.isAlive())
        {
            this.lives -= 1;
        }
    }

    public float healthPercentage()
    {
        return (float)this.health / (float)MAX_LIFE;
    }

    public bool isAlive()
    {
        return (this.health > 0);
    }

    public void fightFinished()
    {
        if (this.lives <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.health = MAX_LIFE;
        }
    }
}

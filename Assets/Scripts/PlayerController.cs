using UnityEngine;

public class PlayerController: MonoBehaviour
{
    const int MAX_LIFE = 100;

    public string playerName = "default player";

    private int health = MAX_LIFE;
    private int lives = 3;

    public Sprite getSprite()
    {
        return this.GetComponent<SpriteRenderer>().sprite;
    }

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
            print("YOU LOST");
        }
        else
        {
            this.health = MAX_LIFE;
        }
    }
}

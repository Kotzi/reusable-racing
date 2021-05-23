using UnityEngine;

public class DriverController: MonoBehaviour
{
    const int MAX_LIFE = 100;

    public string id = System.Guid.NewGuid().ToString();
    public string driverName = "name";

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int health = MAX_LIFE;
    private int lives = 3;
    private int laps = 0;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    public Sprite getSprite()
    {
        return this.spriteRenderer.sprite;
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
        }
        else
        {
            this.health = MAX_LIFE;
        }
    }

    public bool shouldGetNewLap()
    {
        return this.rb.velocity.y > 0; // Improve this
    }

    public void newLap()
    {
        this.laps += 1;
        print("NEW LAP!");
    }
}
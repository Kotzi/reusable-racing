using UnityEngine;

public class DriverController: MonoBehaviour
{
    const int MAX_LIFE = 100;

    public string id = System.Guid.NewGuid().ToString();
    public string driverName = "name";
    public CarController car { get; private set; }

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int health = MAX_LIFE;
    private int lives = 3;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        foreach (var car in this.GetComponents<CarController>())
        {
            if (car.enabled)
            {
                this.car = car;
            }
        }
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

    public void fightFinished(bool youWon)
    {
        this.car.fightFinished(youWon);

        if (this.lives <= 0)
        {
            this.car.gameController.enemyDied(this.id);
            Destroy(this.gameObject);
        }
        else
        {
            this.health = MAX_LIFE;
        }
    }
}
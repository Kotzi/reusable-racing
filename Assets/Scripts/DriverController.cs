using UnityEngine;

public class DriverController: MonoBehaviour
{
    const int MAX_LIFE = 100;

    public string id = System.Guid.NewGuid().ToString();
    public string driverName = "name";
    public CarController car { get; private set; }
    public GameObject explosion;
    public int lives = 3;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float maxHealth = MAX_LIFE;
    private float health = MAX_LIFE;

    void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.rb = this.GetComponent<Rigidbody2D>();
        this.maxHealth = this.GetComponent<CarProperties>().healthModifier * MAX_LIFE;
        this.health = this.maxHealth;
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
        return this.health / this.maxHealth;
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
            this.car.isAlive = false;
            this.car.gameController.enemyDied(this.id);
            this.explosion.SetActive(true);
            Destroy(this.gameObject, 0.9f);
        }
        else if (this.health <= 0)
        {
            this.health = MAX_LIFE;
        }
    }
}
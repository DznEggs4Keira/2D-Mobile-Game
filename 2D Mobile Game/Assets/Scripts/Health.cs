using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject RespawnPoint;
    public HealthBar healthBar;
    public GameOver gameOver;

    public float StartingHealth = 100f;

    int respawnCount = 0;

    public float HealthPoints
    {
        get => _HealthPoints;
        set
        {
            _HealthPoints = Mathf.Clamp(value, 0f, 100f);
            healthBar.SetHealth((int)_HealthPoints);

            if (_HealthPoints <= 0f)
            {
                respawnCount += 1;

                Debug.Log(respawnCount);
                if(respawnCount == 3)
                {
                    respawnCount = 0;
                    //Game Over
                    gameOver.GameOverCalled();
                    return;
                }
                
                //to stop player from sliding
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                StartCoroutine(Respawn(5));
            }
        }
    }

    [SerializeField]
    private float _HealthPoints;

    // Start is called before the first frame update
    void Start()
    {
        HealthPoints = StartingHealth;
        healthBar.SetMaxHealth((int)StartingHealth);
    }

    IEnumerator Respawn(float delay)
    {
        //disable movement
        gameObject.GetComponent<Player_Controller>().enabled = false;
        //Run dead anim
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

        //once the animation has finished
        if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            //don't show player sprite when dead
            GetComponent<SpriteRenderer>().enabled = false;
        }

        //Turn off light
        transform.GetChild(1).gameObject.SetActive(false);

        //wait for 5 seconds
        yield return new WaitForSeconds(delay);

        //reset the position and rotation of the player to the spawn point
        transform.position = RespawnPoint.transform.position;
        transform.rotation = RespawnPoint.transform.rotation;

        //enable the sprite
        GetComponent<SpriteRenderer>().enabled = true;
        //enable light
        transform.GetChild(1).gameObject.SetActive(true);
        //run animation for respawn
        gameObject.GetComponent<Animator>().SetTrigger("Respawn");

        //enable movement
        gameObject.GetComponent<Player_Controller>().enabled = true;
        //reset health
        HealthPoints = StartingHealth;
    }
}

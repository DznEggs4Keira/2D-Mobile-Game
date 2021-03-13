using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject RespawnPoint;
    public HealthBar healthBar;
    public Game_Manager gm;
    public Ad_Manager am;

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
                if(respawnCount >= 3)
                {
                    //player died final
                    FindObjectOfType<Audio_Manager>().Play("Player_Death_Final");

                    //Turn off light
                    transform.GetChild(1).gameObject.SetActive(false);

                    //Run dead anim
                    gameObject.GetComponent<Animator>().SetTrigger("Dead");

                    //once the animation has finished
                    if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Dead"))
                    {
                        //don't show player sprite when dead
                        GetComponent<SpriteRenderer>().enabled = false;

                        //Ad
                        am.PlayInterstitialAd();

                        //Game Over
                        gm.GameOverCalled();

                    }
                }
                else
                {
                    //to stop player from sliding
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                    StartCoroutine(Respawn(5));
                }
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

    public void ResetPlayer()
    {
        respawnCount = 0;

        //reset the position and rotation of the player to the spawn point
        transform.position = RespawnPoint.transform.position;
        transform.rotation = RespawnPoint.transform.rotation;

        //enable the sprite
        GetComponent<SpriteRenderer>().enabled = true;
        //enable light
        transform.GetChild(1).gameObject.SetActive(true);
        //run animation for respawn
        gameObject.GetComponent<Animator>().SetTrigger("Respawn");

        //reset health
        HealthPoints = StartingHealth;
    }

    IEnumerator Respawn(float delay)
    {
        respawnCount += 1;

        Time.timeScale = 0.5f;

        //disable movement
        gameObject.GetComponent<Player_Controller>().enabled = false;

        //player died
        FindObjectOfType<Audio_Manager>().Play("Player_Death");

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
        yield return new WaitForSecondsRealtime(delay);

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

        Time.timeScale = 1f;

        //reset health
        HealthPoints = StartingHealth;
    }
}

using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject RespawnPoint;

    public HealthBar healthBar;

    public float StartingHealth = 100f;

    public float HealthPoints
    {
        get { return _HealthPoints; }
        set
        {
            _HealthPoints = Mathf.Clamp(value, 0f, 100f);
            healthBar.SetHealth((int)_HealthPoints);

            if (_HealthPoints <= 0f)
            {
                //put player on dead layer
                gameObject.layer = 3;

                //disable movement
                gameObject.GetComponent<Player_Controller>().enabled = false;

                StartCoroutine(Respawn(5));
            }
        }
    }

    [SerializeField]
    private float _HealthPoints = 100f;

    // Start is called before the first frame update
    void Start()
    {
        HealthPoints = StartingHealth;
        healthBar.SetMaxHealth((int)StartingHealth);
    }

    IEnumerator Respawn(float delay)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

        yield return new WaitForSeconds(delay);

        transform.position = RespawnPoint.transform.position;
        transform.rotation = RespawnPoint.transform.rotation;

        gameObject.GetComponent<Animator>().SetTrigger("Respawn");
        HealthPoints = StartingHealth;
        gameObject.layer = 0;

        //enable movement
        gameObject.GetComponent<Player_Controller>().enabled = true;
    }
}

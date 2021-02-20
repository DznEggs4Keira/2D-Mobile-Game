using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject GameOverScreen;
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
                StartCoroutine(Respawn(2));
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

    //not working as intended
    IEnumerator Respawn(float delay)
    {
        gameObject.GetComponent<Animator>().SetTrigger("Dead");

        //wait for the animation to end
        yield return new WaitForSeconds(0.360f);

        gameObject.SetActive(false);

        yield return new WaitForSeconds(delay);

        gameObject.SetActive(true);

        transform.position = RespawnPoint.transform.position;
        transform.rotation = RespawnPoint.transform.rotation;

        gameObject.GetComponent<Animator>().SetTrigger("Respawn");
        HealthPoints = StartingHealth;
    }
}

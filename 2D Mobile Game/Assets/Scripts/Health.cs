using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public GameObject RespawnPoint;

    public float StartingHealth = 100f;

    public float HealthPoints
    {
        get { return _HealthPoints; }
        set
        {
            _HealthPoints = Mathf.Clamp(value, 0f, 100f);

            if (_HealthPoints <= 0f)
            {
                //put player on dead layer
                this.gameObject.layer = 8;

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
    }

    IEnumerator Respawn(float delay)
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Dead");

        yield return new WaitForSeconds(delay);

        this.transform.position = RespawnPoint.transform.position;
        this.transform.rotation = RespawnPoint.transform.rotation;

        this.gameObject.GetComponent<Animator>().SetTrigger("Respawn");
        this.gameObject.layer = 0;
    }
}

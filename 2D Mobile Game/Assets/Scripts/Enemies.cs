using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Animator animator = null;

    float boxDamage = 10f;
    float spikeBoxDamage = 20f;
    float saw = 40f;

    float force = 200f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore collisions with upper bound
        if(collision.gameObject.CompareTag("Upper_Bound"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
        else
        {
            //if this is the Gravity_Pool
            if (gameObject.CompareTag("Gravity_Pool"))
            {
                GravityPool(collision);
            }
            else if(collision.gameObject.CompareTag("Player"))
            {
                RegularEnemies(collision);
            }
            //if the collision is with boundaries or gravity pool
            else
            {
                EnemyBounceBack(collision);
            }
        }
    }

    private void EnemyBounceBack(Collision2D collision)
    {
        // Enemy bouces back
        //the angle at the point of contact
        Vector3 dir = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - transform.position;

        dir = -dir.normalized;

        GetComponent<Rigidbody2D>().velocity = (dir * force * Time.deltaTime);
    }

    private void GravityPool(Collision2D collision)
    {
        //if player, instant death
        DealDamage(collision);

        //Enemies should bounce back
        EnemyBounceBack(collision);
    }

    private void RegularEnemies(Collision2D collision)
    {
        EnemyBounceBack(collision);

        //Animation for Hit
        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        DealDamage(collision);
    }

    private void DealDamage(Collision2D collision)
    {
        //Health component to handle damage dealt to player
        Health H = collision.gameObject.GetComponent<Health>();

        if (H == null) return;

        string tag = gameObject.tag;
        switch (tag)
        {
            case "Gravity_Pool":
                {
                    //instant death to player
                    H.HealthPoints = 0f;
                    break;
                }
            case "Box_10":
                {
                    //deal 10 damage for normal box
                    H.HealthPoints -= boxDamage;
                    break;
                }
            case "SpikeBox_20":
                {
                    //deal 20 damage for spiked box
                    H.HealthPoints -= spikeBoxDamage;
                    break;
                }
            case "Saw_40":
                {
                    //deal 40 damage for spinning saw
                    H.HealthPoints -= saw;
                    break;
                }
        }
    }
}

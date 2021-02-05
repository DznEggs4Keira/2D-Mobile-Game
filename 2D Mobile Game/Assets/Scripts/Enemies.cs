using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Animator animator = null;

    private float damage = 10f;
    private float force = 300f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore collisions with powerups or other enemies
        if(collision.gameObject.CompareTag("Powerups") || collision.gameObject.CompareTag("Enemies"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>(), true);
        }
        else
        {
            //Call Handle if regular enemy or gravity pool
            if (gameObject.CompareTag("Gravity_Pool"))
            {
                GravityPool(collision);
            }
            else
            {
                RegularEnemies(collision);
            }

            //Handle Damage
            DealDamage(collision);
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
    }

    private void DealDamage(Collision2D collision)
    {
        //Health component to handle damage dealt to player
        Health H = collision.gameObject.GetComponent<Health>();

        if (H == null) return;

        if(gameObject.CompareTag("Gravity_Pool"))
        {
            //instant death to player
            H.HealthPoints = 0f;
        }
        else
        {
            //Damage Dealt
            H.HealthPoints -= damage;
        }
    }
}

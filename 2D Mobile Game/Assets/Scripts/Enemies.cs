using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Animator animator = null;

    private float damage = 10f;
    private float force = 300f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore collisions with powerups
        if(collision.gameObject.CompareTag("Powerups") || collision.gameObject.CompareTag("Enemies"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>(), true);
        }
        else
        {
            // Enemy bouces back
            //the angle at the point of contact
            Vector3 dir = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0) - transform.position;

            dir = -dir.normalized;

            this.GetComponent<Rigidbody2D>().velocity = (dir * force * Time.deltaTime);

            //Animation for Hit
            if (animator != null)
            {
                animator.SetTrigger("Hit");
            }

            //Damage Dealt
            Health H = collision.gameObject.GetComponent<Health>();

            if (H == null) return;

            //if this script is on the gravity pool and the player comes into contact
            if (this.gameObject.CompareTag("Gravity_Pool"))
            {
                //instant death
                H.HealthPoints = 0f;
            }

            else
            {
                H.HealthPoints -= damage;
            }
        }
    }
}

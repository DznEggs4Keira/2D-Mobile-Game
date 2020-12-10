using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEnemy : MonoBehaviour
{
    public Animator animator;
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if hits the walls
        if (collision.gameObject.tag == "Boundaries")
            animator.SetTrigger("WallHit");
        else if (collision.gameObject.tag == "Gravity_Pool")
            animator.SetTrigger("BottomHit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Upper_Bound")
        {
            Physics2D.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider2D>());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxEnemy : MonoBehaviour
{
    public Animator animator;
    private void OnCollisionStay2D(Collision2D collision)
    {
        animator.SetTrigger("Hit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Upper_Bound")
        {
            Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), collision.collider);
        }
    }
}

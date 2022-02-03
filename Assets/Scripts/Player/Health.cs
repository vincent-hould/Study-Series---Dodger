using UnityEngine;

public class Health : MonoBehaviour
{
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy") {
            PlayerEvents.RaisePlayerDeath();
            animator.SetTrigger("receiveHit");
            animator.SetBool("dead", true);
        }
    }
}

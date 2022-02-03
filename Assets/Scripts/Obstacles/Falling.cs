using UnityEngine;

public class Falling : MonoBehaviour
{
    [SerializeField] private float speed;

    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        transform.position += Vector3.down * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            gameObject.tag = "Untagged";
            animator.SetTrigger("die");
        }
    }

    public void Destroy() {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class Controllable : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private bool isLocked = false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float speedMultiplier = 1f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private float move;
    private float spriteWidth;


    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.sprite.bounds.size.x;
        PlayerEvents.OnPlayerDeath += Disable;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (!isLocked)
            {
                move = Input.GetAxisRaw("Horizontal");
                if (move != 0f)
                {
                    animator.SetBool("isRunning", true);
                    spriteRenderer.flipX = move < 0f;
                }
                else
                {
                    animator.SetBool("isRunning", false);
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    animator.SetTrigger("dash");
                }
            }
        } else
        {
            move = 0f;
        }
    }

    private void FixedUpdate()
    {
        Camera cam = Camera.main;
        float minX = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + (spriteWidth / 2);
        float maxX = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - (spriteWidth / 2);
        float newX = Mathf.Clamp(transform.position.x + (move * speed * speedMultiplier * Time.fixedDeltaTime), minX, maxX);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    private void Disable()
    {
        isEnabled = false;
    }

    private void SetLocked(int locked)
    {
        isLocked = locked > 0;
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameOverManager gameOverManager;
    private Rigidbody2D rb;
    public float speed = 2.0f; // Speed of the player's downward movement
    public float swayAmplitude = 0.5f; // Amplitude of the horizontal sway
    public float pushForce = 5.0f; // Force applied when player presses left/right

    private bool isLeftPressed;
    private bool isRightPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ========== DIFFICULTY ==========
        float depth = Mathf.Abs(transform.position.y);
        float currentSpeed = speed + (depth / 400f);

        // ========== PHYSICS ==========
        float tiltSpeed = 3f;
        float maxTilt = 25f;
        float tilt = Mathf.Sin(Time.time * tiltSpeed) * maxTilt; // Calculate tilt based on time

        transform.rotation = Quaternion.Euler(0, 0, tilt);

        float verticalMovement = -currentSpeed * Time.deltaTime;
        float horizontalGlide = (tilt / 50f) * swayAmplitude * Time.deltaTime; // Adjust horizontal movement based on tilt

        transform.Translate(horizontalGlide, verticalMovement, 0, Space.World);

        // ========== INPUT HANDLING ==========
        isLeftPressed = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        isRightPressed = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    void FixedUpdate()
    {
        if(isLeftPressed)
        {
            rb.AddForce(Vector2.left * pushForce, ForceMode2D.Force);
        }

        if(isRightPressed)
        {
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Force);
        }
    }

    void OnBecameInvisible()
    {
        if(gameOverManager != null) gameOverManager.TriggerGameOver();
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Deadly"))
            {
                if(gameOverManager != null) gameOverManager.TriggerGameOver();
                Destroy(gameObject);
            }
    }
}
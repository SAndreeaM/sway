using UnityEngine;

public class Raindrop : MonoBehaviour
{
    public float speed = 5.0f;
    public float acceleration = 0.1f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = speed;
    }

    void Update()
    {
        transform.Translate(0, -currentSpeed * Time.deltaTime, 0, Space.World);

        currentSpeed += acceleration * Time.deltaTime;
    }
}
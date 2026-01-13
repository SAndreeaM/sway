using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Event handler for collision detection 
    // Destroys objects tagged as "Deadly" upon collision
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Deadly")) {
            Destroy(other.gameObject);
        }
    }
}

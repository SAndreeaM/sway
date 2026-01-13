using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float scrollSpeed = 0.5f;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float yOffset = transform.position.y * scrollSpeed;
        // Update the texture offset to create the scrolling effect
        meshRenderer.material.mainTextureOffset = new Vector2(0, yOffset);
    }
}
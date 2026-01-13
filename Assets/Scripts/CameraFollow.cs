using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;

    void Start()
    {

    }

    void Update()
    {
        if(followTarget != null)
        {
            transform.position = new Vector3(0, followTarget.position.y, transform.position.z);
        } else {
            return;
        }
    }
}
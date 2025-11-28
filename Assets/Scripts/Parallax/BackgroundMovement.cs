using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private float startPosition;
    private float length = 29.0659f;
    public GameObject _camera;
    public float parallaxEffect;

    private void Start()
    {
        startPosition = transform.position.x;
    }
    private void LateUpdate()
    {
        float distance = _camera.transform.position.x * parallaxEffect; // 0 = seguirá a la camara || 1= no se mueve
        float movement = _camera.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);
        if (movement > startPosition + length)
        {
            startPosition += length;
        }
        else if(movement < startPosition - length)
        { startPosition -= length; }
    }
}

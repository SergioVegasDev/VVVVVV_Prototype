using UnityEngine;

public class MoveBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    //private int _direction = 1;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void MoveCharacterHorizontal(Vector2 direction, float speed)
    {
        if (Mathf.Abs(direction.x) > 0) //Usamos Abs, para saber si hay movimeinto en X, nos da igual si es negativo o positivo.
        {
            _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocityY);
        }
        else
        {
            _rb.linearVelocityX = 0;
        }
    }
}

using UnityEngine;

public class ChangeGravityBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private LayerMask Ground;
    private float groundCheckDistance = 1.5f;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void ChangeGravity( ref bool gravityFlipped)
    { 
        gravityFlipped = !gravityFlipped;

        _rb.gravityScale *= -1f;

        transform.rotation = Quaternion.Euler(0f, 0f, gravityFlipped ? 180f : 0f);

        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundCheckDistance, Ground);
        return hit.collider != null;
    }
}

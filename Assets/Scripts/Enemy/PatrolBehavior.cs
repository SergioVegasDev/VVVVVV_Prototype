using UnityEngine;

public class PatrolBehavior : MonoBehaviour
{
    public float raycastDistance = 1f; 
    public LayerMask groundLayer;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void Patrol(Transform groundCheck, float speed)
    {
        if (!IsGrounded(groundCheck))
        {
            _rb.linearVelocity = new Vector2((speed* transform.localScale.x),_rb.linearVelocity.y);
        }   
    }
    public bool IsGrounded(Transform groundCheck)
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, -groundCheck.transform.up, raycastDistance, groundLayer);
        //Debug.DrawLine(groundCheck.position, groundCheck.position -groundCheck.transform.up * raycastDistance);
        return hit.collider != null;
    }
}
    
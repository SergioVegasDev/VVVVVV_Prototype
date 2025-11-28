using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _velocity;
    public Canon canon;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = _velocity;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.TryGetComponent<IDammageable>(out IDammageable iDmg) )
        {
            iDmg.Die();
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3)
        {
            GetComponent<Collider2D>().enabled = false;
            canon.Push(gameObject);
        }
    }
}

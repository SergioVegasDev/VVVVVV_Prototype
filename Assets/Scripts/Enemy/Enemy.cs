using UnityEngine;

[RequireComponent(typeof(PatrolBehavior))]
[RequireComponent(typeof(ChangeSpriteDirectionBehavior))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform groundCheck;
    protected PatrolBehavior _pb;
    protected ChangeSpriteDirectionBehavior _csdb;
    private float speed;
    const float speedMaxValue = 2f, speedMinValue = 0f;
    const int minRand = 1, maxRand = 4;
    private Rigidbody2D _rb;
    protected Animator _animator;
    private bool _canAttack = true;

    private void Awake()
    {
        speed = speedMaxValue;
        _pb = GetComponent<PatrolBehavior>();
        _csdb = GetComponent<ChangeSpriteDirectionBehavior>();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _rb.linearVelocity = transform.right * speed;
        InvokeStopOrContinue();
    }
    public void InvokeStopOrContinue()
    {
        float rand = Random.Range(minRand, maxRand);
        Invoke(nameof(StopOrContinue), rand);
    }

    void Update()
    {
        _pb.Patrol(groundCheck, speed);
        _animator.SetFloat("Velocity", Mathf.Abs(_rb.linearVelocityX));
        _csdb.ChangeSpriteDirection(_rb.linearVelocityX);
    }
    public void StopOrContinue()
    {
        speed = speed == speedMinValue ? speedMaxValue : speedMinValue;
        InvokeStopOrContinue();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IDammageable>(out IDammageable iDmg) && _canAttack)
        {
            iDmg.Die();
            _canAttack = false;
        }
    }
}

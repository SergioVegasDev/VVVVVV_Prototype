using UnityEngine;

public class Laser : MonoBehaviour
{
    private bool _canAttack = true;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<IDammageable>(out IDammageable iDmg) && _canAttack)
        {
            iDmg.Die();
            _canAttack = false;
        }
    }
    public void CanAttackAgain()
    {
        _canAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    bool _canHit = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
       
        if (_canHit == true)
        {
            if (hit != null)
            {
                hit.Damage();
                _canHit = false;
                StartCoroutine(CanHitRoutine());
            }
        }
    }

    IEnumerator CanHitRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canHit = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AcidEffact : MonoBehaviour
{
    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * 2.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Player")
        {
            IDamageable hit = other.GetComponent<IDamageable>();

            if (hit != null)
            { 
                hit.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}

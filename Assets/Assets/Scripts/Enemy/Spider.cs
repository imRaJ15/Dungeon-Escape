using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy,IDamageable
{
    [SerializeField]
    GameObject _acidePreFab;
    public int Health { get; set; }

    public override void InIt()
    {
        base.InIt();
        Health = base.health;
    }

    public void Damage()
    {
        if (isDead == true) return;

        Health--;
        isHit = true;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            anim.SetTrigger("Death");
            isDead = true;

            GameObject diamond = Instantiate(diamondPreFab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;
        }
    }

    public override void Movement()
    {
        
    }

    public void Attack()
    {
        Instantiate(_acidePreFab, transform.position, Quaternion.identity); 
    }
}

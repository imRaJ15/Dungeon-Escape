using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MossGaint : Enemy, IDamageable
{
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
        anim.SetTrigger("Hit");
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
}
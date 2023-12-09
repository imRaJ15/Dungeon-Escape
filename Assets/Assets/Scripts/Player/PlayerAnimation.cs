using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _anim;
    Animator _swordAnim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>(); 
        _swordAnim = transform.GetChild(1).GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move", Mathf.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }

    public void Dead()
    {
        _anim.SetTrigger("Dead");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    Spider _spider;

    private void Start()
    {
        _spider = GameObject.Find("Spider_Enemy").GetComponent<Spider>();
    }
    void Fire()
    {
        _spider.Attack();
    }
}

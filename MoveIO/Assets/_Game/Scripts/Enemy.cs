using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override void Update()
    {
        base.Update();
        if (isDead != false)
        {
            ChangeAnim(CacheString.ANIM_DIE);
        }
    }
}

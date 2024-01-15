using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheString : Singleton<CacheString>
{
    public const string LAYER_CHARACTER = "Character";
    public const string LAYER_ENEMY = "Enemy";
    public const string LAYER_PLAYER = "Player";

    public const string ANIM_IDLE = "idle";
    public const string ANIM_RUN = "run";
    public const string ANIM_ATTACK = "attack";
    public const string ANIM_DIE = "die";
}

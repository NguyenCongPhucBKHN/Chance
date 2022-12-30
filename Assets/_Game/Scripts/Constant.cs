using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant 
{
    public const float TIMER_BLOCK_ROTATION = 5f;
    public const float TIMER_RUN_ROTATION =2.15f;
    public const float TIMER_BLOCK_DASH =10;
    public const float TIMER_RUN_DASH = 0.7f;
    public const float TIMER_BLOCK_AOE = 15;
    public const float TIMER_RUN_AOE =2.5f;
    public const float TIMER_DEDAY_TO_AOE = 0.52f;
    public const float TIMER_DEDAY_TO_AOE_BOSS = 0.52f;


    public const string ANIM_LABEL_HIT_STEP="hitStep";
    public const string ANIM_TRIGGER_RUN ="Run";
    public const string ANIM_TRIGGER_IDLE ="Idle";
    public const string ANIM_TRIGGER_DIE ="Die";
    public const string ANIM_TRIGGER_ATTACK ="Attack";
    public const string ANIM_TRIGGER_WALK ="Walk";
    public const string ANIM_TRIGGER_DASH = "Dash";
    public const string ANIM_TRIGGER_ROTATION = "Skill2";
    public const string ANIM_TRIGGER_AOE= "Skill3";
    public const string ANIM_TRIGGER_HIT = "Hit";
    public const string ANIM_TRIGGER_RELOAD = "Reload";
    public const string ANIM_TRIGGER_DELAY = "Delay";
    

}

public enum EnemyType
{
    Melee,
    Range,
    Boss
}

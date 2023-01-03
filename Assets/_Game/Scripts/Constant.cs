using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constant 
{
    //TODO: SU DUNG SO THAY CONSTANT DE CO THE THAY DOI THEO LEVEL
    public const float RATE_LOOT_HP =0.005f;
    public const float TIMER_BLOCK_ROTATION = 5f;
    public const float TIMER_RUN_ROTATION =2.15f;
    public const float TIMER_BLOCK_DASH =0;
    public const float TIMER_RUN_DASH = 0.7f;
    public const float TIMER_BLOCK_AOE = 15;
    public const float TIMER_RUN_AOE =2.5f;
    public const float TIMER_DEDAY_TO_AOE = 0.52f;
    public const float TIMER_DEDAY_TO_AOE_BOSS = 1f;


    public static Vector2 TIMER_BOCK_ATT_MELEE = new Vector2 (3, 5);
    public static Vector2 TIMER_BOCK_ATT_RANGE= new Vector2 (3, 7);
     public static Vector2 TIMER_BOCK_ATT_BOSS= new Vector2 (3, 8);

     public static Vector2 TIMER_BOCK_IDLE_MELEE = new Vector2 (1, 3);
    public static Vector2 TIMER_BOCK_IDLE_RANGE= new Vector2 (1, 4);
     public static Vector2 TIMER_BOCK_IDLE_BOSS= new Vector2 (2, 5);

    public static Vector2 TIMER_BOCK_PARTROL_MELEE = new Vector2 (1, 3);
    public static Vector2 TIMER_BOCK_PARTROL_RANGE= new Vector2 (1, 4);
     public static Vector2 TIMER_BOCK_PARTROL_BOSS= new Vector2 (2, 5);


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

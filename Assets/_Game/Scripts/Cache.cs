using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, Character> characterParents = new Dictionary<Collider, Character>();
    private static Dictionary<Collider, IHitAttack> iHitAttacks = new Dictionary<Collider, IHitAttack>();
    private static Dictionary<Collider, IHitDash> iHitDashs = new Dictionary<Collider, IHitDash>();

    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    } 

    public static Character GetCharacterInParent(Collider collider)
    {
        if (!characterParents.ContainsKey(collider))
        {   
            if(collider.GetComponent<SubCharacter>()!=null)
            {
                characterParents.Add(collider, collider.GetComponent<SubCharacter>().parentCharacter);
            }
            else
            {
                return null;
            }
            
        }


        return characterParents[collider];
    }

    public static IHitAttack GetIHitAttackInParent(Collider collider)
    {
        if (!iHitAttacks.ContainsKey(collider))
        {

            iHitAttacks.Add(collider, collider.GetComponentInParent<IHitAttack>());
        }

        return iHitAttacks[collider];
    }

    public static IHitDash GetIHitDashInParent(Collider collider)
    {
        if (!iHitDashs.ContainsKey(collider))
        {

            iHitDashs.Add(collider, collider.GetComponentInParent<IHitDash>());
        }

        return iHitDashs[collider];
    }


}

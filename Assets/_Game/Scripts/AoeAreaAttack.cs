using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAreaAttack : MonoBehaviour
{
     [SerializeField] private int m_FirstDamageTake;
    [SerializeField] private int m_DurationDamageTake;
    private Dictionary<Character, Coroutine> m_Coroutines = new Dictionary<Character, Coroutine>();
    private void OnTriggerEnter(Collider other)
    {
         
        Character character = Cache.GetCharacterInParent(other);
        if (character!= null)
        {
            character.TakeDame(m_FirstDamageTake);
            if(m_Coroutines.ContainsKey(character))
            {
                m_Coroutines.Add(character, StartCoroutine(TakeDamageDuration(character)));
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Character character = Cache.GetCharacterInParent(other);
        if (character!= null)
        {
            StopCoroutine(m_Coroutines[character]);
            m_Coroutines.Remove(character);
        }
    }
    IEnumerator TakeDamageDuration(Character character)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            character.TakeDame(m_DurationDamageTake);
        }
    }
}

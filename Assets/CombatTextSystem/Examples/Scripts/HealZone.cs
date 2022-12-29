using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    [SerializeField] private int m_DurationHealValue;
    private Dictionary<Player, Coroutine> m_Coroutines = new Dictionary<Player, Coroutine>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            m_Coroutines.Add(player, StartCoroutine(HealDuration(player)));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            StopCoroutine(m_Coroutines[player]);
            m_Coroutines.Remove(player);
        }
    }
    IEnumerator HealDuration(Player player)
    {
        while (true)
        {
            // player.Heal(m_DurationHealValue);
            yield return new WaitForSeconds(1);
        }
    }
}

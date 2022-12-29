using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int m_FirstDamageTake;
    [SerializeField] private int m_DurationDamageTake;
    private Dictionary<Player, Coroutine> m_Coroutines = new Dictionary<Player, Coroutine>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            // player.TakeDamage(m_FirstDamageTake, true);
            m_Coroutines.Add(player, StartCoroutine(TakeDamageDuration(player)));
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
    IEnumerator TakeDamageDuration(Player player)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            // player.TakeDamage(m_DurationDamageTake, false);
        }
    }
}

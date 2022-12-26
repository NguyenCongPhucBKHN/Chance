using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints;
    public List<Transform> WayPoints => wayPoints;
    
}

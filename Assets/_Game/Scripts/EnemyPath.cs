using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;
    public Transform[] WayPoints => wayPoints;
}

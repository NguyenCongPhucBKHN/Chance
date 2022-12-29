using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthBarSystem;
public class TestHealthBar : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    private void Start()
    {
        healthBar.Init(100, 0, 100);
    }
    public void IncreaseHealth(float value)
    {
        healthBar.Value += value;
    }
    public void DecreaseHealth(float value)
    {
        healthBar.Value -= value;
    }
}

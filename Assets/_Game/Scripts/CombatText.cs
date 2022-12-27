using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatText : GameUnit
{
    [SerializeField] TextMesh hpText;
    [SerializeField] float speed;
    [SerializeField] private float timeLife;
    private Transform cameraTf;
    void Awake(){
        cameraTf = Camera.main.transform;
        tf = transform;
    }
    public void OnInit(float damage)
    {
        hpText.text = damage.ToString();
        tf.rotation = Quaternion.LookRotation(cameraTf.forward, cameraTf.up);
        Invoke(nameof(OnDespawn), timeLife);
    }
    private void Update() {
        tf.position += Time.deltaTime*Vector3.up *speed;
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}

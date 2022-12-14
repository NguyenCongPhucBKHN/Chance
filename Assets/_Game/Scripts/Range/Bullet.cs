using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    // [SerializeField] Transform tf;
    [SerializeField] float speed;
    [SerializeField] float timeLife;
    [SerializeField] float damge;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] GameObject hitBullet;
    [SerializeField] Hit hit;
    public Rigidbody rb;
    void Start()
    {
        tf = transform;
        // OnInit();
    }

    public void OnInit()
    {
        rb.velocity = tf.right * speed;
        // if(hitVFX.isPlaying)
        // {
        //     hitVFX.Stop();
        // }
        Invoke(nameof(OnDespawn), timeLife);
    }

    private void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        IHitBullet hitBullet = Cache.GetIHitBullet(other);
        if(hitBullet!= null)
        {
            hitBullet.OnHitBullet(tf, damge);
            OnDespawn();
        }
   
    }

}

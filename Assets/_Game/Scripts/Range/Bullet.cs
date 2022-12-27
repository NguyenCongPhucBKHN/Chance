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
        if(hit!=null)
        {
            hit.Despawn();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player!= null)
        {
            player.OnHitAttack(damge);
            hit = SimplePool.Spawn<Hit>(PoolType.HitBullet, tf.position, tf.rotation);
            // Instantiate(hitBullet, tf.position, tf.rotation);
            OnDespawn();
        }
    }
}

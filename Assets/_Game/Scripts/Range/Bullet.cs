using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Transform tf;
    [SerializeField] float speed;
    [SerializeField] float timeLife;
    [SerializeField] float damge;
    [SerializeField] ParticleSystem hitVFX;
    [SerializeField] GameObject hitBullet;
    public Rigidbody rb;
    void Start()
    {
        OnInit();
    }

    private void OnInit()
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
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if(player!= null)
        {
            player.OnHitAttack(damge);
            Instantiate(hitBullet, tf.position, tf.rotation);
            OnDespawn();
        }
    }
}

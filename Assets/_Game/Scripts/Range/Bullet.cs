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
    public Rigidbody rb;
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        rb.velocity = tf.right * speed;
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
            player.takeDame(damge);
            OnDespawn();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = LevelManager.Instance.player;
    }

    void Update()
    {
        Vector3 arrow = transform.position - player.tf.position;
        arrow.y =0;
        player.arrowTF.rotation = Quaternion.LookRotation(arrow, Vector3.up);
    }

    public void Deactivate()
    {
        gameObject.SetActive(true);
    }
    public void Activate()
    {
        gameObject.SetActive(false);
    }
}

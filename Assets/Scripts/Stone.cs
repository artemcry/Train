using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Resource
{
    private void Start()
    {
        type = Type.Stone;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        Player p = collision.gameObject.GetComponent<Player>();
        p.resources[type]++;
        Destroy(gameObject);
    }
}

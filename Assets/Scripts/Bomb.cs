using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (other.GetComponent<PlayerController>())
        {
            player.HurtPlayer();

            Destroy(gameObject);
        }
    }
}

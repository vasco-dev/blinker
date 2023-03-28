using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectible
{
    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlayerController>(out PlayerController _player);
        if (_player != null)
        {
            _player.HurtPlayer();

            Destroy(gameObject);
        }
    }
}

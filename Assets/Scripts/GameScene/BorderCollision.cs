using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private CollectibleManager _collectibleManager;


    private void OnTriggerEnter(Collider other)
    {
        PlayerController _player = other.GetComponent<PlayerController>();

        if (_player != null)
        {
            _player.HurtPlayer();
        }

        Collectible _collectible = other.GetComponent<Collectible>();

        if (_collectible != null)
        {
            Destroy(_collectible);
        }
    }
}

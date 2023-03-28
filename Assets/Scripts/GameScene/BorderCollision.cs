using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private CollectibleManager _collectibleManager;


    private void OnTriggerEnter(Collider other)
    {

        other.TryGetComponent<Collectible>(out Collectible _collectible);
        if (_collectible != null)
        {
            _collectible.gameObject.SetActive(false);
            Destroy(_collectible.gameObject);
        }

        other.TryGetComponent<PlayerController>(out PlayerController _player);
        if (_player != null)
        {
            _player.HurtPlayer();
        }
    }
}

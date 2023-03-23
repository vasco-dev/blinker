using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        /*PlayerMovement _player = other.GetComponent<PlayerMovement>();

        if (_player != null)
        {
            _player.transform.position = new Vector3(0, 0, 0);
            __gameover = true;
        }

        Collectible _collectible = other.GetComponent<Collectible>();

        if (_collectible != null)
        {
            Destroy(_collectible.GameObject);
            __gameover = true;
        }*/
    }
}

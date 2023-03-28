using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] public float speed;

    [SerializeField] public int scoreValue;

    public Rigidbody Body { get; private set; }

    void Awake()
    {
        //set rigidbody and throw error if there is none
        Body = GetComponent<Rigidbody>();

        if (Body == null)
        {
            Debug.LogError("NO RIGID BODY");
        }
    }

    private void OnDestroy()
    {
        CollectibleManager.Instance.RemoveCollectible(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<PlayerController>(out PlayerController _player);
        if (_player != null)
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

}

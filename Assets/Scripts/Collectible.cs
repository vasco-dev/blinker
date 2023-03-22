using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] public float speed;

    [SerializeField] public int scoreValue;
    public Rigidbody Body { get; private set;}

    // Start is called before the first frame update
    void Awake()
    {
        //set rigidbody and throw error if there is none
        Body = GetComponent<Rigidbody>();

        if(Body == null){
            Debug.LogError("NO RIGID BODY");
        }
    }
    private void OnDestroy()
    {
        CollectibleManager.Instance.RemoveCollectible(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(other.GetComponent<PlayerController>())
        {
            GameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int _hp = 1;

    public Rigidbody Body { get; private set; } = null;

    private Collectible _targetCollectible = null;

    private Touch _touch;

    private float _shortestDistance = 1000000000f;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);

            if (_touch.phase == TouchPhase.Began)
            {
                GetClosestObj();
            }
        }
    }


    private void GetClosestObj()
    {
        float radiusScale = 0.1f;

        Collider[] colliders = { };

        while (_targetCollectible == null && radiusScale < 1000f)
        {
            colliders = Physics.OverlapSphere(transform.position, radiusScale, Physics.AllLayers, QueryTriggerInteraction.UseGlobal);
            
            foreach(Collider obj in colliders)
            {
                float localDistance = (obj.transform.position - transform.position).magnitude;

                if (localDistance < _shortestDistance)
                {
                    _shortestDistance = localDistance;
                    _targetCollectible = obj.GetComponent<Collectible>();
                }
            }
            radiusScale += 0.1f;
        }

        if (_targetCollectible != null)
        {
            transform.position = _targetCollectible.transform.position;

            Body.velocity = _targetCollectible.Body.velocity;

            _targetCollectible = null;
        }
        else
        {
            Debug.Log(" obj null wtf");


        }
    }

}

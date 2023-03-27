using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _particles;

    [SerializeField] private int _maxHP = 1;    
    public Rigidbody Body { get; private set; } = null;

    private PlayerInputActions _input;


    private Collectible _targetCollectible = null;

    private void Awake()
    {
        Body = GetComponent<Rigidbody>();

        if (_input == null){
            _input = new PlayerInputActions();

            Debug.Log("" + _input);
        }  
    }
    private void Start()
    {
        _input.Enable();
        _input.Touch.Tap.started += PlayerTap;
    }
    private void FixedUpdate()
    {
        GetClosestObj();
    }

    public void PlayerTap(InputAction.CallbackContext obj)
    {
        BlinkToObject();
        _targetCollectible = null;
    }


    public void HurtPlayer()
    {
        --_maxHP; 
        if (_maxHP <= 0)
        {
            transform.position = CollectibleManager.Instance._centerPoint.position;
            Body.velocity = Vector3.zero;

            GameManager.Instance.RestartLevel();
        }
    }


    private void BlinkToObject()
    {
        if (_targetCollectible != null)
        {
            transform.position = _targetCollectible.transform.position;

            Body.velocity = _targetCollectible.GetComponent<Rigidbody>().velocity;

            _targetCollectible = null;
        }
        else
        {
            Debug.Log(" no collectible found ");
        }
    }

    private void GetClosestObj()
    {
        if (CollectibleManager.Instance.ListCollectibles.Count > 0)
        {
            _targetCollectible = null;

            float radiusScale = 0.1f;
            float _shortestDistance = 1000f;

            Collider[] colliders = { };

            while (_targetCollectible == null && radiusScale < 50f)
            {
                colliders = Physics.OverlapSphere(transform.position, radiusScale, Physics.AllLayers, QueryTriggerInteraction.UseGlobal);

                foreach (Collider obj in colliders)
                {
                    Collectible isCollectible = obj.GetComponent<Collectible>();
                    if (isCollectible)
                    {
                        //Debug.Log(" found collectible ");


                        float localDistance = (obj.transform.position - transform.position).magnitude;

                        if (localDistance <= _shortestDistance)
                        {
                            _shortestDistance = localDistance;
                            _targetCollectible = isCollectible;

                            _particles.transform.position = _targetCollectible.transform.position + Vector3.down;
                            _particles.GetComponent<MeshFilter>().mesh = _targetCollectible.GetComponent<MeshFilter>().mesh;
                            
                            //#if UNITY_EDITOR
                            //    Debug.Log("found shortest distance");
                            //#endif
                        }
                    }
                }
                if (_targetCollectible)
                {
                    _particles.transform.position = _targetCollectible.transform.position + Vector3.down;
                }
                else
                {
                    _particles.transform.position = transform.position;
                    _particles.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
                }
                radiusScale += 0.5f;
            }
        }
        else
        {
            _particles.transform.position = transform.position;
            _particles.GetComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject collectible = other.gameObject;
        if (collectible.layer == LayerMask.NameToLayer("Collectible"))
        {
            GameManager.Instance.AddScore(10);
            Destroy(collectible);
        }
    }



}

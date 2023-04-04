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

            //Debug.Log("" + _input);
        }  
    }
    private void Start()
    {
        _input.Enable();
        _input.Touch.Tap.started += PlayerTap;
    }
    private void Update()
    {
        GetClosestObj();
    }
    private void LateUpdate()
    {
        if (_targetCollectible != null){
            _particles.transform.position = _targetCollectible.transform.position + Vector3.down;
        }
        else{
            _particles.transform.position = transform.position + Vector3.down;
        }
    }

    public void PlayerTap(InputAction.CallbackContext obj)
    {
        Debug.Log("TAPPED");
        BlinkToObject();
        _targetCollectible = null;
    }


    public void HurtPlayer()
    {
        --_maxHP; 
        if (_maxHP <= 0)
        {
            _maxHP = 1;

            transform.position = CollectibleManager.Instance._centerPoint.position;
            Body.velocity = Vector3.zero;

            GameManager.Instance.RestartGame();
        }
    }


    private void BlinkToObject()
    {
        Debug.Log("OBJ: " + _targetCollectible.name);
        if (_targetCollectible != null)
        {
            Body.velocity = Vector3.zero;

            Body.velocity = _targetCollectible.Body.velocity;

            transform.position = _targetCollectible.transform.position;

            Debug.Log("TELEPORTED");

        }
    }

    private void GetClosestObj()
    {
        if (CollectibleManager.Instance.ListCollectibles.Count > 0)
        {
            _targetCollectible = null;

            float radiusScale = 0.1f;
            float _shortestDistance = 1000f;

            Collider[] colliders;

            while (_targetCollectible == null && radiusScale < 50f)
            {
                colliders = Physics.OverlapSphere(transform.position, radiusScale, Physics.AllLayers, QueryTriggerInteraction.UseGlobal);

                foreach (Collider obj in colliders)
                {
                    obj.TryGetComponent<Collectible>(out Collectible isCollectible);
                    if (isCollectible != null)
                    {
                        //Debug.Log(" found collectible ");

                        float localDistance = (obj.transform.position - transform.position).magnitude;

                        if (localDistance <= _shortestDistance)
                        {
                            _shortestDistance = localDistance;
                            _targetCollectible = isCollectible;
                        }
                    }
                }
                radiusScale += 0.5f;
            }
        }
    }

}

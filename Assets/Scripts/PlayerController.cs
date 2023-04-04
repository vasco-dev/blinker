using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _maxHP = 1;    
    public Rigidbody Body { get; private set; } = null;

    private PlayerInputActions _input;

    private int _targetNumber = 0;

    private Collectible _closestCollectible = null;
    private Collectible _nextCollectible = null;

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
        UpdateFeedback();
    }

    public void PlayerTap(InputAction.CallbackContext obj)
    {
        BlinkToObject();
        _closestCollectible = null;
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
        if (_closestCollectible != null)
        {
            Body.velocity = Vector3.zero;

            Body.velocity = _closestCollectible.Body.velocity;

            transform.position = _closestCollectible.transform.position;

            Debug.Log("TELEPORTED");

        }
    }

    private void GetClosestObj()
    {
        if (CollectibleManager.Instance.ListCollectibles.Count > 0)
        {
            _closestCollectible = null;
            _nextCollectible = null;

            float radiusScale = 0.1f;

            Collider[] colliders;

            while (_targetNumber < 2 && radiusScale < 50f)
            {
                colliders = Physics.OverlapSphere(transform.position, radiusScale, Physics.AllLayers, QueryTriggerInteraction.UseGlobal);

                foreach (Collider obj in colliders)
                {
                    obj.TryGetComponent<Collectible>(out Collectible checkCollectible);

                    if (checkCollectible != null)
                    {
                        if (_closestCollectible == null)
                        { 
                            _closestCollectible = checkCollectible;
                            ++_targetNumber;
                        } 
                        else if(checkCollectible != _closestCollectible)
                        { 
                            _nextCollectible = checkCollectible;
                            ++_targetNumber;
                        }
                    }
                }
                radiusScale += 0.5f;
            }

            _targetNumber = 0;
        }
    }

    private void UpdateFeedback()
    {
        if (_closestCollectible != null)
        {
            CollectibleManager.Instance.SetClosestObj(_closestCollectible.transform);
        }
        else
        {
            CollectibleManager.Instance.SetClosestObj(transform);
        }
        if (_nextCollectible != null)
        {
            CollectibleManager.Instance.SetNextObj(_nextCollectible.transform);
        }
        else
        {
            CollectibleManager.Instance.SetNextObj(transform);
        }
    }

}

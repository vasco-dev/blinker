using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //how many times can the player get hit
    [SerializeField] private int _maxHP = 1;  
    
    //cooldown after 1 tap, IN SECONDS
    [SerializeField] private float _tapCooldown = 0.2f;    
    private bool _isOnCooldown = false;    
    
    //reference for the player's body
    public Rigidbody Body { get; private set; } = null;

    //input actions
    private PlayerInputActions _input;

    //references for the collectible targets
    private Collectible _closestCollectible = null;
    private Collectible _nextCollectible = null;

    //number of targets aquired
    private int _targetNumber = 0;



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
        if (_closestCollectible != null && !_isOnCooldown)
        {
            Body.velocity = Vector3.zero;

            Body.velocity = _closestCollectible.Body.velocity;

            transform.position = _closestCollectible.transform.position;


            StartCoroutine(HandleCooldown());
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
    private IEnumerator HandleCooldown()
    {
        while(!_isOnCooldown)
        {
            _isOnCooldown = true;
            yield return new WaitForSeconds(_tapCooldown);
        }

        _isOnCooldown= false;
    }

}

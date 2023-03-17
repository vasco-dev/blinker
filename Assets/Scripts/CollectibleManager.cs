using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [Header("DIFFERENT COLLECTIBLE PREFABS")]
    
    [SerializeField] private Collectible _prefabCollectibleA;
    [SerializeField] private Collectible _prefabCollectibleB;
    [SerializeField] private Collectible _prefabCollectibleC;
    [SerializeField] private Collectible _prefabCollectibleD;
    [SerializeField] private Collectible _prefabBomb;

     [Space(20)]

    [Header("SPAWNER AND CENTER TRANSFORMS")]
    [Min(1)]
    [SerializeField] private List<Transform> listSpawners = new List<Transform>();
    [SerializeField] private Transform _centerPoint;

     [Space(20)]

    [Header("SPAWNING")]
    

    [SerializeField] private int spawnRandOffsetScale = 500;

    /// <summary>
    /// number of subdivisons per tick, how many times the spawners are gonna spawn in a single tick 
    /// </summary>
    [Min(1)]
    [SerializeField] private int subTicks = 1;

    /// <summary>
    /// duration of each tick, in seconds
    /// </summary>
    [Min(1)]
    [SerializeField] private float _tickDuration = 3f;

     [Space(20)]

    private float _currentTimeInTick = 0f;

    private List<Collectible> listCollectibles = new List<Collectible>();
    private int _maxSizeCollectiblePool = 20;
    public static CollectibleManager Instance { get; private set; }
    private void Awake()
    {
        //siingleton
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
        }

        //throw error message if the list of spawners is empty
        if(listSpawners.Count <= 0){
            Debug.LogError("NO SPAWNERS!");
        }

        //if no center point is referenced, assume 0,0,0
        if(_centerPoint == null){
            _centerPoint.position= Vector3.zero;
        }
    }

    private void Start(){
        StartCoroutine(SpawnTimer());
    }
    private void OnDisable(){
        StopAllCoroutines();
    }

    public void RemoveCollectible(Collectible collectibleToRemove){
        listCollectibles.Remove(collectibleToRemove);
    }

    private void SpawnCollectible(int spawner)
    {
        //instantiate new collectible
        Collectible spawnedCollectible = Instantiate(_prefabCollectibleB);

        //randomize the spawner and go to it's position
        spawnedCollectible.transform.position = listSpawners[spawner].transform.position;

        //get the slightly randomized and normalized direction of the movement and then multiply by the preset speed of the specific collectible
        int randX = UnityEngine.Random.Range(-spawnRandOffsetScale, spawnRandOffsetScale+1);
        int randY = UnityEngine.Random.Range(-spawnRandOffsetScale, spawnRandOffsetScale+1);
        Vector3 randPos = new Vector3(randX,0, randY);
        randPos *= 0.01f;
        randPos += _centerPoint.position;

        Vector3 vel = (randPos - spawnedCollectible.transform.position).normalized;

        vel *= spawnedCollectible.speed;

        //set the rigidbody velocity
        spawnedCollectible.Body.velocity = vel;

        if(listCollectibles.Count >= _maxSizeCollectiblePool) {
            Destroy(listCollectibles[0]);
            listCollectibles.RemoveAt(0);
        }

        listCollectibles.Add(spawnedCollectible);
    }
    /// <summary>
    /// Shuffles the order of all the spawners in listSpawners
    /// </summary>   
    private void ShuffleSpawnerList()
    {
        for (int i = 0; i < listSpawners.Count; i++)
        {
            Transform spawner = listSpawners[i];

            int randIndex = UnityEngine.Random.Range(i, listSpawners.Count);
            
            listSpawners[i] = listSpawners[randIndex];
            
            listSpawners[randIndex] = spawner;
        }
    }



    /// <summary>
    /// Coroutine that times the spawn according to design presets
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnTimer()
    {
        while(GameManager.Instance.IsRunning){
            
            
            //per spawn tick (1s)

            //Shuffles all spawners to have a random order
            ShuffleSpawnerList();

            for (int i=0; i< listSpawners.Count; ++i) {

                //make range larger for Random.Range
                float maxSpawnRange = (float)subTicks / listSpawners.Count;
                maxSpawnRange *= 1000f;

                float rand = UnityEngine.Random.Range(_currentTimeInTick+1, maxSpawnRange);
                

                //divide by 1000 to convert back into seconds
                rand *= 0.001f;

                //set current spawn tick
                _currentTimeInTick += rand;
                
                SpawnCollectible(i);
                Debug.Log("collectible spawned \n time left in tick: " +( _tickDuration - _currentTimeInTick));

                yield return new WaitForSeconds(rand);
                
               
            }

            float tickLeftOvers = _tickDuration - _currentTimeInTick;

            Debug.Log("ALL COLLECTIBLES SPAWNED \n LEFTOVER TIME: " + tickLeftOvers);

            if (tickLeftOvers > 0){
                yield return new WaitForSeconds(tickLeftOvers);
                _currentTimeInTick= 0;
            }
            else{
                Debug.LogError("SPAWNED OVERTIME");
            }

        }
        yield return null;
    }




}

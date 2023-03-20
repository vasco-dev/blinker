using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    [Space(20)]

    [Header("SPAWNER AND CENTER TRANSFORMS")]
    [Min(1)]
    [SerializeField] private List<Transform> listSpawners = new List<Transform>();
    [SerializeField] private Transform _centerPoint;

    [Space(20)]

    [Header("SPAWNING")]
    /// <summary>
    /// maximum deviation (offset) from center position
    /// </summary>
    [SerializeField] private int spawnRandOffsetScale = 500;

    /// <summary>
    /// duration of each tick, in seconds
    /// </summary>
    private float _currentTickTime = 3f;

    /// <summary>
    /// number of subdivisons per tick, how many times the spawners are gonna spawn in a single tick 
    /// </summary>
    private int _currentSubTickRate = 1;  

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

    public void UpdateLevelData()
    {
        _currentTickTime = GameManager.Instance.CurrentLevelData.TickTime;
        _currentSubTickRate = GameManager.Instance.CurrentLevelData.SubtickRate;
        Debug.Log("Upped Level");

    }

    /// <summary>
    /// Spawn a collectible from a given Spawner
    /// </summary>   
    /// 
    private void SpawnCollectible(int spawner)
    {
        Collectible randomPrefabToSpawn = GetRandomCollectible();

        //instantiate new collectible in the spawner's position
        Collectible spawnedCollectible = Instantiate(randomPrefabToSpawn, listSpawners[spawner].transform.position, Quaternion.identity, transform);


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
    /// Randomizes which Collectible to spawn based on chances from current LevelData
    /// </summary>
    /// <returns>Random Collectible</returns>
    private Collectible GetRandomCollectible()
    {
        LevelData levelData = GameManager.Instance.CurrentLevelData;
        int collectibleIndex = 0;

        int rand = UnityEngine.Random.Range(0, 100);

        for(int i = 0; i < levelData.CollectibleChance.Length; ++i)
        {
            int currentRange = 0;
            int j = -1;

            do
            {
                ++j;
                currentRange += levelData.CollectibleChance[j];

                if (rand <= currentRange) { 
                    collectibleIndex = i;
                    return levelData.CollectiblePrefabs[collectibleIndex].GetComponent<Collectible>();
                }

            } while (j<i);

            //Debug.Log(rand);            
            //Debug.Log(currentRange);            

        }               

        return levelData.CollectiblePrefabs[collectibleIndex].GetComponent<Collectible>();
    }


    /// <summary>
    /// Remove a Collectible from the pool
    /// </summary>   
    public void RemoveCollectible(Collectible collectibleToRemove)
    {
        listCollectibles.Remove(collectibleToRemove);
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
                float maxSpawnRange = (float)_currentSubTickRate / listSpawners.Count;
                maxSpawnRange *= 1000f;

                float rand = UnityEngine.Random.Range(_currentTimeInTick+1, maxSpawnRange);
                

                //divide by 1000 to convert back into seconds
                rand *= 0.001f;

                //set current spawn tick
                _currentTimeInTick += rand;
                
                SpawnCollectible(i);
                //Debug.Log("collectible spawned \n time left in tick: " +( _tickDuration - _currentTimeInTick));

                yield return new WaitForSeconds(rand);
                
               
            }

            float tickLeftOvers = _currentTickTime - _currentTimeInTick;

            //Debug.Log("ALL COLLECTIBLES SPAWNED \n LEFTOVER TIME: " + tickLeftOvers);

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

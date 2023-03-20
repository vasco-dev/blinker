using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

[CreateAssetMenu()]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// Difficulty index:
    /// 0 - Easy
    /// 1 - Medium
    /// 2 - Hard
    /// </summary>
    public int Difficulty = 0;

    /// <summary>
    /// Time when this level portion starts, in seconds
    /// </summary>
    public int StartTime = 0;

    /// <summary>
    /// Time when this level portion ends, in seconds
    /// </summary>
    public float EndTime = 10f;

    /// <summary>
    /// Duration of a single tick, in seconds
    /// </summary>
    public float TickTime = 3f;

    /// <summary>
    /// how many subticks (spawns from each spawner) in a single tick
    /// </summary>
    public int SubtickRate = 1;

    /// <summary>
    /// world speed multiplier
    /// </summary>
    public float SpeedMultipier = 1f;

    /// <summary>
    /// collectible score multiplier
    /// </summary>
    public float ScoreMultipier = 1f;

    /// <summary>
    /// Prefabs for each type of collectible:
    ///[0] = Collectible A
    ///[1] = Collectible B
    ///[2] = Collectible C
    ///[3] = Collectible D
    ///[4] = Bomb
    /// </summary>
    [NativeFixedLength(5)]
    public GameObject[] CollectiblePrefabs;

    /// <summary>
    /// Chances of each type of collectible spawning:
    ///[0] = Collectible A
    ///[1] = Collectible B
    ///[2] = Collectible C
    ///[3] = Collectible D
    ///[4] = Bomb
    /// </summary>
    [NativeFixedLength(5)]
    public int[] CollectibleChance = {60, 30, 5, 0, 5} ;
    
}

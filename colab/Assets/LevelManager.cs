using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int rooms;
    public int maxRooms;
    public float difficulty;

    public GameObject[] roomPrefabs;
    public GameObject[] corridorPrefabs;

    public List<GameObject> spawnedRooms;
    public List<GameObject> spawnedCorridors;

    private void Awake()
    {
        Instance = this;
    }
}

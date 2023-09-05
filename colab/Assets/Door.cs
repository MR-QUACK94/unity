using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool spawnRoom;
    public bool spawnCorridor;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager lm = LevelManager.Instance;
        GameObject[] rooms = lm.roomPrefabs;
        GameObject[] corridors = lm.corridorPrefabs;
        if (lm.rooms < lm.maxRooms)
        {
            if(spawnRoom)
            {
                bool spawned = false;
                while (!spawned)
                {
                    bool canSpawn = true;
                    foreach (GameObject room in lm.spawnedRooms)
                    {
                        if (transform.position + (transform.forward * 5f) == room.transform.position)
                        {
                            canSpawn = false;
                        }
                    }
                    if (canSpawn)
                    {
                        int randItem = Random.Range(0, rooms.Length);
                        GameObject spawnedRoom = Instantiate(rooms[randItem], transform.position + (transform.forward * 5f), transform.rotation);
                        lm.rooms += 1;
                        lm.spawnedRooms.Add(spawnedRoom);
                        spawned = true;
                    }
                    else
                    {
                        Debug.Log("Space Taken");
                    }
                }   
            }
            if (spawnCorridor)
            {
                int randItem = Random.Range(0, corridors.Length - 1);
                GameObject spawnedCorridor = Instantiate(corridors[randItem], transform.position, transform.rotation);
                lm.spawnedCorridors.Add(spawnedCorridor);
            }
        }
    }
}

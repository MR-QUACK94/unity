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
        Spawn();
    }

    public void Spawn()
    {
        LevelManager lm = LevelManager.Instance;
        GameObject[] rooms = lm.roomPrefabs;
        GameObject[] corridors = lm.corridorPrefabs;
        if (lm.rooms < lm.maxRooms)
        {
            if (spawnRoom)
            {
                //Debug.Log("rooms at start: " + lm.spawnedRooms.Count);
                bool canSpawn = true;
                foreach (GameObject room in lm.spawnedRooms)
                {
                    Vector3 roundPos = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), Mathf.Round(transform.position.z));
                    Vector3 roomRoundPos = new Vector3(Mathf.Round(room.transform.position.x), Mathf.Round(room.transform.position.y), Mathf.Round(room.transform.position.z));
                    Debug.Log((roundPos + (transform.forward * 5f)) + " || " + roomRoundPos);
                    if (roundPos + (transform.forward.normalized * 5f) == roomRoundPos)
                    {
                        Debug.Log("Room cannot spawn");
                        canSpawn = false;
                    }
                }
                if (canSpawn)
                {
                    int randItem = Random.Range(0, rooms.Length);
                    GameObject spawnedRoom = Instantiate(rooms[randItem], transform.position + (transform.forward * 5f), transform.rotation);
                    spawnedRoom.transform.position = new Vector3(Mathf.Round(spawnedRoom.transform.position.x), Mathf.Round(spawnedRoom.transform.position.y), Mathf.Round(spawnedRoom.transform.position.z));
                    lm.rooms += 1;
                    lm.spawnedRooms.Add(spawnedRoom);
                }
                else if (!canSpawn)
                {
                    //Debug.Log("Space Taken");
                    GameObject lastRoom = lm.spawnedRooms[lm.spawnedRooms.Count-1];
                    GameObject lastCorridor = lm.spawnedCorridors[lm.spawnedCorridors.Count - 1];
                    Destroy(lastRoom);
                    Destroy(lastCorridor);
                    //Debug.Log("Destroyed Last Room and Corridor");
                }
                //Debug.Log("rooms at end: " + lm.spawnedRooms.Count);
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

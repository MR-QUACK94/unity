using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public GameObject[] corridorPrefabs;
    public int maxRooms;
    public GameObject startRoom;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> corridors = new List<GameObject>();
        List<GameObject> doors = new List<GameObject>();
        List<GameObject> rooms = new List<GameObject>();

        GameObject room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length - 1)], Vector3.zero, Quaternion.identity);
        doors = room.GetComponent<Room>().doors.ToList<GameObject>();
        rooms.Add(room);

        for (int i = 0; i < maxRooms;)
        {
            foreach (GameObject corridor in corridors)
            {
                GameObject door = corridor.transform.GetComponent<Room>().doors[0];
                bool canSpawn = true;
                foreach(GameObject _room in rooms)
                {
                    if(door.transform.position == _room.transform.position)
                    {
                        canSpawn = false;
                    }
                }
                if(canSpawn == true)
                {
                    room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length - 1)], door.transform.position, door.transform.rotation);
                    doors = room.GetComponent<Room>().doors.ToList<GameObject>();
                    rooms.Add(room);
                    i += 1;
                }
            }

            if (i < maxRooms - 1)
            {
                Debug.Log(doors.Count);
                foreach (GameObject door in doors)
                {
                    Debug.Log(door.transform.name);
                    GameObject corridor = Instantiate(corridorPrefabs[Random.Range(0, corridorPrefabs.Length - 1)], door.transform.position, door.transform.rotation);
                    corridors.Add(corridor);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class SpawnerObject
{
    public GameObject gameObject;
    public int amount;
}

public enum SpawnerType
{
    Enemy,
    Item
}

public class Spawner : MonoBehaviour
{
    //The object to spawn plus the amount
    public SpawnerObject[] objectsToSpawn;

    //The rate at which to spawn in seconds
    public float spawnRate = 1f;
    public SpawnerType type;

    void Start()
    {
        Invoke("Spawn", spawnRate);
    }

    void Spawn()
    {
        if (GameController.Instance.spawnerActive != type)
        {
            Invoke("Spawn", spawnRate);
            return;
        }

        //take a random object from the dictionary
        var randomIndex = UnityEngine.Random.Range(0, objectsToSpawn.Length);
        var randomEntry = objectsToSpawn[randomIndex];

        //spawn the object
        Instantiate(randomEntry.gameObject, transform.position, transform.rotation);

        //subtract one from the amount of objects to spawn
        randomEntry.amount--;

        //if there are no more objects to spawn, remove it from the dictionary
        if (randomEntry.amount == 0)
        {
            objectsToSpawn = objectsToSpawn.Where((source, index) => index != randomIndex).ToArray();
        }

        //if there are still objects to spawn, spawn another one
        if (objectsToSpawn.Length > 0)
        {
            Invoke("Spawn", spawnRate);
        }
    }
}

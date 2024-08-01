using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn_Script : MonoBehaviour
{
    [SerializeField]
    [Header("Game State")]
    private bool isGameContinues = true; 
    
    private Collider spawnRegion;
    private Coroutine spawnerCoroutine;

    [Header("Fruit Prefab Array")]
    public GameObject[] fruitPrefabs; 

    [Header("Despawn Settings")]
    public float despawnTime = 6f;

    [Header("Spawn Force Settings")]
    public float minForce = 20f;
    public float maxForce = 25f;

    [Header("Spawn Force Angle Settings")]
    public float minAngle = -10f;
    public float maxAngle = 10f;

    [Header("Spawn Delay Settings")]
    public float minSpawnDelay = 0.5f;
    public float maxSpawnDelay = 1.5f;

    private void Awake()
    {
        spawnRegion = GetComponent<Collider>();  
    }

    private void OnEnable()
    {
        StartSpawning();
    }

    private void OnDisable()
    {
        StopSpawning();
    }

    private void StartSpawning()
    {
        if(spawnerCoroutine != null) //Cheks if the coroutine is already working 
        {
            StopCoroutine(spawnerCoroutine); //Stops coroutine
        }

        spawnerCoroutine = StartCoroutine(Spawner()); //Starts coroutine and keeps the reference
    }

    private void StopSpawning()
    {
        if (spawnerCoroutine != null) //Cheks if the coroutine is already working 
        {
            StopCoroutine(spawnerCoroutine); //Stops coroutine
            spawnerCoroutine = null; //Clears the coroutine reference
        }
    }

    private IEnumerator Spawner()
    {
        while(isGameContinues)
        {
            GameObject prefab = GetRandomPrefab();
            Vector3 position = GetRandomPosition();
            Quaternion rotation = GetRandomRotation();

            GameObject fruit = Instantiate(prefab, position, rotation);
            Destroy(fruit, despawnTime);

            float force = GetRandomForce(); 
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(GetRandomSpawnDelay());

        }
    }

    private GameObject GetRandomPrefab()
    {
        if (fruitPrefabs.Length == 0)
        {
            Debug.LogError("The fruitPrefabs array is empty. Please add items to the array.");
            return null; 
        }

        return fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
        
    }       

    private Vector3 GetRandomPosition()
    {
        return new Vector3
        {
            x = Random.Range(spawnRegion.bounds.min.x, spawnRegion.bounds.max.x),
            y = Random.Range(spawnRegion.bounds.min.y, spawnRegion.bounds.max.y),
            z = Random.Range(spawnRegion.bounds.min.z, spawnRegion.bounds.max.z),
        };
    }


    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0f, 0f, Random.Range(minAngle, maxAngle));

    }

    private float GetRandomForce()
    {
        return Random.Range(minForce, maxForce);
    }

    private float GetRandomSpawnDelay()
    {
        return Random.Range(minSpawnDelay, maxSpawnDelay);
    }

    public void PauseSpawning()
    {
        isGameContinues = false;
        StopSpawning();
    }

    public void ResumeSpawning()
    {
        if (!isGameContinues)
        {
            isGameContinues = true;
            StartSpawning();
        }
    }

}

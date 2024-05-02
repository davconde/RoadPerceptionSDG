using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.GroundTruth.LabelManagement;

public class Spawner : MonoBehaviour
{
    // Public variables
    public float minSpawnRate = 2f ; // In seconds
    public float maxSpawnRate = 4f ; // In seconds
    public float resetPoolTime = 20f ; // In seconds

    // Private variables
    private LineRenderer lineRenderer;
    private List<List<GameObject>> objectPools; // List of subpools for each category
    private List<float> weights;
    private float spawnTimer = 0f;
    private float spawnRate = 0f;
    private float resetPoolTimer = 0f;

    public GameObject GetObjectFromPool(int categoryIndex)
    {
        List<GameObject> subpool = objectPools[categoryIndex];
        if (subpool.Count > 0)
        {
            GameObject obj = subpool[0];
            subpool.RemoveAt(0);
            // Reset object position and rotation
            obj.transform.position = lineRenderer.GetPosition(0);
            obj.transform.rotation = Quaternion.LookRotation(lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));
            obj.SetActive(true);
            return obj;
        }
        else
        {
            // Get random prefab from the list
            List<GameObject> prefabList = GameController.Instance.prefabLists[categoryIndex];
            int prefabIdx = Random.Range(0, prefabList.Count);
            // Get object rotation based on the path
            Quaternion objRotation = Quaternion.LookRotation(lineRenderer.GetPosition(1) - lineRenderer.GetPosition(0));
            // If the subpool is empty, instantiate a new object
            GameObject newObj = Instantiate(prefabList[prefabIdx],
                                            lineRenderer.GetPosition(0),
                                            objRotation,
                                            transform);
            newObj.GetComponent<ForegroundObject>().spawnerReference = this;
            newObj.GetComponent<ForegroundObject>().classID = categoryIndex;
            newObj.GetComponent<Labeling>().labels.Add(GameController.Instance.foundClassesList.Values[categoryIndex]);
            return newObj;
        }
    }

    public void ReturnObjectToPool(GameObject obj, int categoryIndex)
    {
        List<GameObject> subpool = objectPools[categoryIndex];
        subpool.Add(obj);
        obj.SetActive(false);
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        objectPools = new List<List<GameObject>>();
    }

    void Start()
    {
        // Initialize the object pools size communicating with the GameController
        for (int i = 0; i < GameController.Instance.foundClassesList.Count; ++i)
            objectPools.Add(new List<GameObject>());
        // Get probability weights from GlobalSettings
        weights = new List<float>(GlobalSettings.Instance.classesProbabilityWeights);
        // Normalize the weights
        float totalWeight = 0f;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }
        for (int i = 0; i < weights.Count; i++)
        {
            weights[i] /= totalWeight;
        }
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        resetPoolTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            // Spawn an object
            Spawn();
            spawnTimer = 0f;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
        if (resetPoolTimer >= resetPoolTime)
        {
            // Reset the pool
            for (int i = 0; i < objectPools.Count; i++)
            {
                foreach (GameObject obj in objectPools[i])
                {
                    Destroy(obj);
                }
                objectPools[i].Clear();
            }
            resetPoolTimer = 0f;
        }
    }

    void Spawn()
    {
        // Get a category index based on the weights
        float rnd = Random.value;
        int classID = -1;
        for (int i = 0; i < weights.Count; i++)
        {
            if (rnd <= weights[i])
            {
                classID = i;
                break;
            }
            rnd -= weights[i];
        }
        int foundClassID = GameController.Instance.foundClassesList.IndexOfKey(classID);
        // Retrieve an object from the pool
        GameObject obj = GetObjectFromPool(foundClassID);
    }
}

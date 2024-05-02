using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundObject : MonoBehaviour
{
    // Public variables
    public float minSpeed = 70; // In km/h
    public float maxSpeed = 90; // In km/h
    public float timeToLive = 30f;
    [HideInInspector] public Spawner spawnerReference;
    [HideInInspector] public int classID;

    // Private variables
    private LineRenderer pathReference;
    private Vector3 direction;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        pathReference = spawnerReference.GetComponent<LineRenderer>();
    }

    void OnEnable()
    {
        Invoke("Kill", timeToLive);
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object along the path
        direction = pathReference.GetPosition(1) - pathReference.GetPosition(0);
        direction.Normalize();
        transform.position += direction * Time.deltaTime * speed / 3.6f;
        // Check if the object has reached the end of the path
        if (Vector3.Distance(transform.position, pathReference.GetPosition(1)) < 1)
        {
            Kill();
        }
    }

    private void Kill()
    {
        // Prevent previous invocations to kill this instance
        CancelInvoke();
        // Send back to the pool from which it came from
        spawnerReference.ReturnObjectToPool(gameObject, classID);
    }
}

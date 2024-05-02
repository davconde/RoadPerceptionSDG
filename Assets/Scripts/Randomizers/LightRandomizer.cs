using UnityEngine;

public class LightRandomizer : MonoBehaviour
{
    private Light directionalLight;
    [SerializeField] private float minIntensity = 10000f;
    [SerializeField] private float maxIntensity = 130000f;
    [SerializeField] private float interval = 1f;

    private float timer = 0f;

    private void Start()
    {
        directionalLight = GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            RandomizeLight();
            timer = 0f;
        }
    }

    private void RandomizeLight()
    {
        // Randomize intensity
        float randomIntensity = Random.Range(minIntensity, maxIntensity);
        directionalLight.intensity = randomIntensity;

        // Randomize light direction
        Vector3 randomDirection = Random.insideUnitSphere;
        randomDirection.y = -Mathf.Abs(randomDirection.y);
        directionalLight.transform.rotation = Quaternion.LookRotation(randomDirection);
    }
}
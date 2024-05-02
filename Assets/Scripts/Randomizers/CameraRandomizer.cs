using UnityEngine;

public class CameraRandomizer : MonoBehaviour
{
    [SerializeField] private Vector3 minPosition = new Vector3(141.7f, 20.0f, 20.0f);
    [SerializeField] private Vector3 maxPosition = new Vector3(141.7f, 20.0f, 20.0f);
    [SerializeField] private Vector3 minRotation = new Vector3(25.0f, -161.0f, 0.0f);
    [SerializeField] private Vector3 maxRotation = new Vector3(50.0f, -107.0f, 0.0f);
    [SerializeField] private float interval = 1f;

    private float timer = 0f;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            RandomizeCamera();
            timer = 0f;
        }
    }

    private void RandomizeCamera()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minPosition.x, maxPosition.x),
            Random.Range(minPosition.y, maxPosition.y),
            Random.Range(minPosition.z, maxPosition.z)
        );

        Vector3 randomRotation = new Vector3(
            Random.Range(minRotation.x, maxRotation.x),
            Random.Range(minRotation.y, maxRotation.y),
            Random.Range(minRotation.z, maxRotation.z)
        );

        transform.position = randomPosition;
        transform.rotation = Quaternion.Euler(randomRotation);
    }
}
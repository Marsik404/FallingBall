using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;
    public float spawnHeight = 1f;
    public int delayTime = 1;

    private void Start()
    {
        Invoke("SpawnBallOnTopRod", delayTime);
    }

    void SpawnBallOnTopRod()
    {
        float cylinderHeight = GetComponent<Renderer>().bounds.size.y;

        Vector3 spawnPosition = transform.position + new Vector3(-1.5f, cylinderHeight / 2 + spawnHeight, 0);

        //Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        GameObject spawnedBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
        if (cameraFollow is not null)
        {
            cameraFollow.SetBall(spawnedBall.transform);
        }
    }
}

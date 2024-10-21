using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public GameObject spherePrefab;
    public int numberOfSpheres = 5;
    public float distanceBetweenSpheres = 3f;

    private void Start()
    {
        SpawnSpheres();
    }

    void SpawnSpheres()
    {
        float cylinderHeight = GetComponent<Renderer>().bounds.size.y;
        Vector3 startPosition = transform.position - new Vector3(0, cylinderHeight / 2, 0);



        // Фіксована відстань
        for (int i = 0; i < numberOfSpheres; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(0, i * distanceBetweenSpheres, 0);

            if (spawnPosition.y <= transform.position.y + cylinderHeight / 2)
            {
                Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                break;
            }
        }



        //// Рівномірна відстань
        //float distanceBetweenSpheres = cylinderHeight / (numberOfSpheres - 1);
        //for (int i = 0; i < numberOfSpheres; i++)
        //{
        //    Vector3 spawnPosition = startPosition + new Vector3(0, i * distanceBetweenSpheres, 0);
        //    Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
        //}

    }
}

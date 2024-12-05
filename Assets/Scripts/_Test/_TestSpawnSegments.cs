using UnityEngine;

public class _TestSpawnSegments : MonoBehaviour
{
    [SerializeField] private GameObject _segmentPrefab;
    [SerializeField] private float _radius;
    private GameObject _segment;



    private void Start()
    {
        SpawnSingleSegment();
    }

    void SpawnSingleSegment()
    {
        float cylinderHeight = GetComponent<Renderer>().bounds.size.y;
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y - cylinderHeight / 2, transform.position.z);
        float angleStep = 360f / 6;

        for (int i = 0; i < 5; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;



            Vector3 sectorOffsetHorizontal = transform.position + new Vector3(Mathf.Cos(angle) * _radius, 0, Mathf.Sin(angle) * _radius);                   // spawnPosition - spawning at the bottom of the cylinder

            _segment = Instantiate(_segmentPrefab, sectorOffsetHorizontal, Quaternion.identity);

            Quaternion rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Vector3.up) * Quaternion.Euler(0, 90, 0);    // Quaternion.Euler(0, 90, 0); - for segment with forward (X)



            _segment.transform.rotation = rotation;
            _segment.transform.parent = transform;

            Debug.DrawLine(_segment.transform.position, transform.position, Color.red, 60f);
        }
    }
}

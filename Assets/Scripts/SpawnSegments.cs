using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SpawnSegments : MonoBehaviour
{
    [SerializeField] private GameObject _segmentPrefab;                 // GameObject - because there is no component on the segment
    [SerializeField] private int _numberOfSegment = 5;
    [SerializeField] private float _distanceBetweenSegments = 3f;

    private int _sectorsPerCylinder = 6;
    private Renderer _renderer;

    private void Awake()
    {
        if (_renderer == null)
        {
            _renderer = GetComponent<Renderer>();
        }
    }

    private void Start()
    {
        SpawnSegmentsOnRod();
    }

    void SpawnSegmentsOnRod()
    {
        float cylinderHeight = _renderer.bounds.size.y;
        float cylinderRadius = _renderer.bounds.size.x / 2;

        Vector3 basePosition = transform.position - new Vector3(0, cylinderHeight / 2, 0);
        float angleStepForSegment = 360f / _sectorsPerCylinder;

        for (int i = 0; i < _numberOfSegment; i++)
        {
            Vector3 spawnPositionVertical = basePosition + new Vector3(0, i * _distanceBetweenSegments, 0);

            if (spawnPositionVertical.y <= transform.position.y + cylinderHeight / 2)
            {
                int segmentsToSpawn = (i == 0) ? _sectorsPerCylinder : Random.Range(1, 6);                        // 1,6 - magic numbers?

                HashSet<int> listSectors = new HashSet<int>();

                for (int j = 0; j < segmentsToSpawn; j++)
                {
                    // We choose a random sector for the segment until we find a free one. Logic is SHIT!!!
                    int randomSector;
                    do
                    {
                        randomSector = Random.Range(0, 6);                                                        // 0,6 - magic numbers? Logic sucks!!!
                    } while (listSectors.Contains(randomSector));

                    listSectors.Add(randomSector);



                    // Calculate the angle for the current segment, taking into account the random sector
                    float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;                             // Convert degrees to radians

                    // Calculate the position of the segment based on the angle
                    Vector3 sectorOffsetHorizontal = new Vector3(Mathf.Cos(angle) * (cylinderRadius - 0.5f), 0, Mathf.Sin(angle) * (cylinderRadius - 0.5f)); // Reduced by 0.5 to avoid overlap
                    Vector3 finalSpawnPosition = spawnPositionVertical + sectorOffsetHorizontal;

                    GameObject segment = Instantiate(_segmentPrefab, finalSpawnPosition, Quaternion.identity);

                    // Calculate the rotation of the segment so that the nose points to the center
                    Quaternion rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Vector3.up);



                    segment.transform.rotation = rotation;

                    segment.transform.parent = transform;
                }
            }
            else
            {
                break;
            }
        }
    }
}

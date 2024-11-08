using System.Collections.Generic;
using UnityEngine;

public class SpawnPiece : MonoBehaviour
{
    [SerializeField] private GameObject _cylinderPrefab;
    [SerializeField] private GameObject _segmentPrefab;                 // GameObject - because there is no component on the segment
    [SerializeField] private int _countPlanforms = 5;
    [SerializeField] private float _distanceBetweenPlanforms = 3;
    [SerializeField] private SpawnCylinder _spawnCylinder;

    private int _sectorsPerCylinder = 6;

    private void Start()
    {
        SpawnSegmentsOnRod();
    }

    void SpawnSegmentsOnRod()
    {

        float cylinderHeight = _spawnCylinder.GetHeightBlocks();
        float cylinderRadius = _cylinderPrefab.GetComponent<Renderer>().bounds.size.x / 2;

        Vector3 basePosition = transform.position;

        float angleStepForSegment = 360f / _sectorsPerCylinder;

        for (int i = 0; i < _countPlanforms; i++)
        {
            Vector3 spawnPositionVertical = basePosition + new Vector3(0, i * _distanceBetweenPlanforms, 0);

            if (spawnPositionVertical.y > cylinderHeight * 2)
            {
                Debug.Log("Перевищенно висоту");
                break;
            }

            int segmentsToSpawn = (i == 0) ? _sectorsPerCylinder : Random.Range(4, 6);

            HashSet<int> listSectors = new HashSet<int>();

            for (int j = 0; j < segmentsToSpawn; j++)
            {
                int randomSector;
                do
                {
                    randomSector = Random.Range(0, _sectorsPerCylinder);
                } while (listSectors.Contains(randomSector));

                listSectors.Add(randomSector);

                float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;
                Vector3 sectorOffsetHorizontal = new Vector3(Mathf.Cos(angle) * (cylinderRadius - 0.5f), 0, Mathf.Sin(angle) * (cylinderRadius - 0.5f));

                Vector3 finalSpawnPosition = spawnPositionVertical + sectorOffsetHorizontal;

                GameObject segment = Instantiate(_segmentPrefab, finalSpawnPosition, Quaternion.identity);
                Quaternion rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Vector3.up);

                segment.transform.rotation = rotation;
                segment.transform.parent = transform;
            }
        }
    }
}

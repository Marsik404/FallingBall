using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SegmentedPlatform : Platform
{
    protected const int _dividePlatformIntoSectors = 6;
    [SerializeField] protected Segment _segmentPrefab;

    //protected void InitializeSegments(int segmentsToSpawn = 6, float radius = 0f)     // TODO ? v.2
    protected void InitializeSegments(int segmentsToSpawn, int dividePlatformIntoSectors, float radius = 0f)
    {
        HashSet<int> listSectors = new HashSet<int>();
        float angleStepForSegment = 360f / dividePlatformIntoSectors;

        for (int j = 0; j < segmentsToSpawn; j++)
        {
            // TODO find logic is better. We choose a random sector for the segment until we find a free one.
            int randomSector;
            do
            {
                randomSector = Random.Range(0, dividePlatformIntoSectors);          // TODO ? 0 - magic numbers?
            } while (listSectors.Contains(randomSector));

            listSectors.Add(randomSector);

            float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;       // Convert degrees to radians

            // Calculate the position of the segment based on the angle
            Vector3 sectorOffsetHorizontal = transform.position + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            Segment segment = Instantiate(_segmentPrefab, sectorOffsetHorizontal, Quaternion.identity);

            // Calculate the rotation of the segment so that the nose points to the center
            Quaternion rotation = Quaternion.LookRotation(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)), Vector3.up);

            segment.transform.rotation = rotation;
            segment.transform.parent = transform;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : Platform
{
    public override void AddSegments()
    {
        int dividePlatformIntoSectors = 6;
        float radius = 0f;
        int segmentsToSpawn = dividePlatformIntoSectors;
        float angleStepForSegment = 360f / dividePlatformIntoSectors;

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



            float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;                             // Convert degrees to radians

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

using System.Collections.Generic;
using UnityEngine;

public abstract class SegmentedPlatform : Platform
{
    protected const int DIVIDE_PLATFORM_INTO_SECTORS = 6;

    [SerializeField] protected Segment _segmentPrefab;

    private List<Segment> _segments = new List<Segment>();    // not yet use in other scripts, just saving Instantiate



    protected void InitializeSegments(int segmentsToSpawn, int dividePlatformIntoSectors, float radius = 0f)
    {
        HashSet<int> listSectors = new HashSet<int>();
        float angleStepForSegment = 360f / dividePlatformIntoSectors;

        for (int i = 0; i < segmentsToSpawn; i++)
        {
            Segment instanceSegment = Instantiate(_segmentPrefab, transform);
            instanceSegment.name = $"Segment_{i}";
            _segments.Add(instanceSegment);

            //instanceSegment.SegmentID = i; // TODO ?



            // TODO find logic is better. We choose a random sector for the segment until we find a free one.
            int randomSector;
            do
            {
                randomSector = Random.Range(0, dividePlatformIntoSectors);
            } while (listSectors.Contains(randomSector));

            listSectors.Add(randomSector);

            float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;       // Convert degrees to radians



            // Calculate the rotation of the segment so that the nose points to the center
            //var sectorRotationOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            //Quaternion rotation = Quaternion.LookRotation(sectorRotationOffset, Vector3.up);    // * Quaternion.Euler(0, 90, 0);

            //Vector3 position = rotation * -instanceSegment.transform.right * radius;

            //instanceSegment.transform.rotation = rotation;
            //instanceSegment.transform.localPosition += -instanceSegment.transform.right * radius;


            var sectorRotationOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Quaternion rotation = Quaternion.LookRotation(sectorRotationOffset, Vector3.up);

            Vector3 position = rotation * -instanceSegment.transform.right * radius;

            instanceSegment.transform.rotation = rotation;
            instanceSegment.transform.localPosition = position;
        }
    }

    // TODO ?
    //public List<Segment> GetSegments()
    //{
    //    return _segments;
    //}
}

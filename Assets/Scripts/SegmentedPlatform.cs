using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SegmentedPlatform : Platform
{
    protected const int DIVIDE_PLATFORM_INTO_SECTORS = 6;
    protected CancellationTokenSource _cancellationTokenSource;

    [SerializeField] protected Segment _segmentPrefab;

    private List<Segment> _segments = new List<Segment>();    // not yet use in other scripts, just saving Instantiate



    protected void InitializeSegments(int segmentsToSpawn, int dividePlatformIntoSectors, float radius = 0f)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        HashSet<int> listSectors = new HashSet<int>();
        float angleStepForSegment = 360f / dividePlatformIntoSectors;

        for (int i = 0; i < segmentsToSpawn; i++)
        {
            Segment instanceSegment = Instantiate(_segmentPrefab, transform);
            instanceSegment.name = $"Segment_{i + 1}";
            instanceSegment.SetCancellationTokenSource(_cancellationTokenSource);
            instanceSegment.SetPlatform(this);
            _segments.Add(instanceSegment);

            // TODO find logic is better. We choose a random sector for the segment until we find a free one.
            int randomSector;
            do
            {
                randomSector = Random.Range(0, dividePlatformIntoSectors);
            } while (listSectors.Contains(randomSector));

            listSectors.Add(randomSector);

            float angle = randomSector * angleStepForSegment * Mathf.Deg2Rad;       // Convert degrees to radians

            var sectorRotationOffset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            Quaternion rotation = Quaternion.LookRotation(sectorRotationOffset, Vector3.up);

            Vector3 position = rotation * -instanceSegment.transform.right * radius;

            instanceSegment.transform.rotation = rotation;
            instanceSegment.transform.localPosition = position;
        }
    }

    protected void DestroyAllSegments()
    {
        foreach (var segment in _segments)
        {
            if (segment != null)
            {
                Destroy(segment.gameObject);
            }
        }
        _segments.Clear();
    }

    public IReadOnlyList<Segment> GetSegments()
    {
        return _segments.AsReadOnly();
    }

    //// V.1 Destroy Segment + Platform
    //public async Task DestroyPlatform()
    //{
    //    foreach (Segment segment in _segments)
    //    {
    //        if (segment != null)
    //        {
    //            Destroy(segment.gameObject);
    //        }
    //    }
    //    Destroy(gameObject);    // Destroy Platform
    //}

}

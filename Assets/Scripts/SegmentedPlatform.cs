using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SegmentedPlatform : Platform
{
    protected const int DIVIDE_PLATFORM_INTO_SECTORS = 6;

    [SerializeField] protected Segment _segmentPrefab;

    private List<Segment> _segments = new List<Segment>();    // not yet use in other scripts, just saving Instantiate

    //protected CancellationTokenSource _cancellationTokenSource;
    private CancellationTokenSource _cancellationTokenSource;
    private CancellationToken _cancellationToken;



    protected void InitializeSegments(int segmentsToSpawn, int dividePlatformIntoSectors, float radius = 0f)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _cancellationToken = _cancellationTokenSource.Token;

        HashSet<int> listSectors = new HashSet<int>();
        float angleStepForSegment = 360f / dividePlatformIntoSectors;

        for (int i = 0; i < segmentsToSpawn; i++)
        {
            Segment instanceSegment = Instantiate(_segmentPrefab, transform);
            instanceSegment.name = $"Segment_{i + 1}";
            instanceSegment.SegmentTookTheBall += DestroyPlatform;
            //instanceSegment.SetCancellationTokenSource(_cancellationTokenSource);
            //instanceSegment.SetPlatform(this);
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

    //public IReadOnlyList<Segment> GetSegments()
    //{
    //    return _segments.AsReadOnly();
    //}


    protected void CancelAnimation()
    {
        _cancellationTokenSource.Cancel();
    }






    //public void DestroySegmentsAboveBall(Vector3 ballPosition)
    //{
    //    List<Segment> segmentsToRemove = new List<Segment>();


    //    foreach (var segment in _segments)
    //    {
    //        if (segment.transform.position.y > ballPosition.y)
    //        {
    //            segmentsToRemove.Add(segment);
    //        }
    //    }

    //    foreach (var segment in segmentsToRemove)
    //    {
    //        _segments.Remove(segment);
    //        Destroy(segment.gameObject);
    //    }
    //}





    public async void DestroyPlatform(Segment segment)
    {
        List<Task> animationTasks = new List<Task>();

        foreach (Segment bufferSegment in _segments)
        {
            if (bufferSegment != null)
            {
                var task = bufferSegment.AnimateCrashAsync(_cancellationToken);
                animationTasks.Add(task);
            }
        }

        await Task.WhenAll(animationTasks);

        foreach (Segment bufferSegment in _segments)
        {
            if (bufferSegment != null)
            {
                Destroy(bufferSegment.gameObject);
            }
        }

        _segments.Clear();
        Destroy(gameObject);
    }




}

using UnityEngine;

public class LevelPlatform : SegmentedPlatform
{
    [SerializeField, Range(0, 6)] private int _minSerments = 4;
    [SerializeField, Range(0, 6)] private int _maxSerments = 5;



    public override void InitializeSegments()
    {
        int segmentsToSpawn = Random.Range(_minSerments, _maxSerments);

        InitializeSegments(segmentsToSpawn, DIVIDE_PLATFORM_INTO_SECTORS);
    }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : SegmentedPlatform
{
    public override void InitializeSegments()
    {
        int segmentsToSpawn = _dividePlatformIntoSectors;

        InitializeSegments(segmentsToSpawn, _dividePlatformIntoSectors);

        //TODO ? v.2
        //InitializeSegments();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : SegmentedPlatform
{
    public override void InitializeSegments()
    {
        InitializeSegments(_dividePlatformIntoSectors, _dividePlatformIntoSectors);
    }
}

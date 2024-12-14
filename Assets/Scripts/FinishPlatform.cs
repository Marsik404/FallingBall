using UnityEngine;

public class FinishPlatform : SegmentedPlatform
{
    public override void InitializeSegments()
    {
        InitializeSegments(DIVIDE_PLATFORM_INTO_SECTORS, DIVIDE_PLATFORM_INTO_SECTORS);
    }
}

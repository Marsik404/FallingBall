using UnityEngine;

public class LevelPlatform : SegmentedPlatform
{
    [SerializeField, Range(0, 6)] private int _minSerments;
    [SerializeField, Range(0, 6)] private int _maxSerments;



    private void OnValidate()
    {
        if (_minSerments <= 0 || _maxSerments >= 6)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Debug.Log($"Value _minSerments should be > 0 (now:{_minSerments}) and " +
                      $"Value _maxSerments should be < 6 (now:{_maxSerments})");
#else
	            Application.Quit();
#endif
        }
    }

    public override void InitializeSegments()
    {
        int segmentsToSpawn = Random.Range(_minSerments, _maxSerments);

        InitializeSegments(segmentsToSpawn, DIVIDE_PLATFORM_INTO_SECTORS);
    }
}


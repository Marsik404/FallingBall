using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] private Platform _levelPlatformPrefab;
    [SerializeField] private Platform _finishPlatformPrefab;

    private Platform _platform; // not yet use in other scripts, just saving Instantiate



    public void AddPlatform(bool isFinish)
    {
        Platform prefab = isFinish ? _finishPlatformPrefab : _levelPlatformPrefab;
        _platform = Instantiate(prefab, transform.position, Quaternion.identity, transform);

        // Call the platform method to add segments
        _platform.InitializeSegments();
    }
}

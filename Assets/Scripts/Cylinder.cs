using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
    [SerializeField] LevelPlatform _levelPlatformPrefab;
    [SerializeField] FinishPlatform _finishPlatformPrefab;

    public void AddPlatform(bool isFinish)
    {
        //// v.2 spawn from the bottom of the cylinder
        //float cylinderHeight = GetComponent<Renderer>().bounds.size.y;
        //Vector3 spawnPosition = transform.position - new Vector3(0, cylinderHeight / 2, 0);

        Platform platform = isFinish
            ? Instantiate(_finishPlatformPrefab, transform.position /*spawnPosition*/, Quaternion.identity, transform)
            : Instantiate(_levelPlatformPrefab, transform.position /*spawnPosition*/, Quaternion.identity, transform);

        // Call the platform method to add segments
        platform.AddSegments();
    }
}

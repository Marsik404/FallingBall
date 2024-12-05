using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    [SerializeField] private CameraController _cameraPrefab;
    
    private CameraController _camera;   // not yet use in other scripts, just saving Instantiate



    public CameraController CreateCamera(Transform spawnPointCamera)
    {
        _camera = Instantiate(_cameraPrefab, spawnPointCamera.transform.position, Quaternion.identity);

        // v.2
        //_camera.FollowBall(spawnPointCamera);

        return _camera;
    }
}

using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    [SerializeField] private CameraController _cameraPrefab;

    private CameraController _camera;   // not yet use in other scripts, just saving Instantiate



    public CameraController CreateCamera(Vector3 spawnPointCamera)
    {
        _camera = Instantiate(_cameraPrefab, spawnPointCamera, Quaternion.identity);

        return _camera;
    }
}

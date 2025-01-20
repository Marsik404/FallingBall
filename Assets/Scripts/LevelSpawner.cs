using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private RodSpawner _rodSpawner;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private CameraSpawner _cameraSpawner;

    [SerializeField, Range(1, 100)] private int _heightSpawnBall = 5;

    private BallController _ball;       // not yet use in other scripts, just saving Instantiate
    private CameraController _camera;   // not yet use in other scripts, just saving Instantiate



    void Start()
    {
        int _heightBlocks = _rodSpawner.HeightBlocks;

        _heightSpawnBall = Mathf.Clamp(_heightSpawnBall, 1, _heightBlocks);

        _rodSpawner.BuildRod();

        _rodSpawner.SetHighestSpawnBall(_heightSpawnBall);

        _ball = _ballSpawner.CreateBall(_rodSpawner.HightPointRod);

        _camera = _cameraSpawner.CreateCamera(_ball.transform.position);

        _camera.FollowBall(_ball.transform);

    }
}

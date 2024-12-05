using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private RodSpawner _rodSpawner;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private CameraSpawner _cameraSpawner;

    private BallController _ball;       // not yet use in other scripts, just saving Instantiate
    private CameraController _camera;   // not yet use in other scripts, just saving Instantiate



    void Start()
    {
        _rodSpawner.BuildRod();

        _ball = _ballSpawner.CreateBall(_rodSpawner._highestPointRod);

        _camera = _cameraSpawner.CreateCamera(_ball.transform);

        _camera.FollowBall(_ball.transform);

    }
}

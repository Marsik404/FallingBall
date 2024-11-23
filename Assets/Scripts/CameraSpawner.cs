using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    [SerializeField] private CameraSpawner _cameraSpawner;

    private ControllerBall _ball;

    // TODO x Debug
    [SerializeField] private Vector3 _offset;



    public void CreateCamera(ControllerBall ball)
    {
        _ball = ball;
    }

    void Update()
    {
        // TODO x Debug
        transform.position = _ball.transform.position + _offset;    // TODO ? Need check on the null? if _ball == null, but its bad practice
    }
}

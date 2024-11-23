using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private RodSpawner _rodSpawner;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private CameraSpawner _cameraSpawner;



    void Start()
    {
        // TODO x Debug
        //StartCoroutine(Example());


        _rodSpawner.BuildRod();

        _ballSpawner.SetRodSpawner(_rodSpawner);

        ControllerBall ball = _ballSpawner.CreateBall();
        
        _cameraSpawner.CreateCamera(ball);

    }

    // TODO x Debug
    //IEnumerator Example()
    //{
    //    yield return new WaitForSeconds(2f);
    //    _rodSpawner.BuildRod();
    //    yield return new WaitForSeconds(2f);
    //    ControllerBall ball = _ballSpawner.CreateBall();
    //    yield return new WaitForSeconds(2f);
    //    _cameraSpawner.CreateCamera(ball);
    //}

}

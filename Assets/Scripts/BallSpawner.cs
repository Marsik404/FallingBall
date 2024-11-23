using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private ControllerBall _ballPrefab;

    private RodSpawner _rodSpawner;

    public void SetRodSpawner(RodSpawner rodSpawner)            // TODO ? properies?
    {
        _rodSpawner = rodSpawner;
    }



    // TODO x Debug
    //private void Start()          
    //{
    //    Invoke("CreateBall", 1f);
    //}



    public ControllerBall CreateBall()
    {
        Vector3 cylinderHeight = _rodSpawner.HighestPointRod;

        Vector3 spawnPosition = cylinderHeight + new Vector3(-1.5f, 2f, 0);

        ControllerBall ball = Instantiate(_ballPrefab, spawnPosition, Quaternion.identity);

        return ball;
    }





    //    ////// v.1 We transfer the object to the camera
    //    ////CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
    //    ////if (cameraFollow != null)
    //    ////{
    //    ////    cameraFollow.SetBall(spawnedBall.transform);
    //    ////}


    //    ////// v.2 We transfer the object to the camera
    //    //OnBallSpawned?.Invoke(spawnedBall.transform);
    //}
}

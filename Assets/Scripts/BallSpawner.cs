using System;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private BallController _ballPrefab;
    [SerializeField] private Vector3 _offsetSpawnBall = new Vector3(-1.5f, 2f, 0);

    private BallController _ball;   // not yet use in other scripts, just saving Instantiate



    public BallController CreateBall(Vector3 spawnPointBall)    // TODO ? (Transform instead Vector3?)
    {
        Vector3 spawnPosition = spawnPointBall + _offsetSpawnBall;

        _ball = Instantiate(_ballPrefab, spawnPosition, Quaternion.identity);

        return _ball;
    }
}

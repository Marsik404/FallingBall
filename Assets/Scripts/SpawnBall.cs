//using System;
//using UnityEngine;

//[RequireComponent(typeof(Renderer))]
//public class SpawnBall : MonoBehaviour
//{
//    [SerializeField] private SpawnCylinder _spawnCylinder;
//    [SerializeField] private ControllerBall _ballPrefab;
//    [SerializeField] private float _spawnHeight = 1f;
//    [SerializeField] private int _delayTime = 1;

//    private Renderer _renderer;

//    //// v.2 We transfer the object to the camera
//    public event Action<Transform> OnBallSpawned;

//    private void Awake()
//    {
//        if (_renderer == null)
//        {
//            _renderer = GetComponent<Renderer>();
//        }
//    }

//    private void Start()
//    {
//        Invoke("SpawnBallOnTopRod", _delayTime);
//    }

//    void SpawnBallOnTopRod()
//    {
//        float cylinderHeight = _spawnCylinder.GetHeightBlocks();

//        Vector3 spawnPosition = transform.position + new Vector3(-1.5f, cylinderHeight * 2 + _spawnHeight, 0);

//        ControllerBall spawnedBall = Instantiate(_ballPrefab, spawnPosition, Quaternion.identity);




//        //// v.1 We transfer the object to the camera
//        //CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
//        //if (cameraFollow != null)
//        //{
//        //    cameraFollow.SetBall(spawnedBall.transform);
//        //}


//        //// v.2 We transfer the object to the camera
//        OnBallSpawned?.Invoke(spawnedBall.transform);
//    }
//}

using UnityEngine;
using Cinemachine;

// TODO CameraFollow
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private BallSpawner _spawnBall;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _moveDuration = 2f;

    private Transform _ball;
    private bool _isMovingToBall = false;
    private float _elapsedTime = 0f;

    private void Start()
    {
        if (_spawnBall != null)
        {
            // TODO event
            //_spawnBall.OnBallSpawned += SetBall;         // In BallSpawner need (public event Action<Transform> OnBallSpawned;)
        }

        if (_startPoint != null && _virtualCamera != null)
        {
            _virtualCamera.transform.position = _startPoint.position;
        }
    }

    private void Update()
    {
        if (_isMovingToBall && _ball != null)
        {
            _elapsedTime += Time.deltaTime;
            float t = _elapsedTime / _moveDuration;

            _virtualCamera.transform.position = Vector3.Lerp(_startPoint.position, _ball.position, t);

            if (t >= 1f)
            {
                _isMovingToBall = false;
                _virtualCamera.Follow = _ball;
                _virtualCamera.transform.rotation = Quaternion.Euler(30f, 90f, 0f);
            }
        }
    }

    public void SetBall(Transform newBall)
    {
        _ball = newBall;

        if (_ball != null)
        {
            _isMovingToBall = true;
            _elapsedTime = 0f;
        }
    }
}

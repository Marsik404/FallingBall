using UnityEngine;

public class OldCameraFollow : MonoBehaviour
{
    [SerializeField] private float _distanceFromBall = 5f;
    [SerializeField] private float _heightOffset = 2f;
    [SerializeField] private float _followSpeed = 2f;
    [SerializeField] private float _horizontalAngle = 0f;
    [SerializeField] private float _verticalAngle = 30f;
    [SerializeField] private SpawnBall _spawnBall;

    private Transform _ball;

    // v.2
    private void Start()
    {
        if (_spawnBall != null)
        {
            _spawnBall.OnBallSpawned += SetBall;
        }
    }

    private void LateUpdate()
    {
        if (_ball != null)
        {
            // Calculate the new position for the camera taking into account the angles
            Quaternion rotation = Quaternion.Euler(_verticalAngle, _horizontalAngle, 0);
            Vector3 offset = rotation * new Vector3(0, _heightOffset, -_distanceFromBall);
            Vector3 targetPosition = _ball.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, _followSpeed * Time.deltaTime);

            transform.LookAt(_ball);
        }
    }

    public void SetBall(Transform newBall)
    {
        _ball = newBall;
    }
}

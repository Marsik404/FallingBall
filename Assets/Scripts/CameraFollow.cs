using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _ball;

    public float distanceFromBall = 5f;
    public float heightOffset = 2f;
    public float followSpeed = 2f;
    public float horizontalAngle = 0f;
    public float verticalAngle = 30f;

    private void LateUpdate()
    {
        if (_ball is not null)
        {
            // Обчислюємо нову позицію для камери з урахуванням кутів
            Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
            Vector3 offset = rotation * new Vector3(0, heightOffset, -distanceFromBall);
            Vector3 targetPosition = _ball.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            transform.LookAt(_ball);
        }
    }

    public void SetBall(Transform newBall)
    {
        _ball = newBall;
    }
}

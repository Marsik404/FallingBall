using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 _offsetCamera;

    private Transform _ball;



    public void FollowBall(Transform ball)
    {
        _ball = ball;
    }

    void Update()
    {
        transform.position = _ball.transform.position + _offsetCamera;    // TODO ? Need check on the null? if _ball == null, but its bad practice
    }
}

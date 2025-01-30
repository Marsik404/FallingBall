using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)] private float _heightCrashSegment;
    // Horizontal move
    [SerializeField] private float _horizontalMoveDistance = 10f;
    [SerializeField] private float _horizontalMoveDuration = 0.4f;
    // Vertical move
    [SerializeField] private float _verticalMoveDistance = 10f;
    [SerializeField] private float _verticalMoveDuration = 1f;
    // Rotate
    [SerializeField] private float _rotationSpeed = 180f;

    private Vector3 _originalPosition;
    private Vector3 _targetPosition;



    public event Action<Segment> SegmentTookTheBall;





    //public void DestroySegmentsAbove()
    //{
    //    _segments = _segmentedPlatform.GetSegments();

    //    foreach (var segment in _segments)
    //    {
    //        segment.StartCrash(segment);
    //    }
    //}








    public void TouchedSegment(float totalFallDistance)
    {
        if (totalFallDistance >= _heightCrashSegment)
        {
            Debug.Log($"Crash: {totalFallDistance}");

            SegmentTookTheBall?.Invoke(this);

        }
    }

    public async Task AnimateCrashAsync(CancellationToken cancellationToken)
    {
        _originalPosition = transform.position;
        Vector3 newPosition = transform.position - transform.right * _horizontalMoveDistance;
        newPosition.y = transform.position.y;
        _targetPosition = newPosition;

        try
        {
            float crashTime = 0f;

            // Horizontal move
            while (crashTime < _horizontalMoveDuration)
            {
                if (cancellationToken.IsCancellationRequested) return;

                crashTime += Time.deltaTime;
                float progress = Mathf.Clamp01(crashTime / _horizontalMoveDuration);

                Vector3 newHorizontalPosition = Vector3.Lerp(_originalPosition, _targetPosition, progress);
                float zRotation = -_rotationSpeed * crashTime;

                transform.position = newHorizontalPosition;
                transform.Rotate(Vector3.forward, zRotation * Time.deltaTime);

                await Task.Yield();
            }

            if (cancellationToken.IsCancellationRequested) return;

            // Vertical move
            float fallTime = 0f;
            Vector3 fallStartPosition = transform.position;
            Vector3 fallEndPosition = fallStartPosition + Vector3.down * _verticalMoveDistance;

            while (fallTime < _verticalMoveDuration)
            {
                if (cancellationToken.IsCancellationRequested) return;

                fallTime += Time.deltaTime;
                float progress = Mathf.Clamp01(fallTime / _verticalMoveDuration);

                transform.position = Vector3.Lerp(fallStartPosition, fallEndPosition, progress);

                float zRotation = -_rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, zRotation);

                await Task.Yield();
            }
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Crash animation cancelled.");
        }
    }

}
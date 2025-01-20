using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Segment : MonoBehaviour
{
    [SerializeField, Range(0.1f, 5f)] private float _heightCrashSegment;

    private SegmentedPlatform _segmentedPlatform;



    ////V.1 Destroy Segment + Platform
    //public void SetPlatform(SegmentedPlatform platform)
    //{
    //    _segmentedPlatform = platform;
    //}

    //public void DestroySegment(float totalFallDistance)
    //{
    //    if (totalFallDistance >= _heightCrashSegment)
    //    {
    //        Debug.Log($"Crash: {totalFallDistance}");

    //        //Destroy(gameObject);  // Destroy Segment
    //        if (_segmentedPlatform != null)
    //        {
    //            _segmentedPlatform.DestroyPlatform();
    //        }
    //    }
    //}





    // V.2 Destroy Segment + Platform
    private IReadOnlyList<Segment> _segments;

    // Horizontal move
    [SerializeField] private float _horizontalMoveDistance = 10f;
    [SerializeField] private float _horizontalMoveDuration = 0.4f;
    // Vertical move
    [SerializeField] private float _verticalMoveDistance = 10f;
    [SerializeField] private float _verticalMoveDuration = 1f;

    [SerializeField] private float _rotationSpeed = 180f;

    private Vector3 _originalPosition;
    private Vector3 _targetPosition;
    private bool _isCrash = false;

    CancellationTokenSource _cancellationTokenSource;
    CancellationToken _cancellationToken;



    public void SetCancellationTokenSource(CancellationTokenSource cancellationTokenSource)
    {
        _cancellationTokenSource = cancellationTokenSource;
        _cancellationToken = cancellationTokenSource.Token;
    }

    public void SetPlatform(SegmentedPlatform platform)
    {
        _segmentedPlatform = platform;
    }



    public void DestroySegmentsAbove()
    {
        _segments = _segmentedPlatform.GetSegments();

        foreach (var segment in _segments)
        {
            segment.StartCrash(segment);
        }
    }

    public void DestroySegment(float totalFallDistance)
    {
        if (totalFallDistance >= _heightCrashSegment && !_isCrash)
        {
            Debug.Log($"Crash: {totalFallDistance}");

            _segments = _segmentedPlatform.GetSegments();

            foreach (var segment in _segments)
            {
                //Destroy(segment.gameObject);  // Destroy segment when touch

                if (segment != null)
                {
                    //Vector3 newPosition = segment.transform.position - segment.transform.right * 3;
                    //newPosition.y = segment.transform.position.y;
                    //segment.transform.position = newPosition;


                    segment.StartCrash(segment);
                    //segment.AnimateBounceAsync(_cancellationToken, segment);
                }
            }
        }
    }

    public void StartCrash(Segment segment)
    {
        if (_isCrash)
        {
            return;
        }
        _isCrash = true;

        _originalPosition = segment.transform.position;
        Vector3 newPosition = segment.transform.position - segment.transform.right * _horizontalMoveDistance;
        newPosition.y = segment.transform.position.y;
        _targetPosition = newPosition;

        AnimateCrashAsync(_cancellationToken);
    }

    public async void AnimateCrashAsync(CancellationToken cancellationToken)
    {
        try
        {
            float crashTime = 0f;

            // Horizontal move
            // TODO ? create method?
            while (crashTime < _horizontalMoveDuration)
            {
                if (cancellationToken.IsCancellationRequested) break;

                crashTime += Time.deltaTime;
                float progress = Mathf.Clamp01(crashTime / _horizontalMoveDuration);

                Vector3 newPosition = Vector3.Lerp(_originalPosition, _targetPosition, progress);
                float zRotation = -_rotationSpeed * crashTime;

                transform.position = newPosition;
                transform.Rotate(Vector3.forward, zRotation * Time.deltaTime);

                await Task.Yield();
            }

            if (cancellationToken.IsCancellationRequested) return;

            // Vertical move
            // TODO ? create method?
            float fallTime = 0f;
            Vector3 fallStartPosition = transform.position;
            Vector3 fallEndPosition = fallStartPosition + Vector3.down * _verticalMoveDistance;

            while (fallTime < _verticalMoveDuration)
            {
                if (cancellationToken.IsCancellationRequested) break;

                fallTime += Time.deltaTime;
                float progress = Mathf.Clamp01(fallTime / _verticalMoveDuration);

                transform.position = Vector3.Lerp(fallStartPosition, fallEndPosition, progress);

                float zRotation = -_rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.forward, zRotation);

                await Task.Yield();
            }

            if (!cancellationToken.IsCancellationRequested)
            {
                _isCrash = false;
                Destroy(gameObject);
            }
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Crash animation cancelled.");
        }
    }

}
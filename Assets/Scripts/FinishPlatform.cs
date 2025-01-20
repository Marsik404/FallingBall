using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPlatform : SegmentedPlatform
{
    public override void InitializeSegments()
    {
        InitializeSegments(DIVIDE_PLATFORM_INTO_SECTORS, DIVIDE_PLATFORM_INTO_SECTORS);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BallController controller))
        {
            CancelAnimations();
            //DestroyAllSegments();
            RestartScene();
        }
    }

    private void CancelAnimations()
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

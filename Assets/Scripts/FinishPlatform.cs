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
            CancelAnimation();

            RestartScene();
        }
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

using UnityEngine;

public class BallUpperCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Segment segment))
        {
            Debug.Log($"UpperCollider touched segment: {segment.name}");
            segment.DestroySegmentsAbove();
        }
    }
}

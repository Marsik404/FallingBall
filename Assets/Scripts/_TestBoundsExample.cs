using UnityEngine;

public class _TestBoundsExample : MonoBehaviour
{
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            Bounds bounds = meshRenderer.bounds;

            Debug.Log("Size of object: " + bounds.size);
            Debug.Log("Center of object: " + bounds.center);
        }
    }
}

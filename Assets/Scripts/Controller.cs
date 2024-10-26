using UnityEngine;

public class Controller : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 rotation = new Vector3(0, horizontalInput * rotationSpeed * Time.deltaTime, 0);

        transform.Rotate(rotation);
    }
}

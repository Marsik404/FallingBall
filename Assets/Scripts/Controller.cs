using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;



    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 rotation = new Vector3(0, horizontalInput * _rotationSpeed * Time.deltaTime, 0);

        transform.Rotate(rotation);
    }
}

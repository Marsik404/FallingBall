using UnityEngine;

public class ControllerBall : MonoBehaviour
{
    private Rigidbody _rb;
    private bool _isGrounded;

    public float jumpForce = 5f;
    public float groundCheckDistance = 0.3f;
    public LayerMask groundLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (_isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
    }
}

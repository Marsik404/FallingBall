//using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]                               // adds a Rigidbody component to the Awake() method call
//public class ControllerBall : MonoBehaviour
//{
//    [SerializeField] private Rigidbody _rb;                         // if we don't drag?
//    [SerializeField] private float _jumpForce = 5f;
//    [SerializeField] private float _groundCheckDistance = 0.3f;
//    [SerializeField] private LayerMask _groundLayer;

//    private bool _isGrounded;

//    private void Awake()
//    {
//        if (_rb == null)
//        {
//            _rb = GetComponent<Rigidbody>();
//        }
//    }

//    private void Update()
//    {
//        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

//        if (_isGrounded)
//        {
//            Jump();
//        }
//    }

//    void Jump()
//    {
//        _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
//    }
//}

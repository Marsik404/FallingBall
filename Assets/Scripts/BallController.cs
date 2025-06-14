using UnityEngine;

[RequireComponent(typeof(Rigidbody))]                               // adds a Rigidbody component to the Awake() method call
public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isGrounded;
    private float _totalFallDistance;
    private float _previousHeight;



    private void FixedUpdate()
    {
        // Calculate fall distance
        float currentHeight = transform.position.y;
        if (currentHeight < _previousHeight)
        {
            _totalFallDistance += _previousHeight - currentHeight;
        }
        _previousHeight = currentHeight;

        // Check ground
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

        if (_isGrounded)
        {
            _totalFallDistance = 0;
            Jump();
        }
    }

    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, _jumpForce, _rb.velocity.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("BallMainLayer"))
        {
            if (other.TryGetComponent(out Segment segment))
            {
                Debug.Log($"Ball entered segment Name: {segment.name} - {_totalFallDistance}");

                Jump();
                segment.TouchedSegment(_totalFallDistance);
                _totalFallDistance = 0;
            }
        }
    }



    // TODO x debug. Show TotalFallDistance in Playmode - Game
    private void OnGUI()
    {
        var position = new Rect(10, 10, 200, 20);
        var text = "_totalFallDistance: " + _totalFallDistance.ToString("F2");
        GUI.Label(position, text);
    }

    // TODO x debug. Show _groundCheckDistance in - Scene
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var radiusBall = GetComponent<Renderer>().bounds.size.x / 2;
        var offsetGizmo = new Vector3(0, -_groundCheckDistance - radiusBall, 0);
        Gizmos.DrawLine(transform.position, transform.position + offsetGizmo);
    }
}

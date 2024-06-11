using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    [SerializeField] private float _groundDistance;
    [SerializeField] private float _rayDuration;

    private Rigidbody _rigidbody;
    private float _targetDistance;
    private bool _isGrounded;
    private Vector3 _moveDirection;
    private Vector3 _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _moveDirection = Vector3.zero;

        _targetDistance = Vector3.Distance(transform.position, _player.position);

        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, _groundMask);

        if (_targetDistance > _stopDistance)
        {
            _direction = (_player.position - transform.position).normalized;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _rayDuration, _groundMask))
            {                
                _moveDirection = Vector3.ProjectOnPlane(_direction, hit.normal).normalized * _speed;
            }
            else
            {
                _moveDirection = _direction * _speed;
            }
        }

        if (_isGrounded == false)
        {
            _moveDirection = _rigidbody.velocity + Physics.gravity * Time.deltaTime;
        }
        
        _rigidbody.velocity = _moveDirection;
    }
}
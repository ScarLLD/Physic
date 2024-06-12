using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private Vector3 _direction;
    private float _horizontalInput;
    private float _verticalInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (_characterController != null)
        {

            _direction.y = 0;
            _moveDirection = new(_horizontalInput, -1f, _verticalInput);
            _moveDirection *= _speed;
        }
        else
        {
            _direction.y += Physics.gravity.y * Time.deltaTime;
            _moveDirection.y = _direction.y;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}

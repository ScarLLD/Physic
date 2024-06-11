using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
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

        if (_characterController.isGrounded)
        {
            _moveDirection = new(_horizontalInput, 0, _verticalInput);
            _moveDirection *= _speed;
        }
        else
        {

            _moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }


        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}

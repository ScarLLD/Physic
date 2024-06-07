using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
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

        if (_characterController != null)
        {
            _moveDirection = new(_horizontalInput, 0, _verticalInput);
            _moveDirection *= _speed * Time.deltaTime;

            if (_characterController.isGrounded == false)
            {
                _moveDirection += Time.deltaTime * Physics.gravity;
            }

            _characterController.Move(_moveDirection);
        }
    }
}

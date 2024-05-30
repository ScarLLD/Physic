using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class BotMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _minTargetDistance;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        bool isWork = true;
        Vector3 tempVector = Vector3.one;

        while (isWork)
        {
            float distance = Vector3.Distance(transform.position, _playerTransform.position);

            if (distance >= _minTargetDistance)
            {
                Vector3 moveDirection = (_playerTransform.position - transform.position);

                if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _layerMask))
                {
                    moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal);
                }


                _rigidbody.MovePosition(transform.position + _speed * Time.deltaTime * moveDirection.normalized);
            }

            yield return null;
        }
    }
}

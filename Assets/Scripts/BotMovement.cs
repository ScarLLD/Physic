using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody))]
public class BotMovement : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float stoppingDistance = 2.0f;
    public float gravity = 9.81f;
    public float groundCheckDistance = 1.1f;
    public LayerMask groundMask;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // ��������� ���������� ����������
    }

    private void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // ���������� �� ������
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ��������, �� ����� �� ���
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // ���� ��� ������ �� ������, ������������ ���
        if (distanceToPlayer > stoppingDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            // �������� ����������� ����� ����� ��� �������� �������� �� ��������� �����������
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, out RaycastHit hit, 1.0f, groundMask))
            {
                Vector3 surfaceNormal = hit.normal;
                moveDirection = Vector3.ProjectOnPlane(direction, surfaceNormal).normalized * speed;
            }
            else
            {
                moveDirection = direction * speed;
            }
        }

        // ���������� ����������
        if (isGrounded)
        {
            moveDirection.y = 0; // �� ����� �� ������
        }
        else
        {
            moveDirection.y = rb.velocity.y - Physics.gravity * Time.deltaTime; // ��������� ����������
        }

        // ����������� ����
        rb.velocity = moveDirection;
    }
}
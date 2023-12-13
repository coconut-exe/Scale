using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    public Vector3 resetPosition = new Vector3(0, 1, 0); // ����λ��
    public float fallThreshold = -5f; // ������ֵ

    void Start()
    {
        // ��ȡС���Rigidbody���
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // ��ȡˮƽ�ʹ�ֱ����
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ����һ����������ʾ�ƶ�������ٶ�
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Ӧ�������ƶ�С��
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        // ���С���Ƿ���䵽ָ��λ������
        if (transform.position.y < fallThreshold)
        {
            // ����С���λ�ú��ٶ�
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = new Vector3(resetPosition.x, resetPosition.y + 2, resetPosition.z);
        }
    }
}

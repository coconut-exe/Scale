using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    public Vector3 resetPosition = new Vector3(0, 1, 0); // 重置位置
    public float fallThreshold = -5f; // 掉落阈值

    void Start()
    {
        // 获取小球的Rigidbody组件
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // 获取水平和垂直输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 创建一个向量，表示移动方向和速度
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // 应用力来移动小球
        rb.AddForce(movement * speed);
    }

    void Update()
    {
        // 检查小球是否掉落到指定位置以下
        if (transform.position.y < fallThreshold)
        {
            // 重置小球的位置和速度
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = new Vector3(resetPosition.x, resetPosition.y + 2, resetPosition.z);
        }
    }
}

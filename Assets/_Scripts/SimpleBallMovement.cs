using UnityEngine;

public class SimpleBallMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // 获取水平和垂直输入值
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 根据输入值计算新的位置
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;

        // 更新小球的位置
        transform.position += movement;
    }
}

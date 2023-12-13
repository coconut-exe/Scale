using UnityEngine;

public class SimpleBallMovement : MonoBehaviour
{
    public float speed = 5.0f;

    void Update()
    {
        // ��ȡˮƽ�ʹ�ֱ����ֵ
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // ��������ֵ�����µ�λ��
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;

        // ����С���λ��
        transform.position += movement;
    }
}

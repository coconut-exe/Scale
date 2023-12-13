using UnityEngine;

public class ScaleControl : MonoBehaviour
{
    // 缩放速度
    public float scaleSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // 如果按下W键，就放大物体
        if (Input.GetKey(KeyCode.W))
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }

        // 如果按下S键，就缩小物体
        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}

using UnityEngine;

public class ScaleControl : MonoBehaviour
{
    // �����ٶ�
    public float scaleSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // �������W�����ͷŴ�����
        if (Input.GetKey(KeyCode.W))
        {
            transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
        }

        // �������S��������С����
        if (Input.GetKey(KeyCode.S))
        {
            transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject ball;
    public Vector3 offset;

    public float sensitivity = 5.0f;  // �����Ǣͧ�����ع���ͧ
    public float distance = 5.0f;  // ������ҧ�ͧ���ͧ�ҡ ball
    public float height = 2.0f;  // �����٧�ͧ���ͧ�ҡ ball

    private float xAngle = 0.0f;
    private float yAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //offset = this.transform.position - ball.transform.position; // ������ҧ�ͧ�����С��ͧ

        // ��͹����������������ҡ���˹�Ҩ�
        Cursor.lockState = CursorLockMode.Locked;

        // ��˹����������鹢ͧ������ͧ
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = ball.transform.position + offset; // ���˹�����ͧ���ͧ

        // �ӹǳ�����ع���ͧ���������
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        xAngle += mouseX;
        yAngle -= mouseY;

        // ��˹��ͺࢵ�ͧ������ͧ
        yAngle = Mathf.Clamp(yAngle, -90f, 90f);

        // �ӹǳ���˹觢ͧ���ͧ
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0);
        Vector3 position = rotation * new Vector3(0, height, -distance) + ball.transform.position;

        // ��˹����˹���С����ع�ͧ���ͧ
        transform.rotation = rotation;
        transform.position = position;

    }
}

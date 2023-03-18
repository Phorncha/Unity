using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject ball;
    public Vector3 offset;

    public float sensitivity = 5.0f;  // ความไวของการหมุนกล้อง
    public float distance = 5.0f;  // ระยะห่างของกล้องจาก ball
    public float height = 2.0f;  // ความสูงของกล้องจาก ball

    private float xAngle = 0.0f;
    private float yAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        //offset = this.transform.position - ball.transform.position; // ระยะห่างของบอลและกล้อง

        // ซ่อนเมาส์เพื่อไม่ให้ปรากฏบนหน้าจอ
        Cursor.lockState = CursorLockMode.Locked;

        // กำหนดค่าเริ่มต้นของมุมกล้อง
        Vector3 angles = transform.eulerAngles;
        xAngle = angles.y;
        yAngle = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = ball.transform.position + offset; // ตำแหน่งใหม่ของกล้อง

        // คำนวณการหมุนกล้องโดยใช้เมาส์
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        xAngle += mouseX;
        yAngle -= mouseY;

        // กำหนดขอบเขตของมุมกล้อง
        yAngle = Mathf.Clamp(yAngle, -90f, 90f);

        // คำนวณตำแหน่งของกล้อง
        Quaternion rotation = Quaternion.Euler(yAngle, xAngle, 0);
        Vector3 position = rotation * new Vector3(0, height, -distance) + ball.transform.position;

        // กำหนดตำแหน่งและการหมุนของกล้อง
        transform.rotation = rotation;
        transform.position = position;

    }
}

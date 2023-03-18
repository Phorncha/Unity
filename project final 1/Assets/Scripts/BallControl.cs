using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 10.0f;
    public float jumSpeed;
    private float jumpForce;
    public bool OnGround;
    private int coin;
    public Text coinUI;
    public int targetScore;
    public string nextScene;
    private AudioSource coinSound;
    public float jumpHeight = 2f; // ความสูงของการกระโดด
    public float forceMagnitude = 1f;
    private Vector3 movementDirection; // เก็บเวกเตอร์เคลื่อนที่


    // Start is called before the first frame update
    void Start()
    {
        //Debug.log("Start")

        rb = this.GetComponent<Rigidbody>();
        coinSound = this.GetComponent<AudioSource>();

        coin = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.log("Update")

        if (OnGround)
        {
            // หาเวกเตอร์เคลื่อนที่จากมุมกล้อง
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0f; // ลบค่า y ออกเพื่อให้เป็นเวกเตอร์แนวราบ
            cameraRight.y = 0f;

            // คำนวณทิศทางการเคลื่อนที่จากการเคลื่อนไปข้างหน้าและข้างขวา
            Vector3 movementX = cameraRight.normalized * Input.GetAxis("Horizontal");
            Vector3 movementZ = cameraForward.normalized * Input.GetAxis("Vertical");

            // รวมเวกเตอร์เคลื่อนที่ทั้งหมด
            movementDirection = movementX + movementZ;

            // เปลี่ยนทิศทางของลูกบอลตามทิศทางเคลื่อนที่
            if (movementDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDirection);
            }
            //GetComponent<Rigidbody>().velocity = movementDirection * speed;
            rb.AddForce(movementDirection * forceMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGround) // กระโดด
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            OnGround = false;
        }
        else
        {
            //jumpForce = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) // วัตถุชนกับวัตถุ
    {
        if (collision.gameObject.tag == "Floor")
        {
            OnGround = true;
        }
    }

    private void OnTriggerEnter(Collider other) //บอลชนกับเหรียญ
    {
        if (other.gameObject.tag == "Coin")
        {
            coinSound.Play();
            Destroy(other.gameObject); // ชนแล้วให้ทำลาย
            
            coin++;
            coinUI.text = "Coin" + coin;

            targetScore--;
            if(targetScore == 0)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
        else if (other.gameObject.tag =="Boundary")
        {
            //Debug.Log("Restart game");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        

      this.transform.localScale *= 1f; // ขนาดของลูกบอลเวลาชน
    }
}

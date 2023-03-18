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
    public float jumpHeight = 2f; // �����٧�ͧ��á��ⴴ
    public float forceMagnitude = 1f;
    private Vector3 movementDirection; // ���ǡ��������͹���


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
            // ���ǡ��������͹���ҡ������ͧ
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0f; // ź��� y �͡����������ǡ�������Һ
            cameraRight.y = 0f;

            // �ӹǳ��ȷҧ�������͹���ҡ�������͹仢�ҧ˹����Т�ҧ���
            Vector3 movementX = cameraRight.normalized * Input.GetAxis("Horizontal");
            Vector3 movementZ = cameraForward.normalized * Input.GetAxis("Vertical");

            // ����ǡ��������͹��������
            movementDirection = movementX + movementZ;

            // ����¹��ȷҧ�ͧ�١��ŵ����ȷҧ����͹���
            if (movementDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(movementDirection);
            }
            //GetComponent<Rigidbody>().velocity = movementDirection * speed;
            rb.AddForce(movementDirection * forceMagnitude);
        }

        if (Input.GetKeyDown(KeyCode.Space) && OnGround) // ���ⴴ
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            OnGround = false;
        }
        else
        {
            //jumpForce = 0;
        }
    }

    private void OnCollisionEnter(Collision collision) // �ѵ�ت��Ѻ�ѵ��
    {
        if (collision.gameObject.tag == "Floor")
        {
            OnGround = true;
        }
    }

    private void OnTriggerEnter(Collider other) //��Ū��Ѻ����­
    {
        if (other.gameObject.tag == "Coin")
        {
            coinSound.Play();
            Destroy(other.gameObject); // �������������
            
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
        

      this.transform.localScale *= 1f; // ��Ҵ�ͧ�١������Ҫ�
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float JumpF;
    [SerializeField] private int JumpC;

    [Header("freelookcamera")]
    [SerializeField] List<GameObject> Camera;

    private Rigidbody rb;

    private float movex;
    private float movez;

    private float CurrentSpeed;
    private float CurrentJump;

    private GameObject currentCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentCamera = Camera[0];

        CurrentSpeed = speed;
        CurrentJump = JumpC;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        move();
    }

    private void move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentSpeed = speed * 2;
        }
        else
        {
            CurrentSpeed = speed;
        }

        movex = Input.GetAxis("Horizontal");
        movez = Input.GetAxis("Vertical");

        //andar direcao camera
        Vector3 CamF = currentCamera.transform.forward;
        Vector3 CamR = currentCamera.transform.right;

        CamF.y = 0;
        CamR.y = 0;

        Vector3 Dir = CamF * movez + CamR * movex;

        rb.linearVelocity = new Vector3(Dir.x * CurrentSpeed, rb.linearVelocity.y, Dir.z * CurrentSpeed);

        //rotacao Player
        Quaternion finalRotation = new Quaternion(0, currentCamera.transform.rotation.y, 0, currentCamera.transform.rotation.w);
        transform.rotation = finalRotation;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && CurrentJump > 0)
        {
            rb.linearVelocity = new Vector3(0, JumpF, 0);
            CurrentJump--;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CurrentJump = JumpC;
        }
    }
}

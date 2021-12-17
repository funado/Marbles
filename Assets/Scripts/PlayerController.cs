// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider SphereCol;

    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float JumpForce;
    [SerializeField]
    private float RayDistanceGround = 0.016f;
    [SerializeField]
    private LayerMask GroundLayerMask;
    [SerializeField]
    private LayerMask OutOfBoundsMask;
    [SerializeField]
    private GameObject EndUI;
    [SerializeField]
    private TextMeshProUGUI WinLossText;

    private float VVelocity;
    private float HVelocity;

    private bool CanJump = false;

    private bool HasLost = false;

    private bool dirty_;

    //Aim
    private float movespeed;
    private float dirX, dirZ;
    private float rotX, rotY = 0.0f;
    public float sensitivity;
    public float msens = 200.0f;
    public float clamp = 80.0f;

    //Shoot
    Camera cam;
    RaycastHit hitInfo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SphereCol = GetComponent<SphereCollider>();

        dirty_ = false;
    }

    private void Update()
    {
        // Checks if player is grounded
        bool CheckGrounded = IsGrounded();
        // Checks if player is outside of tracks
        bool CheckOutOfBounds = IsOutOfBounds();

        // Checks if player officially lost and shows the end UI
        if (HasLost)
        {
            EndUI.SetActive(true);
            WinLossText.text = "GAME OVER";
            return;
        }

        // Gets player input in Update and sets variables which will be used in the FixedUpdate for physics
        VVelocity = Input.GetAxisRaw("Vertical");
        HVelocity = Input.GetAxisRaw("Horizontal");

        // Checks if Jump Key was pressed, sets CanJump to true, executes jump in FixedUpdate
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CanJump = CheckGrounded;
        }

        HasLost = CheckOutOfBounds;

        if (dirty_)
        {
            List<string> pos = new List<string>();

            pos.Add(rb.position.ToString());        

            System.IO.File.WriteAllLines(Application.dataPath + "/positionFile.txt", pos);

            dirty_ = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            dirty_ = true;
        }
    }

    private void FixedUpdate()
    {
        // Player Movement/Jump handles in FixedUpdate
        Jump();
        MovePlayer();
        Aim();
    }

    private void MovePlayer()
    {
        // Uses torque to rotate the ball
        rb.AddTorque(VVelocity * MoveSpeed * 100, rb.velocity.y, -HVelocity * MoveSpeed * 100);
    }

    private void Jump()
    {
        // Checks if player can Jump, uses force to give the player impulse on Y Axis, once finished, sets CanJump to false
        if (CanJump)
        {
            Vector3 JumpVector = new Vector3(0f, JumpForce, 0f);
            rb.AddForce(JumpVector, ForceMode.Impulse);
            CanJump = false;
        }
    }
    private void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(EventSystem.current.IsPointerOverGameObject(-1))
            {
                return;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
            {
                //RaycastHit hit;
                Debug.Log("ye");
                Destroy(gameObject);
            }
        }
    }

    private void Aim()
    {
        dirX = Input.GetAxis("Horizontal") * movespeed;
        dirZ = Input.GetAxis("Vertical") * movespeed;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseY * msens * Time.deltaTime;
        rotY += mouseX * msens * Time.deltaTime;


        rotX = Mathf.Clamp(rotX, -clamp, clamp);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private bool IsGrounded()
    {
        // Uses a raycast which checks if it's currently hitting the ground layer mask, if true, player is grounded
        if (Physics.Raycast(SphereCol.bounds.center, Vector3.down, SphereCol.bounds.extents.y + RayDistanceGround, GroundLayerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsOutOfBounds()
    {
        // Uses a raycast which checks if it's currently hitting the out of bounds layer mask, if true, player has lost
        if (Physics.Raycast(SphereCol.bounds.center, Vector3.down, SphereCol.bounds.extents.y + RayDistanceGround, OutOfBoundsMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if player has passed the finishing line
        if (other.tag == "FinishLine")
        {
            EndUI.SetActive(true);
            WinLossText.text = "YOU'VE FINISHED";
        }
    }
}

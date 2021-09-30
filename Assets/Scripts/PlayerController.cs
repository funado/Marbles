using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        SphereCol = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        bool CheckGrounded = IsGrounded();
        bool CheckOutOfBounds = IsOutOfBounds();

        if (HasLost)
        {
            EndUI.SetActive(true);
            WinLossText.text = "GAME OVER";
            return;
        }

        VVelocity = Input.GetAxisRaw("Vertical");
        HVelocity = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CanJump = CheckGrounded;
        }

        HasLost = CheckOutOfBounds;
    }

    private void FixedUpdate()
    {
        Jump();
        MovePlayer();
    }

    private void MovePlayer()
    {
        rb.AddTorque(VVelocity * MoveSpeed * 100, rb.velocity.y, -HVelocity * MoveSpeed * 100);
    }

    private void Jump()
    {
        if (CanJump)
        {
            Vector3 JumpVector = new Vector3(0f, JumpForce, 0f);
            rb.AddForce(JumpVector, ForceMode.Impulse);
            CanJump = false;
        }
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(SphereCol.bounds.center, Vector3.down * (SphereCol.bounds.extents.y + RayDistanceGround));

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
        Debug.DrawRay(SphereCol.bounds.center, Vector3.down * (SphereCol.bounds.extents.y + RayDistanceGround));

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
        if (other.tag == "FinishLine")
        {
            EndUI.SetActive(true);
            WinLossText.text = "YOU'VE FINISHED";
        }
    }
}

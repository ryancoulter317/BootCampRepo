using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPhys : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 150f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 10.0f;
    [SerializeField] float modifierSpeed = 10.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float jumpForce = 10.0f;

    float cameraPitch = 0.0f;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;
    public Rigidbody playerBody;
    public Collider floorCollider;
    public bool isGrounded = true;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        UpdateMouseLook();
        UpdateMovement();
    }

    void UpdateMouseLook()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        cameraPitch -= mouseDelta.y * mouseSensitivity * Time.deltaTime; //inverted for invertlook
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        //X AXIS
        transform.Rotate(Vector3.up * mouseSensitivity * Time.deltaTime * mouseDelta.x);

        //Y AXIS
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
    }
    void OnCollisionEnter()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    void UpdateMovement()
    {
        //Walking
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); //dont make diagonal movement faster
        
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Jumping
        if (isGrounded && Input.GetAxisRaw("Jump") != 0f && playerBody.velocity.y <4) //able to jump and the button is held AND y velocity isnt over 10
            playerBody.AddForce(transform.up * jumpForce);

        //Moving
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed;

        //playerBody.MovePosition(transform.position + velocity * Time.deltaTime);
        playerBody.AddForce(velocity * Time.deltaTime * modifierSpeed / Mathf.Clamp(Mathf.Round((Vector3.Magnitude(playerBody.velocity))), 1.0f, 5.0f));

        Debug.Log(Mathf.Clamp(Mathf.Round((Vector3.Magnitude(playerBody.velocity))),1.0f,5.0f));
    }
}
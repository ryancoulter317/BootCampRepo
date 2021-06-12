using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 150f;
    [SerializeField] bool lockCursor = true;
    [SerializeField] float walkSpeed = 10.0f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] float jumpHeight = 10.0f;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if(lockCursor)
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

    void UpdateMovement()
    {
        //Gravity
        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        //Walking
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize(); //dont make diagonal movement faster

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        //Jumping
        if (controller.isGrounded && Input.GetAxisRaw("Jump") != 0f) //able to jump and the button is held
            velocityY += Mathf.Sqrt(jumpHeight * -1 * gravity);

        //Moving
        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private Transform cam;

    [SerializeField] private float speed = 6f;

    [SerializeField] private float turnSmoothDelay = 0.1f;

    [SerializeField] private float jumpHeight = 1f;

    private const float gravityValue = -9.81f;
    private float turnSmoothVelocity;
    private bool isPlayerOnGround;
    private Vector3 playerJumpVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        isPlayerOnGround = controller.isGrounded;

        if (isPlayerOnGround && playerJumpVelocity.y < 0f)
        {
            playerJumpVelocity.y = 0f;
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothDelay);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isPlayerOnGround)
        {
            playerJumpVelocity.y += Mathf.Sqrt(jumpHeight * -3f * gravityValue);
        }

        playerJumpVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerJumpVelocity * Time.deltaTime);
    }
}

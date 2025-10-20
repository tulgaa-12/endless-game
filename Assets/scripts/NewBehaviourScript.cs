using System.Collections;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed = 10f;

    private int desiredLane = 1;
    public float laneDistance = 4;

    public float jumpForce;
    public float gravity = -20;


   
    public float maxSpeed;

    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (!playerManager.isGameStarted)
            return;
        // Increase forward speed over time
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;



        animator.SetBool("isGameStart", true);
        direction.z = forwardSpeed;

        animator.SetBool("isGrounded", controller.isGrounded);



        if (controller.isGrounded)
        {
            if (direction.y < 0)
                direction.y = -1;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }

        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Slide());
        }



        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            desiredLane++;
            if (desiredLane > 2)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if (desiredLane < 0)
                desiredLane = 0;
        }


        Vector3 targetPosition = transform.position.z * Vector3.forward + transform.position.y * Vector3.up;

        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;


        Vector3 moveDir = targetPosition - transform.position;
        moveDir.y = 0;
        controller.Move(moveDir * 10 * Time.deltaTime + direction * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            playerManager.gameover = true;
           FindObjectOfType<AudioManager>().PlaySound("Gameover");
        }

    }

    private IEnumerator Slide()
    {
        animator.SetBool("isSlide", true);
        controller.center = new Vector3(0, -0.5f,0);
        controller.height = 1f;
        yield return new WaitForSeconds(1.3f);
        controller.height = 2;
        animator.SetBool("isSlide", false);
    }
}

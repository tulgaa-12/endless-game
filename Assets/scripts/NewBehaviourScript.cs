using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Slide Settings")]
    public float slideHeight = 1.0f;
    public float slideCenterY = 0.5f;
    public float slideDuration = 0.5f;
    public float slideSpeedMultiplier = 1.5f;
    private float originalHeight;
    private Vector3 originalCenter;
    private bool isSliding = false;

   

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed = 10f; 
    public float jumpForce = 10f;
    public float gravity = -20f;
    public float followSpeed = 10f;
    public float limitX = 3f;
    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        originalHeight = controller.height;
        originalCenter = controller.center;

        
    }

    void Update()
    {
        if (!playerManager.isGameStarted)
            return;

        playerManager.currentScore = (int)transform.position.z;
        animator.SetBool("isGameStart", true);

        

        if (controller.isGrounded)
        {
            if (direction.y < 0)
                direction.y = -1;

           
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
            }
         
            else if (Input.GetMouseButtonDown(1) && !isSliding)
            {
                StartCoroutine(Slide());
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }

       
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        float targetX = Mathf.Clamp(worldPos.x, -limitX, limitX);
        float horizontalSpeed = (targetX - transform.position.x) * followSpeed;

        float currentForwardSpeed = isSliding ? forwardSpeed * slideSpeedMultiplier : forwardSpeed;

     
        Vector3 moveVector = new Vector3(horizontalSpeed, direction.y, currentForwardSpeed);

        
        controller.Move(moveVector * Time.deltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
        animator.SetTrigger("Jump");
    }

    private IEnumerator Slide()
    {
        isSliding = true;

        animator.SetBool("isSliding", true);

 
        controller.height = slideHeight;
        controller.center = new Vector3(originalCenter.x, slideCenterY, originalCenter.z);

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        controller.center = originalCenter;

        animator.SetBool("isSliding", false);
        isSliding = false;
    }

 

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.CompareTag("Obstacle"))
        {
            playerManager.gameover = true;
            FindObjectOfType<AudioManager>().PlaySound("Gameover");
        }
    }
}
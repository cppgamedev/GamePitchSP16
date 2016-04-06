using UnityEngine;
using System.Collections;
using Prime31;


public class PlayerInput : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	public bool dash;
    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;
    
    private bool jumped = false;
    private CharacterController2D controller;
    private Animator animator;
    private RaycastHit2D lastControllerColliderHit;
    private Vector3 velocity;
    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();

        
    }
    void Update()
        //Double jumping/dash additions
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dash = true;
        }
        else
        {
            dash = false;
        }

        if (controller.isGrounded)
            velocity.y = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (dash && controller.isGrounded)
            {
                normalizedHorizontalSpeed = 2;
            }
            else
            {
                normalizedHorizontalSpeed = 1;
            }
            if (transform.localScale.x < 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (controller.isGrounded)
                animator.Play(Animator.StringToHash("Run"));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (dash && controller.isGrounded)
            {
                normalizedHorizontalSpeed = -2;
            }
            else
            {
                normalizedHorizontalSpeed = -1;
            }
            if (transform.localScale.x > 0f)
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            if (controller.isGrounded)
                animator.Play(Animator.StringToHash("Run"));
        }
        else
        {
            normalizedHorizontalSpeed = 0;

            if (controller.isGrounded)
                animator.Play(Animator.StringToHash("Idle"));
        }


        // we can only jump whilst grounded
        if (controller.isGrounded && jumped == true)
        {
            jumped = false;
        }
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumped = true;
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            animator.Play(Animator.StringToHash("Jump"));
            
        }
        if (controller.isGrounded == false && jumped == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            animator.Play(Animator.StringToHash("Jump"));
            jumped = false;
        }
       
       
        

        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        velocity.x = Mathf.Lerp(velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving
        velocity.y += gravity * Time.deltaTime;

        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets uf jump down through one way platforms
        if (controller.isGrounded && Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y *= 3f;
            controller.ignoreOneWayPlatformsThisFrame = true;
        }

        controller.move(velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        velocity = controller.velocity;
    }

}

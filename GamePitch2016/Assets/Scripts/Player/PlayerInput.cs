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

	//Custom Added
	public int jumpsAllowed = 1;
	public float dashDistance = 3f;

	[HideInInspector]
	private float HorzSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;

	private int jumpCount = 0;
	private float dashCooldown = 0;
	private float dashDistanceThisFrame = 1f;
    private bool isCroched = false;

    private Inventory inventory;
    private PlayerStats playerStats;

    void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;

        inventory = this.gameObject.transform.Find("Inventory").GetComponent<Inventory>();
        playerStats = this.gameObject.GetComponent<PlayerStats>();
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;
		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
        
        if(col.gameObject.tag == "Item")
        {
            //Gets the item id from the item world object returns true if add to inventory
            if(inventory.addItem(col.gameObject.GetComponent<ItemPickup>().getItemID()))
            {
                //destroys object if successfuly add to inventory.
                col.gameObject.GetComponent<ItemPickup>().destroyItem();
            }
        }
        else if (col.gameObject.tag == "Enemy")
        {
            Damage enemy = col.gameObject.GetComponent<Damage>();
            playerStats.removeHealth(enemy.getDamage());
            if(col.gameObject.transform.position.x - 
                this.gameObject.transform.position.x > 0)
            {
                _controller.move(new Vector3(-enemy.getKnockBackX(),
                    enemy.getKnockBackY(), 0));
            }
            else
            {
                _controller.move(new Vector3(enemy.getKnockBackX(),
                    enemy.getKnockBackY(), 0));
            }
            
        }
        else
        {
            Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
        }
	}


	void onTriggerExitEvent( Collider2D col )
	{
	}

	#endregion


	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
	{
		if (_controller.isGrounded)
		{
			_velocity.y = 0;
			jumpCount = 0;
			dashCooldown = 0;
            _animator.SetBool("Falling", false);
        } 
		else if (_controller.isWalled) //Reset for double jump
		{
			jumpCount = 0;
		}

        if (Input.GetButton("Horizontal"))
		{
            HorzSpeed = Input.GetAxis("Horizontal");
            if (transform.localScale.x < 0f && HorzSpeed > 0f ||
                transform.localScale.x > 0f && HorzSpeed < 0f)
                transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

			if (_controller.isGrounded)
            {
                if (!isCroched)
                    _animator.Play(Animator.StringToHash("Run"));
                else
                    _animator.Play(Animator.StringToHash("Crawl"));
            }
		}

        else 
		{
            HorzSpeed = 0;
			if (_controller.isGrounded)
            {
                if(!isCroched)
					_animator.Play (Animator.StringToHash ("Idle"));
                else
                    _animator.Play(Animator.StringToHash("Idle Crawl"));
            }
                
		}

		if (Input.GetButtonDown("Sprint"))
        {
			if (dashCooldown <= 0 && dashDistanceThisFrame <= 1) 
			{
				dashDistanceThisFrame = dashDistance;
				dashCooldown = 5f;
			}
		}

		if (dashCooldown > 0)
			dashCooldown -= 1 * Time.deltaTime;

		// we can only jump whilst grounded OR belew set double Jump limit
		if ((jumpCount < jumpsAllowed) && Input.GetButtonDown("Jump") &&
            !Input.GetButton("Crouch")) 
        {
            ++jumpCount;
			_velocity.y = Mathf.Sqrt (2f * jumpHeight * -gravity);
            _animator.SetBool("Falling", true);
			_animator.Play (Animator.StringToHash ("Jump"));
           
		}
			
		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?

		_velocity.x = Mathf.Lerp( _velocity.x * dashDistanceThisFrame, HorzSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		if (dashDistanceThisFrame > 1)
			dashDistanceThisFrame--;

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets uf jump down through one way platforms
		if( _controller.isGrounded && Input.GetButton("Crouch"))
        {
            if (!isCroched)
            {
                isCroched = true;
                _animator.Play (Animator.StringToHash("Duck"));
            }
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}
        else
        {
            isCroched = false;
        }

		_controller.move( _velocity * Time.deltaTime );

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}

}

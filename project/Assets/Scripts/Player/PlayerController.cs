using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour {

	// STATE
	bool grounded;
	private bool facingRight = true;


	// MOVEMENT
	public float walkSpeed;
	public float jumpInitialForce;
	private Vector2 speed = new Vector2();
	private bool doubleJumpEnabled = false;
	private float jumpSpeed;
	private bool jumpPressed;
	private bool prevPressed;


	// COLLISION
	private BoxCollider2D box;
	public LayerMask colMask;

	// HEALTH
    public int maxHealth;
    private int currentHealth;
	
	// ANIMATION
	Animator anim;

	// CAM
	public Camera cam;

	// LIGHT
	private float angle = 0;

	public float groundRadius = 100f;
    public LayerMask bouncerLayer;
    public LayerMask enemyLayer;
    private CircleCollider2D circle;
	private bool bouncejump = false;
    private SpriteRenderer srend;
	public GameObject playerSpawn;
	int jumps = 0;
	int maxjumps = 0;

    public float startImmuneTimer;
    float immuneTimer=0;

	public Transform groundCheck;
	public float nextJumpMinYSpeed;
	private Vector2 jumpForce;
    private AudioSource hurtSfx;
    private float bounceForce;


	private int immuneActive;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        immuneActive = 1;
		anim = GetComponent<Animator>();
		jumpForce = new Vector2( 0, jumpInitialForce );
        srend = this.GetComponent<SpriteRenderer>();
        hurtSfx = GetComponent<AudioSource>();
	}

	void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "LIVES " + currentHealth);
    }

	void Update(){

		if( immuneTimer > 0 ) immuneTimer -= Time.deltaTime;
        
        if( immuneActive != 0 && immuneTimer <= 0 )
        {
            immuneActive = 0;
        }



        Color c = srend.color;
		if( immuneActive == 1 )
        {
            c.a = 0.5f*Mathf.Sin(immuneTimer * 10);
        }
        else
        {
            c.a = 1f;
        }
		srend.color = c;

        if( (grounded || jumps > 0 ) /* && grounded */ && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))) {
            rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce (jumpForce);
			jumps--;
		}

		if( bouncejump )
		{
			Debug.Log("LETS ROCK!");
			bouncejump = false;
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddForce(new Vector2(0, bounceForce ));
		}

        Debug.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x + groundRadius, groundCheck.position.y ), Color.red, 20, false);

	}

	void OnTriggerEnter2D( Collider2D other )
	{
		//Debug.Log("try");
		if( other.gameObject.GetComponent<Item>() != null )
		{
			Item it = other.gameObject.GetComponent<Item>();
			switch( it.itemType )
			{
				case Item.ItemType.EnableDoubleJump:
					Destroy( other.gameObject );
					Debug.Log("DOUBLE JUMP!");
					maxjumps++;
					break;
				case Item.ItemType.Spikes:
					DealDamage(maxHealth - currentHealth+1);
					Debug.Log("AMDED!!");
					break;
			}
		}
		else if( ((1<<other.gameObject.layer) & bouncerLayer ) != 0)
		{
			Debug.Log("HEY!");
			bouncejump = true;
            bounceForce = other.GetComponent<Bouncer>().force;
		}
		else if( ((1<<other.gameObject.layer) & enemyLayer) != 0 && immuneActive == 0 )
        {
			Debug.Log("current: " + immuneActive);
			//Debug.Log("SHOCA");
            DealDamage(1);
            Debug.Log("next: " + immuneActive);
		}
	}

	private void DealDamage( int qtt )
    {
		immuneTimer = startImmuneTimer;
        Debug.Log("DEALIN!");
		currentHealth = currentHealth - qtt;
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.AddForce(new Vector2(0, 400));
		if (this.currentHealth < 0)
		{
			 Application.LoadLevel("Gameplay");
		}
        immuneActive = 1;
        Color c = srend.color;
		c.a = 0.5f;
		srend.color = c;
        hurtSfx.Play();

    }

	public bool FacingLeft()
	{
		return !facingRight;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{

		// CHECK GROUND
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, colMask);

		if( grounded )
		{
			jumps = maxjumps;
		}

		// HORIZONTAL MOVE
		speed.x = 0;
		if (Input.GetKey (KeyCode.D) ) {//|| Input.GetAxis("Horizontal") > 0) {
			speed.x = walkSpeed;
		} else if (Input.GetKey (KeyCode.A) ) {// || Input.GetAxis("Horizontal") < 0) {
			speed.x = -walkSpeed;
		}

		speed.y = rigidbody2D.velocity.y;

		rigidbody2D.velocity = speed;

		// GRAPHICS
		if( speed.x > 0 && !facingRight ) Flip();
		else if( speed.x < 0 && facingRight) Flip();

		anim.SetFloat("Speed", Mathf.Abs(speed.x));
		anim.SetBool("Grounded", grounded);

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

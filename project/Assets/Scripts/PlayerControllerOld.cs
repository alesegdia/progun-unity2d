using UnityEngine;
using System.Collections;

/*

// tipo de daño que hace el arma
public enum DamageType
{
	Projectile,
	Blunt,
	Slash,
	Magic
}


// forma de activar el ataque
public interface ITriggerType { bool TryAttack(); }

// tipo de fisicas del arma
public interface IWeaponType { void DoAttack(); }


//	OnePress,		// pulsando una vez
//	Continued,		// dejandolo pulsado
//	Strike,			// dejandolo pulsado, rafagas de n
//	Combo
[System.Serializable]
public class OnePress : ITriggerType
{
	public float minWaitTime;
	bool click;
	public bool TryAttack()
	{
		if( Input.GetKey( KeyCode.Space ) )
		{
			return true;
		}
		return false;
	}
};

[System.Serializable]
public class ContinuePress : ITriggerType
{
	public float rateOfAttack;
	public bool TryAttack()
	{
		return true;
	}
};

[System.Serializable]
public class StrikePress : ITriggerType
{
	public float rateOfStrike;
	public float timeGap;
	public float attacksPerStrike;

	public bool TryAttack()
	{
		return true;
	}
};

[System.Serializable]
public class ChargedAttack : ITriggerType
{
	public float timeToCharge;

	public bool TryAttack()
	{
		return true;
	}

};




//	Throw,			// de proyectil
//	Hit				// de collider
[System.Serializable]
public class Throw : IWeaponType
{

	public GameObject thrownObject;

	public void DoAttack()
	{

	}

}

[System.Serializable]
public class Hit : IWeaponType
{

	public GameObject hittingObject;

	public void DoAttack()
	{

	}

}

// para construir un arma
[System.Serializable]
public class Weapon<TT,WT>
{
	public DamageType damageType;
	public TT triggerType;
	public WT weaponType;
}

[System.Serializable]
public class Knife : Weapon<OnePress, Hit> {}
[System.Serializable]
public class Dagger : Weapon<StrikePress, Throw> {}
[System.Serializable]
public class DualSwords : Weapon<StrikePress, Hit> {}
[System.Serializable]
public class FuryFists : Weapon<ContinuePress, Hit> {}
[System.Serializable]
public class BigFuckingFireball : Weapon<OnePress, Throw> {}
[System.Serializable]
public class IceShards : Weapon<ContinuePress, Throw> {}
[System.Serializable]
public class LightingRod : Weapon<StrikePress, Hit> {}

[System.Serializable]
public class Weapons
{
	public Knife knife;
	public Dagger dagger;
	public DualSwords dualSwords;
	public FuryFists furyFists;
	public BigFuckingFireball bigFuckingFireball;
	public IceShards iceShards;
	public LightingRod lightingRod;
}
*/

public class PlayerControllerOld : MonoBehaviour {

	//public Weapons weapons;

	// MOVEMENT
	public float walkSpeed;
	public float jumpInitialForce;
	private float x_move;

	// COLLISION
	private BoxCollider2D box;
	public LayerMask colMask;

	// ANIMATION
	Animator anim;

	// CAM
	public Camera cam;

	// LIGHT
	public Light light;
	private float angle = 0;


	private bool grounded;
	private bool facingRight = true;

	private float jumpSpeed;
	private bool jumpPressed;
	private bool prevPressed;
	public float groundRadius = 0.2f;
	private CircleCollider2D circle;

	public Transform groundCheck;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update(){
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		// JUMP
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, colMask);
		if( grounded && Input.GetKeyDown(KeyCode.W)) {
			rigidbody2D.AddForce (new Vector2(0, jumpInitialForce ));
		}

		// HORIZONTAL MOVE
		x_move = 0;
		if (Input.GetKey (KeyCode.D)) {
			x_move = walkSpeed;
		} else if (Input.GetKey (KeyCode.A)) {
			x_move = -walkSpeed;
		}
		rigidbody2D.velocity = new Vector3( x_move, rigidbody2D.velocity.y );

		// GRAPHICS
		if( x_move > 0 && !facingRight ) Flip();
		else if( x_move < 0 && facingRight) Flip();
		anim.SetFloat("Speed", Mathf.Abs(x_move));

	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

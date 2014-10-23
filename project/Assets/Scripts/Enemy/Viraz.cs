using UnityEngine;
using System.Collections;

public class Viraz : MonoBehaviour {

	public LayerMask limitLayer;
	Vector2 speed = new Vector2( 4, 0 );
	Health health;

	// Use this for initialization
	void Start () {
		health = GetComponent<Health>();
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if( ((1<<other.gameObject.layer) & limitLayer) != 0)
		{
			speed.x = -speed.x;
		}
	}

	// Update is called once per frame
	void Update () {
		speed.y = rigidbody2D.velocity.y;
		rigidbody2D.velocity = speed;
		if( health.dead ) Destroy( this.gameObject );
	}
}

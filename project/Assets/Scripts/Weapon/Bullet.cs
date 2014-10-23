using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int weapon;
	public float power;
	public bool on = true;

	public float timer;
	Weapons weapons;

	// Use this for initialization
	void Start () {
		weapons = (GameObject.FindGameObjectsWithTag("weapon")[0]).GetComponent<Weapons>();
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		Health h = other.gameObject.GetComponent<Health>();
		if( h != null ) {
			h.current -= power;
            if (h.current <= 0) h.dead = true;
		}
		on = false;
	}

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if( timer <= 0 || !on )
		{
			weapons.NotifyBulletOff( weapon, this );
		}
	}

}

using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	public GameObject[] bulletPrefabs = new GameObject[3];
	public GameObject[] weapons = new GameObject[3];
	public float[] bulletSpeed = new float[3];
	public float[] bulletRate = new float[3];
	public float[] bulletLifetime = new float[3];
	public float[] bulletPower = new float[3];
	public GameObject shotPoint;
	public float shotgunCone;
	public float shotgunHalfExtraBullets;

	PlayerController pc;
	Stack[] pools = new Stack[3];
	float lastShot = 0;
	int currentWeapon = -1;
    AudioSource currentSfx;

	// Use this for initialization
	void Start () {
		SelectWeapon(0);
		pc = transform.parent.gameObject.GetComponent<PlayerController>();
		for( int i = 0; i < 3; i++ )
		{
			pools[i] = new Stack();
			for( int j = 0; j < 100; j++ )
			{
				GameObject bullet = ((GameObject)Instantiate( bulletPrefabs[i] ));
				bullet.SetActive(false);
				pools[i].Push( bullet.GetComponent<Bullet>() );
			}
		}
	}

	void SelectWeapon( int id )
	{
		if( currentWeapon != id )
		{
			currentWeapon = id;
			for( int i = 0; i < 3; i++ )
			{
                if (i == id) {
                    currentSfx = weapons[i].GetComponent<AudioSource>();
                    weapons[i].active = true;
                } else weapons[i].active = false;
			}
		}
	}

	void NextWeapon()
	{
		currentWeapon = (currentWeapon+1)%3;
		SelectWeapon( currentWeapon );
	}

	void PrevWeapon()
	{
		currentWeapon--;
		if( currentWeapon < 0 ) currentWeapon = 2;
		SelectWeapon( currentWeapon );
	}

	void Shot( )
	{
        currentSfx.Play();
		Shot_( 0f, 0f );
		if( currentWeapon == 2 )
		{
			int num_balas_lado = 2;
			for( float i = 0; i < shotgunHalfExtraBullets; i++ )
			{
				Shot_( 0f, 0.2f, shotgunCone/(i+1) );
				Shot_( 0f, -0.2f, -(shotgunCone/(i+1)) );
			}
		}
	}

	void Shot_( float offx, float offy, float angle=0 )
	{
		Debug.Log("currentweapon: " + currentWeapon );
		Bullet bullet = ((Bullet)pools[currentWeapon].Pop());
		bullet.gameObject.SetActive(true);
		bullet.on = true;
		bullet.power = bulletPower[currentWeapon];
		bullet.timer = bulletLifetime[currentWeapon];
		bullet.weapon = currentWeapon;
		Vector2 aux = new Vector2( offx + shotPoint.transform.position.x, offy + shotPoint.transform.position.y );
		bullet.gameObject.transform.position = aux;
		bullet.rigidbody2D.velocity = new Vector2( bulletSpeed[currentWeapon] * (pc.FacingLeft() ? -1 : 1), angle );
	}

	public void NotifyBulletOff( int weapon, Bullet bullet )
	{
		bullet.gameObject.SetActive(false);
		pools[weapon].Push( bullet );
	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			SelectWeapon(0);
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			SelectWeapon(1);
		} else if( Input.GetKeyDown( KeyCode.Alpha3 ) ) {
			SelectWeapon(2);
		}

		lastShot -= Time.deltaTime;
		if( Input.GetKey( KeyCode.Space ) )
		{
			if( lastShot <= 0 )
			{
				Shot();
				lastShot = bulletRate[currentWeapon];
			}
		}
	}
}

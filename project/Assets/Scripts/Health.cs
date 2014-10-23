using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float current = 3;
	public bool dead = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if( current <= 0 ) dead = true;
	}
}

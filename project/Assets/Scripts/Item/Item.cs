using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

	public enum ItemType {
		PistolAmmo,
		ShotgunAmmo,
		RepeaterAmmo,
		EnableDoubleJump,
		Spikes
	}

	public ItemType itemType;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}
}

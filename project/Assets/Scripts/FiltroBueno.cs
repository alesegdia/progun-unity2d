using UnityEngine;
using System.Collections;

public class FiltroBueno : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTexture.filterMode = FilterMode.Point;
	}
}

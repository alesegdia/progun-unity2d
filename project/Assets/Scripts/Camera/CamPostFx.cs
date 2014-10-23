using UnityEngine;
using System.Collections;

public class CamPostFx : MonoBehaviour {

	public Material mat;

	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
	
	}

	void OnRenderImage( RenderTexture src, RenderTexture dst )
	{
		Graphics.Blit (src, dst, mat);
	}
}

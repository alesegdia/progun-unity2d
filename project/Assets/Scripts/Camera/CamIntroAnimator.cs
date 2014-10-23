using UnityEngine;
using System.Collections;

public class CamIntroAnimator : MonoBehaviour {

    private float timer;
    private float zinit;
    private float prev;
    private float amp = 0.5f;

	// Use this for initialization
	void Start () {
        timer = 0;
        zinit = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        Vector3 pos = transform.position;
        pos.z = zinit + 2*Mathf.Sin(timer*5);
        transform.position = pos;

        prev = 90 * Mathf.Sin(timer * 3);
        float current = amp * Mathf.Sin(timer * 3);
        transform.Rotate(Vector3.forward, current, Space.World);
	}
}

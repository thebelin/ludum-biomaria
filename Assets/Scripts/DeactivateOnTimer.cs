using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnTimer : MonoBehaviour {
	public float delayTime = 1;
	public bool destroy = false;

	private float awakeTime;

	// Use this for initialization
	void Awake () {
		awakeTime = Time.time;	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (awakeTime + delayTime > Time.time)
			return;

		if (destroy)
			Destroy (gameObject);
		else
			gameObject.SetActive (false);

	}
}

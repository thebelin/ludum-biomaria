using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {
	public KeyCode[] left;
	public KeyCode[] right;
	public KeyCode[] accelerate;
	public KeyCode[] reverse;

	public float turnSpeed = 1;
	public float accelSpeed = 1;
	public float reverseSpeed = 1;

	private int turning;
	private bool accelerating;
	private bool reversing;

	private Rigidbody rbody;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		foreach (KeyCode key in left)
			if (Input.GetKeyDown (key)) {
				Turn (-1);
				break;
			}

		foreach (KeyCode key in right)
			if (Input.GetKeyDown (key)) {
				Turn (1);
				break;
			}
		foreach (KeyCode key in left)
			if (Input.GetKeyUp (key)) {
				Turn (0);
				break;
			}

		foreach (KeyCode key in right)
			if (Input.GetKeyUp (key)) {
				Turn (0);
				break;
			}

		foreach (KeyCode key in accelerate)
			if (Input.GetKeyDown (key)) {
				StartAccelerate();
				break;
			}

		foreach (KeyCode key in accelerate)
			if (Input.GetKeyUp (key)) {
				StopAccelerate();
				break;
			}

		foreach (KeyCode key in reverse)
			if (Input.GetKeyDown (key)) {
				reversing = true;
				break;
			}

		foreach (KeyCode key in reverse)
			if (Input.GetKeyUp (key)) {
				reversing = false;
				break;
			}

		// perform any turn that is specified
		transform.Rotate (0f, turning * Time.deltaTime * turnSpeed, 0f);

		// Perform acceleration as specified
		if (accelerating)
			rbody.AddForce (transform.rotation * Vector3.forward * accelSpeed);
		if (reversing)
			rbody.AddForce (transform.rotation * Vector3.back * reverseSpeed);

	}

	void StartAccelerate ()
	{
		accelerating = true;
	}

	void StopAccelerate()
	{
		accelerating = false;
	}

	void Turn (int dir)
	{
		turning = dir;
	}
}

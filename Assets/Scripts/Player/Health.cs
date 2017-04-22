using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
	float max;
	float current;

	public GameObject[] toDestroy;
	public GameObject[] onDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Kill()
	{
		// kill the item
		foreach (GameObject td in toDestroy)
			td.SetActive (false);

		foreach (GameObject od in onDestroy)
			od.SetActive (true);
	}

	public void Hurt(float damage)
	{
		current -= damage;
		if (current <= 0)
			Kill ();
	}
}

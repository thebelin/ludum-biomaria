using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace biomaria {
	public class Health : MonoBehaviour {
		public float max;
		public float current;

		public GameObject[] toDestroy;
		public GameObject[] onDestroy;
		public GameObject[] onDamage;
		public GameObject[] onHeal;

		private Collider col;
		private QuorumController qc;

		// Use this for initialization
		void Start () {
			col = GetComponent<Collider> ();	
			col.enabled = true;
			qc = FindObjectOfType<QuorumController> ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void Kill()
		{
			// Disable the collider
			if (col)
				col.enabled = false;

			// kill the item
			foreach (GameObject td in toDestroy)
				td.SetActive (false);

			foreach (GameObject od in onDestroy)
				od.SetActive (true);
		}
		public void Hurt(float damage)
		{
			current -= damage;
			foreach (GameObject od in onDamage) {
				od.SetActive (true);
			}
			if (current <= 0)
				Kill ();
		}

		public void Hurt(float damage, Vector3 location)
		{
			current -= damage;
			foreach (GameObject od in onDamage) {
				od.transform.position = location;
				od.SetActive (true);
			}
			if (current <= 0)
				Kill ();
		}
		public void Hurt(float damage, Vector3 location, Quaternion rot)
		{
			current -= damage;
			foreach (GameObject od in onDamage) {
				od.transform.position = location;
				od.transform.rotation = rot;
				od.SetActive (true);
			}
			if (current <= 0)
				Kill ();
		}

		public void Heal(float amount)
		{
			current += amount;
			foreach (GameObject oh in onHeal) {
				oh.SetActive (true);
			}
		}

		public void Regenerate()
		{
			
		}
	}
}
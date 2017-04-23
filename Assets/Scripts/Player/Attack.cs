using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace biomaria {
	
	public class Attack : MonoBehaviour {

		public float power = 1f;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionEnter(Collision col)
		{
			Control localCon = GetComponentInParent<Control> ();
			Control con = col.collider.gameObject.GetComponentInParent<Control> ();
			Debug.Log ("localCon :" + localCon + " con:" + con);
			if (localCon != null && con != null) {
				
				return;
			}
			// If the target collided with has health, then do damage to it
			Health health = col.collider.gameObject.GetComponent<Health>();
			if (health != null) {
				Debug.Log ("laying the hurt" + power);
				health.Hurt (power, col.transform.position, col.transform.rotation);
			}
		}
	}

}
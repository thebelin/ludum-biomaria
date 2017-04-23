using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace biomaria {
	public class MoveTowardsPlayer : MonoBehaviour {
		public float speed;
		public float detectionRadius;


		private float lastTime;
		private float delayTime = .5f;
		private GameObject target;

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void LateUpdate () {
			if (Time.time > lastTime + delayTime) {
				target = null;
				lastTime = Time.time;
				Collider[] targets = Physics.OverlapSphere(transform.position, detectionRadius);

				//Bacteria[] bacs = Physics.OverlapSphere(transform.position, detectionRadius, out bacs, )
				foreach (Collider th in targets) {
					if (!th.gameObject.Equals (gameObject)) {
						Health thisHealth = th.gameObject.GetComponent<Health> ();
						if (thisHealth != null) {
							target = th.gameObject;
							return;
						}
						Food thisFood = th.gameObject.GetComponent<Food> ();
						if (thisFood != null) {
							target = th.gameObject;
							return;
						}
					}
				}
			}
			if (target == null)
				return;
			// move towards any current target at whatever speed
			transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
		}
	}
}

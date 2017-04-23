using UnityEngine;
namespace biomaria {
	[RequireComponent (typeof(Rigidbody))]
	public class Food : MonoBehaviour {
		public float value = 1;

		public GameObject replacement;

		private Rigidbody rbody;

		// Use this for initialization
		void Start () {
			rbody = GetComponent<Rigidbody> ();
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnCollisionEnter(Collision col)
		{
			Debug.Log ("collision" + col.collider.name);
			// Get the bacteria of the collision
			Bacteria colBac = col.collider.gameObject.GetComponent<Bacteria>();

			if (colBac != null && colBac.eatRatio > 0) {
				Debug.Log ("bacteria" + colBac.Name);
				colBac.Feed (value);
				Destroy (gameObject, 2);
				if (replacement != null) {
					replacement.SetActive (true);
					replacement.transform.parent = null;
				}
				gameObject.SetActive (false);
			}
		}

	}
}
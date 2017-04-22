using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace biomaria {
	public class Quorum : MonoBehaviour {
		// The bacteria which this player is made from
		public Bacteria [,] bacs;

		// The maximum quorum the player can support
		public int maxQuorum;

		// The current quorum value
		public int currentQuorum;

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void LateUpdate () {
			
		}
		public void LoadBacteria(Bacteria[,] newBacs)
		{
			bacs = newBacs;
			foreach (Bacteria bac in bacs) {
				// Place the bacteria prefab gameObject in the desired relative space
				GameObject thisBac = Instantiate(bac.gameObject, transform);
				thisBac.transform.localPosition = new Vector3 (2 - bac.row, 0, 2 - bac.col);
			}
		}

		public void BacteriaKilled (Bacteria killed)
		{
			// remove the bacteria specified
			bacs[killed.col, killed.row] = null;

			// Examine each bacs and make sure it's connected to other bacteria
			// if it isn't, then destroy it
			foreach (Bacteria bac in bacs)
				if (isAlone (bac))
					bac.GetComponent<Health> ().Kill ();
		}

		private bool isAlone(Bacteria bac)
		{
			// If there's a non null item in a row that is the same and 1 col away from col
			// return true;

			// If there's a non null item in a col that is the same and 1 row away from row
			// return true;

			return false;
		}
	}
}

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
//			if (bacs != null)
//				for (int i = bacs.GetLength(0) - 1; i >= 0; i--)
//					for (int h = bacs.GetLength(1) - 1; h >= 0; h--)
//						if (bacs[i, h] != null && bacs[i, h].gameObject != null)
//							Destroy (bacs[i, h].gameObject);
			
			transform.rotation = Quaternion.identity;
			bacs = newBacs;
			for (int row = 0; row < bacs.GetLength (0); row++)
				for (int col = 0; col < bacs.GetLength (1); col++)
					if (bacs [row, col]) {
						Debug.Log ("Placing bacteria at " + bacs [row, col].row + bacs [row, col].col);
						// Place the bacteria prefab gameObject in the desired relative space
						GameObject thisBac = bacs [row, col].gameObject;
						thisBac.transform.SetParent (transform);
						thisBac.transform.localPosition = new Vector3 (col - 2, 0, -row + 2);
					}
			ShapeBacteria ();
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

		private void FeedBacteria(int row, int col, float food)
		{
			// Get the bacteria at the coordinates
			Bacteria bac = bacs[row, col];

			// Feed it the food
			Health bacHealth = bac.GetComponent<Health>();
			if (bacHealth && bacHealth.current < bacHealth.max) {
				bacHealth.Heal (food);
				food -= bacHealth.max - bacHealth.current;
			}

			// If there's still more food, spread it according to the Bacteria resourceSpread
			// We only want to propagate food away from here

			// If there's a non null item in a row that is the same and resourceSpread col away from col
			// return true;

			// If there's a non null item in a col that is the same and resourceSpread row away from row
			// return true;

		}

		private void ShapeBacteria ()
		{
			// Iterate the bacteria, and shape them appropriately
			foreach (Bacteria bac in bacs) {
				if (bac != null && bac.type != bacteriaTypes.None) {
					bac.HideBodies ();
					Debug.Log ("All bacs " + bac.Name + bac.row + bac.col);
					// Scan, rotating from left, top, right, bottom, for other biofilm members
					bool isLeft = bac.col >= 1 ? bacs [bac.row, bac.col - 1] != null && bacs [bac.row, bac.col - 1].type != bacteriaTypes.None : false;
					bool isRight = bac.col < bacs.GetLength (1) - 1 ? bacs [bac.row, bac.col + 1] != null && bacs [bac.row, bac.col + 1].type != bacteriaTypes.None : false;
					bool isTop = bac.row > 0 ? bacs [bac.row - 1, bac.col] != null  && bacs [bac.row - 1, bac.col].type != bacteriaTypes.None : false;
					bool isBottom = bacs.GetLength (0) > bac.row + 1 ? bacs [bac.row + 1, bac.col] != null && bacs [bac.row + 1, bac.col].type != bacteriaTypes.None : false;

					// activate one of the body types based on what's around
					Debug.Log ("activate based on isLeft: " + isLeft + " isRight:" + isRight + " isTop:" + isTop + " isBottom:" + isBottom);
					// Center
					if (isLeft && isRight && isTop && isBottom)
						bac.bodyCenter.SetActive (true);

				// Edges
				else if (isLeft && isRight && isTop)
						bac.bodyEdgeDown.SetActive (true);
					else if (isLeft && isRight && isBottom)
						bac.bodyEdgeUp.SetActive (true);
					else if (isRight && isTop && isBottom)
						bac.bodyEdgeLeft.SetActive (true);
					else if (isLeft && isTop && isBottom)
						bac.bodyEdgeRight.SetActive (true);

				// Corners
				else if (isLeft && isTop)
						bac.bodyCornerRight.SetActive (true);
					else if (isLeft && isBottom)
						bac.bodyCornerUp.SetActive (true);
					else if (isRight && isTop)
						bac.bodyCornerDown.SetActive (true);
					else if (isRight && isBottom)
						bac.bodyCornerLeft.SetActive (true);

				// Tubes
				else if (isLeft && isRight)
						bac.bodyTubeHor.SetActive (true);
					else if (isTop && isBottom)
						bac.bodyTubeVert.SetActive (true);

				// Ends
				else if (isLeft)
						bac.bodyEndRight.SetActive (true);
					else if (isTop)
						bac.bodyEndDown.SetActive (true);
					else if (isRight)
						bac.bodyEndLeft.SetActive (true);
					else if (isBottom)
						bac.bodyEndUp.SetActive (true);

				}

			}
		}
	}
}

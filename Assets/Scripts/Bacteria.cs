using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace biomaria {
	// Class to track each bacteria
	public class Bacteria : MonoBehaviour {

		// The type of bacteria, from the enum of them
		public bacteriaTypes type;

		// The name of the bacteria
		public string Name;

		// Science name
		public string LatinName;

		// The description
		public string Description;

		// Fun Facts about the bacteria
		public string[] funFacts;

		// the image of the bacteria in the editor
		public Sprite image;

		// The price of this in quorum points
		public int quorumCost = 1;

		// Whether the surfaces of this bacteria can eat and the ratio they add to health
		public float eatRatio = 0;

		// The distance that the bacteria shares consumed resources
		public int resourceSpread = 1;

		// Conversion of resource to quorum ratio
		public float quorumRatio = 0;

		// Attack Value
		public float attack = 0;

		// Armor level (subtracted from other attacks which do health damage)
		public float armor = 0;
	
		// The maneuverability bonus per surface exposed
		public float maneuverability = 0;

		// The acceleration bonus per surface exposed
		public float accelBonus = 0;

		// The bodies which could represent this bacteria, pending biofilm position
		public GameObject bodyEdgeLeft;
		public GameObject bodyEdgeUp;
		public GameObject bodyEdgeRight;
		public GameObject bodyEdgeDown;

		public GameObject bodyCornerLeft;
		public GameObject bodyCornerUp;
		public GameObject bodyCornerRight;
		public GameObject bodyCornerDown;

		public GameObject bodyTubeHor;
		public GameObject bodyTubeVert;

		public GameObject bodyEndLeft;
		public GameObject bodyEndUp;
		public GameObject bodyEndRight;
		public GameObject bodyEndDown;

		public GameObject bodyCenter;
	
		// the location of the bacteria in its biofilm
		public int row = 0;
		public int col = 0;

		private Health health;
		private Quorum quorum;
		private QuorumController qc;

		public void Start ()
		{
			health = GetComponent<Health> ();	
			quorum = GetComponent<Quorum> ();
			qc = FindObjectOfType<QuorumController> ();
			HideBodies ();
		}

		public void Feed(float foodAmount)
		{
			if (type == bacteriaTypes.None)
				return;

			Debug.Log ("Feed: " + foodAmount);
			if (eatRatio > 0 && health != null && health.current < health.max) {
				health.Heal (foodAmount * eatRatio);
				Debug.Log ("healed");
				return;
			} else if (eatRatio > 0 && health != null && health.current >= health.max && resourceSpread >= 1) {
				Debug.Log ("SCORE" + (int) (foodAmount * eatRatio));
				qc.AddPoints ((int) (foodAmount * eatRatio));
				// If there's a non null item in a row that is the same and resourceSpread col away from col
				// return true;

				// If there's a non null item in a col that is the same and resourceSpread row away from row
				// return true;
					

			}
		}
		public void HideBodies()
		{
			if (type == bacteriaTypes.None)
				return;
			// exit if the first body part isn't there
			if (bodyEdgeLeft == null)
				return;
			bodyEdgeLeft.SetActive(false);
			bodyEdgeUp.SetActive(false);
			bodyEdgeRight.SetActive(false);
			bodyEdgeDown.SetActive(false);

			bodyCornerLeft.SetActive(false);
			bodyCornerUp.SetActive(false);
			bodyCornerRight.SetActive(false);
			bodyCornerDown.SetActive(false);

			bodyTubeHor.SetActive(false);
			bodyTubeVert.SetActive(false);

			bodyEndLeft.SetActive(false);
			bodyEndUp.SetActive(false);
			bodyEndRight.SetActive(false);
			bodyEndDown.SetActive(false);

			bodyCenter.SetActive(false);
		}
	}
}
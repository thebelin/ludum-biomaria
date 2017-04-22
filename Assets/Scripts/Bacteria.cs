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

		// the image of the bacteria in the editor
		public Sprite image;

		// The price of this in quorum points
		public int quorumCost = 1;

		// the location of the bacteria in its biofilm
		public int row = 0;
		public int col = 0;
	}
}
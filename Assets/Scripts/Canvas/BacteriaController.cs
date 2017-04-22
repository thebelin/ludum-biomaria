using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace biomaria {
	public class BacteriaController : MonoBehaviour {
		// The row and column in the editor for this bacteria item
		public int row = 0;
		public int col = 0;

		// The bacteria types this item could represent
		public GameObject[] bacTypes;

		// The current type of the bacteria (indexed by bacTypes)
		public int currentType = 0;

		// The current bacteria object for this item
		public Bacteria currentBacteria;

		// The GUI objects which show the bacteria
		public Text nameText;
		public Text costText;
		public Image bacteriaImage;

		private QuorumController qc;

		public void Start ()
		{
			// get the quorum controller
			qc = FindObjectOfType<QuorumController>();

			// Load the type
			GameObject gm = bacTypes [currentType];
			if (gm)
				currentBacteria = gm.GetComponent<Bacteria> ();

			// add the row/col to the bacteria data
			currentBacteria.row = row;
			currentBacteria.col = col;

			// Set the values in the display
			SetValues();

			// Add the bacteria to the quorum
			qc.AddBacteria(currentBacteria);
		}

		// Click handler
		public void ClickHandler()
		{
			// toggle the type of cell in the block through the bacTypes
			currentType++;
			if (bacTypes.Length <= currentType)
				currentType = 0;

			// Load the type
			GameObject gm = bacTypes [currentType];
			if (gm) {
				currentBacteria = gm.GetComponent<Bacteria> ();
				// add the row/col to the bacteria data
				currentBacteria.row = row;
				currentBacteria.col = col;

				// Add the bacteria to the quorum
				qc.AddBacteria(currentBacteria);
			}

			// Set the values in the display
			SetValues();
		}

		// {Basic, Attack, Stomach, Wall, Propeller, Dart};
		public int BacteriaPrice(int type)
		{
			switch (type)
			{
			// none
			case 0:
				return 0;
			// attack
			case 1:
				return 2;
			// Stomach
			case 2:
				return 3;
			// Wall
			case 3:
				return 2;
			case 4:
			// Propeller
				return 2;
			case 5:
			// Dart
				return 4;
			default:
				return 1;
			}
		}

		private void SetValues()
		{
			if (nameText)
				nameText.text = currentBacteria.Name;

			if (costText)
				costText.text = currentBacteria.quorumCost.ToString();

			if (bacteriaImage)
				bacteriaImage.sprite = currentBacteria.image;
		}

	}
}
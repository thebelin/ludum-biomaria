using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace biomaria {
	public class QuorumController : MonoBehaviour {
		// The canvasGroup which manages display of the editor
		public CanvasGroup QuorumCanvas;

		// The textbox which shows the points
		public Text pointsText;

		// A button object for saving
		public Button saveButton;

		// The bacteria which are part of this quorum
		public Bacteria[,] bacs;

		// The total Quorum points the user has
		public int points;

		// The total Quorum points spent on current bacterium
		public int spentPoints;

		// A reference to the player Quorum object
		private Quorum playerQuorum;

		void Start()
		{
			ComputeQuorumValue ();
			UpdateCanvas ();
			bacs = new Bacteria[5, 5];
			Time.timeScale = 0;
			GameObject player = GameObject.FindWithTag ("Player");
			playerQuorum = player.GetComponent<Quorum> ();
		}

		// Add bacteria to quorum
		public void AddBacteria(Bacteria bacteria)
		{
			if (bacs == null || bacteria == null)
				return;

			bacs[bacteria.row, bacteria.col] = bacteria;

			// Debug.Log ("Add bacteria at position" + bacteria.row + bacteria.col);

			ComputeQuorumValue ();
			UpdateCanvas ();
		}

		void UpdateCanvas()
		{
			if (pointsText)
				pointsText.text = spentPoints + "/" + points;

			if (saveButton)
				saveButton.interactable = spentPoints <= points && spentPoints > 0;
		}

		void ComputeQuorumValue()
		{
			spentPoints = 0;
			if (bacs == null)
				return;
			
			foreach (Bacteria bac in bacs)
				if (bac != null)
					spentPoints += bac.quorumCost;
		}

		public void Colonize()
		{
			// Send the player quorum controller the bacteria array

			// hide the editor
			if (QuorumCanvas) {
				Debug.Log ("hide Quorum Canvas");
				QuorumCanvas.interactable = false;
				QuorumCanvas.alpha = 0;
				QuorumCanvas.blocksRaycasts = false;
			}
			Time.timeScale = 1;
		}

	}
}
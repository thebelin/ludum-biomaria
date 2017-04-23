using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace biomaria {
	public class QuorumController : MonoBehaviour {
		// The canvasGroup which manages display of the editor
		public CanvasGroup QuorumCanvas;

		// The canvasgroup for the end screen
		public CanvasGroup WinScreen;

		public AudioClip MenuLoop;
		public AudioClip LevelLoop;
		public AudioClip WinLoop;

		public Text RunTimePoints;

		public CanvasGroup RuntimeCanvas;
		// The textbox which shows the points
		public Text pointsText;

		// The textbox for notifications
		public Text notificationText;

		// A button object for saving
		public Button saveButton;

		// The bacteria which are part of this quorum
		public Bacteria[,] bacs;

		// The total Quorum points the user has
		public int points;

		// The total Quorum points spent on current bacterium
		public int spentPoints;

		// How many points to get before pausing and allowing customization
		public int pointLevels = 10;

		// How many points before you win
		public int winPoints = 60;

		// The audio source
		public AudioSource loopSource;

		// A reference to the player Quorum object
		private Quorum playerQuorum;

		// The last point level the pause happened
		private int lastPausePoints = 0;

		void Start()
		{
			ComputeQuorumValue ();
			UpdateCanvas ();
			bacs = new Bacteria[5, 5];
			Time.timeScale = 0;
			GameObject player = GameObject.FindWithTag ("Player");
			playerQuorum = player.GetComponent<Quorum> ();
			lastPausePoints = points;
			if (loopSource && MenuLoop) {
				loopSource.clip = MenuLoop;
				loopSource.Play ();
			}
				
		}
		public void More ()
		{
			SceneManager.LoadScene ("Main");
		}
		public void Exit ()
		{
			Application.Quit ();
		}
		public void AddPoints (int add)
		{
			points += add;
			playerQuorum.maxQuorum = points;

			if (points >= winPoints) {
				Win ();
				return;
			}
			UpdateCanvas ();
			if (lastPausePoints + pointLevels < points) {
				// Pause
				Time.timeScale = 0;

				// Show the editor
				if (QuorumCanvas) {
					QuorumCanvas.interactable = true;
					QuorumCanvas.alpha = 1;
					QuorumCanvas.blocksRaycasts = true;
				}
				// Hide the points
				if (RuntimeCanvas) {
					RuntimeCanvas.interactable = false;
					RuntimeCanvas.alpha = 0;
					RuntimeCanvas.blocksRaycasts = false;
				}
				lastPausePoints = points;
				if (loopSource && MenuLoop) {
					loopSource.clip = MenuLoop;
					loopSource.Play ();
				}
			}
		}
		void Win()
		{
			Time.timeScale = 0;
			if (WinScreen) {
				WinScreen.alpha = 1;
				WinScreen.interactable = true;
				WinScreen.blocksRaycasts = true;
			}
			if (loopSource && WinLoop) {
				loopSource.clip = WinLoop;
				loopSource.loop = false;
				loopSource.Play ();
			}
		}
		// Add bacteria to quorum
		public void AddBacteria(Bacteria bacteria)
		{
			if (bacs == null || bacteria == null)
				return;

			bacs[bacteria.row, bacteria.col] = bacteria;

			Debug.Log ("Add bacteria at position" + bacteria.row + bacteria.col);

			if (notificationText != null) {
				
				if (bacteria.funFacts != null && bacteria.funFacts.Length != 0) 
					notificationText.text = bacteria.Name + ":   " + bacteria.funFacts [Random.Range (0, bacteria.funFacts.Length - 1)];
				else
					notificationText.text = bacteria.Name + ":   " + bacteria.Description;
			}
			ComputeQuorumValue ();
			UpdateCanvas ();
		}

		void UpdateCanvas()
		{
			if (pointsText)
				pointsText.text = spentPoints + "/" + points;

			if (saveButton)
				saveButton.interactable = spentPoints <= points && spentPoints > 0;

			if (RunTimePoints)
				RunTimePoints.text = points.ToString ();
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
			playerQuorum.LoadBacteria(bacs);

			if (loopSource && LevelLoop) {
				loopSource.clip = LevelLoop;
				loopSource.Play ();
			}

			if (RuntimeCanvas) {
				RuntimeCanvas.interactable = true;
				RuntimeCanvas.alpha = 1;
				RuntimeCanvas.blocksRaycasts = true;
			}
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
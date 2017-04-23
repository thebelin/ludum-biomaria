using UnityEngine;

namespace biomaria {
	public class CameraController : MonoBehaviour {

		// the speed to follow the player
		public float followSpeed = 1;

		// The camera's default distance to maintain
		public float minDistance = 10f;

		// The maximum difference the velocity should make
		public float velocityMod = 10f;

		// The difference to apply to the z axis 
		public float zModifier = -1f;

		//The MainCamera
		private GameObject playerCam;
		private Rigidbody playerBody;

		// Tracks the movement velocity of the camera
		private Vector3 currentVelocity = Vector3.one;
		private float heightVelocity = 1;

		// Use this for initialization
		void Start () {
			playerCam = GameObject.FindGameObjectWithTag("MainCamera");
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			if (player)
				playerBody = player.GetComponent<Rigidbody> ();
		}

		// Update is called once per frame
		void Update () {

			//Camera Controllers
			//If the ball is moving slow, zoom in
			//Follow the ball
			float magnitude = (float) playerBody.velocity.magnitude;

			// Translate the position of the camera towards the desired location
			playerCam.transform.position = Vector3.SmoothDamp(
				playerCam.transform.position,
				new Vector3(
					playerBody.transform.position.x,
					playerBody.transform.position.y + minDistance + Mathf.Max( (float) (playerBody.velocity.magnitude*1.50f), velocityMod),
					playerBody.transform.position.z - zModifier),
				ref currentVelocity,
				1,
				followSpeed,
				Time.deltaTime);
		}
	}
}
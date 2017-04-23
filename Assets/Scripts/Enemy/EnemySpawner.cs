using UnityEngine;
namespace biomaria {
	public class EnemySpawner : MonoBehaviour {

		public GameObject[] enemyPrefabs;
		public int numberOfEnemies = 1;
	    public float minSpawnRate = 0;
	    public float maxSpawnRate = 0;
	    public float rangeLow = -8.0f;
	    public float rangeHigh= 8.0f;

	    public bool lockPositionX;
	    public bool lockPositionY;
	    public bool lockPositionZ;

	    public bool lockRotationX;
	    public bool lockRotationY;
	    public bool lockRotationZ;

	    private float lastSpawnTime;
	    private float thisSpawnRate;

	    private void Start()
	    {
	        RandomizeSpawnRate();
	        if (enemyPrefabs != null)
	            SpawnEm();
	    }

	    private void RandomizeSpawnRate()
	    {
	        thisSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
	    }

	    private void Update()
	    {
	        if (minSpawnRate != 0 && maxSpawnRate != 0 && enemyPrefabs != null && lastSpawnTime + thisSpawnRate < Time.time)
	            SpawnEm();
	    }

	    private void SpawnEm()
	    {
	        // Reset the randomizer
	        RandomizeSpawnRate();

	        // track the spawning time
	        lastSpawnTime = Time.time;

			for (int i=0; i < numberOfEnemies; i++)
			{
				var spawnPosition = new Vector3(
	                lockPositionX ? transform.position.x : transform.position.x + Random.Range(rangeLow, rangeHigh),
	                lockPositionY ? transform.position.y : transform.position.y + Random.Range(rangeLow, rangeHigh),
	                lockPositionZ ? transform.position.z : transform.position.z + Random.Range(rangeLow, rangeHigh));

	            var spawnRotation = Quaternion.Euler(
	                lockRotationX ? transform.rotation.x : Random.Range(0, 180),
	                lockRotationY ? transform.rotation.y : Random.Range(0, 180),
	                lockRotationZ ? transform.rotation.z : Random.Range(0, 180));

				Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPosition, spawnRotation);
			}
		}
	}
}
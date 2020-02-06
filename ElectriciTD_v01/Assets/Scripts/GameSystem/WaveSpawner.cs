using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	#region Variables
	public Transform enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	public float timeBetweenEnemies = .4f;
	private float countdown = 2f;

	private int waveIndex = 0;
    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
		}

		countdown -= Time.deltaTime;
    }
	#endregion

	#region User Methods
	IEnumerator SpawnWave()
	{
		waveIndex++;
		GameManager.instance.Rounds++;

		for (int i = 0; i < waveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(timeBetweenEnemies);
		}

	}

	void SpawnEnemy()
	{
		Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
		
	}
	#endregion
}

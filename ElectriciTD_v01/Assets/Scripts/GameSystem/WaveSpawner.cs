using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
	#region Variables

	public static int EnemiesAlive = 0;

	public Transform spawnPoint;
	public Wave[] waves;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;

	private int waveIndex = 0;
    #endregion

    #region Unity Methods
    void Start()
    {
        
    }

    void Update()
    {
		if (EnemiesAlive > 0)
		{
			return;
		}

		if (countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			countdown = timeBetweenWaves;
			return;
		}

		countdown -= Time.deltaTime;
    }
	#endregion

	#region User Methods
	IEnumerator SpawnWave()
	{
		GameManager.Rounds++;
		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}

		waveIndex++;

		if (waveIndex == waves.Length)
		{
			Debug.Log("You Win Bitch!");
			this.enabled = false;
		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		EnemiesAlive++;
	}
	#endregion
}

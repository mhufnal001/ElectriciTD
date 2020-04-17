using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	#region Variables

	public static int EnemiesAlive = 0;
	public int numberOfEnemies;
	public Transform spawnPoint;
	public GameObject waveOverUI;
	public TextMeshProUGUI spawnWaveText, waveTitleText;
	private string currText;
	public Wave[] waves;

	private int waveIndex = 0;
	public int waveCount;
	public bool roundStart;
	public bool maxEnemies;
    #endregion

    #region Unity Methods
    void Start()
    {
		roundStart = false;
		maxEnemies = false;
		currText = spawnWaveText.text;
    }

    void Update()
    {
		if (roundStart == false)
		{
			if (GameManager.Rounds <= 0)
			{
				waveTitleText.text = "New Game";
				spawnWaveText.text = "Start Wave?";
			}
		}

		numberOfEnemies = EnemiesAlive;
		if (roundStart)
		{
			waveCount = waves[waveIndex - 1].count;
		}
		else
		{
			return;
		}

		if (EnemiesAlive >= waveCount)
		{
			maxEnemies = true;
		}

		if (maxEnemies && EnemiesAlive <= 0)
		{
			roundStart = false;
		}

		if (GameManager.Rounds > 0 && roundStart == false)
		{
			waveOverUI.SetActive(true);
			waveTitleText.text = "Wave: " + waveIndex.ToString();
			spawnWaveText.text = currText;
		}

		if (waveIndex == waves.Length && EnemiesAlive <= 0)
		{
			GameManager.GameWon = true;
		}
		else
		{
			return;
		}
	}
	#endregion

	#region User Methods
	IEnumerator SpawnWave()
	{
		GameManager.Rounds++;
		maxEnemies = false;
		Wave wave = waves[waveIndex];

		for (int i = 0; i < wave.count; i++)
		{
			SpawnEnemy(wave.enemy);
			yield return new WaitForSeconds(1f / wave.rate);
		}
	}

	void SpawnEnemy(GameObject enemy)
	{
		Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
		EnemiesAlive++;
	}

	public void StartNextWave()
	{
		roundStart = true;
		StartCoroutine(SpawnWave());
		waveIndex++;
	}
	#endregion
}

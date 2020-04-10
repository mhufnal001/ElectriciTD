using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
	#region Variables

	public static int EnemiesAlive = 0;

	public Transform spawnPoint;
	public GameObject waveOverUI;
	public TextMeshProUGUI spawnWaveText, waveTitleText;
	private string currText;
	public Wave[] waves;

	private int waveIndex = 0;
	private bool roundStart;
	private bool maxEnemies;
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
		if (roundStart)
		{
			waveOverUI.SetActive(false);
			return;
		}
		else if (roundStart == false)
		{
			if (GameManager.Rounds == 0)
			{
				waveTitleText.text = "New Game";
				spawnWaveText.text = "Start Wave?";
			}
			else
			{
				waveOverUI.SetActive(true);
				waveTitleText.text = "Wave: " + waveIndex.ToString();
				spawnWaveText.text = currText;
			}
		}

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


		if (waveIndex == waves.Length && EnemiesAlive <= 0)
		{
			GameManager.GameWon = true;
		}

		if (EnemiesAlive >= waves[waveIndex].count)
		{
			maxEnemies = true;

			if (maxEnemies && EnemiesAlive <= 0)
			{
				roundStart = false;
			}
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

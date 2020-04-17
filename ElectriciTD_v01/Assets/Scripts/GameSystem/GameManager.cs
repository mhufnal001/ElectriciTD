using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class GameManager : MonoBehaviour
{
	#region Singleton

	public static GameManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one GameManager in scene!");
			return;
		}

		instance = this;
	}

	#endregion


	#region Variables

	public static int Energy;
	public static int Power;
	public static int Rounds;
	[HideInInspector]
	public int maxEnergy = 9999;
	[Header("Health and Currency")]
	public int startEnergy = 450;
	public int maxPower = 100;
	public int startPower = 0;
	public CollectionsUI collections;

	private Wave wave;
	public static bool GameOver;
	public static bool GameWon;
	[Space]
	[Header("Assigned Variables")]
	public GameObject gameOverUI;
	public GameObject gameWonUI;
    #endregion

    #region Unity Methods
    void Start()
    {
		Energy = startEnergy;
		Power = startPower;
		

		Rounds = 0;
		GameOver = false;
		GameWon = false;
    }

    void Update()
    {
		if (Power >= maxPower)
		{
			GameOver = true;
			EndGame(true);
		}
		if (GameWon)
		{
			EndGame(true);
		}
    }

    #endregion

    #region User Methods

	void EndGame(bool gameOver)
	{
		if (GameOver == true && GameWon == false)
		{
			gameOverUI.SetActive(true);
		}
		else if (GameWon == true && GameOver == false)
		{
			gameWonUI.SetActive(true);
		}
	}
	#endregion
}

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

	public static bool GameOver;
	[Space]
	[Header("Assigned Variables")]
	public GameObject gameOverUI;
	public Text gainedEnergyText, energyText;
	public Animator energyAnim;
    #endregion

    #region Unity Methods
    void Start()
    {
		Energy = startEnergy;
		Power = startPower;
		gainedEnergyText.enabled = false;
		energyAnim = energyText.GetComponent<Animator>();

		Rounds = 0;
		GameOver = false;
    }

    void Update()
    {
		if (GameOver)
		{
			return;
		}

		if (Power >= maxPower)
		{
			EndGame();
		}
    }

    #endregion

    #region User Methods

	void EndGame()
	{
		GameOver = true;

		gameOverUI.SetActive(true);
	}

	public void GainedEnergy(int energyGained)
	{
		energyAnim.SetTrigger("GainedEnergy");

		Energy += energyGained;
		gainedEnergyText.text = "+" + energyGained;
		gainedEnergyText.enabled = true;
	}

	#endregion
}

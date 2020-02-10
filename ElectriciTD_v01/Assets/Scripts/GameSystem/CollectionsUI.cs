using UnityEngine;
using UnityEngine.UI;

public class CollectionsUI : MonoBehaviour
{
	#region Variables

	public Text powerText, gainedEnergyText, gainedPowerText, energyText, spentEnergyText;

	public Animator energyAnim, powerAnim;

	GameManager gm; 

    #endregion

    #region Unity Methods
    void Start()
    {
		gm = GameManager.instance;

		powerAnim = powerText.GetComponent<Animator>();
		energyAnim = energyText.GetComponent<Animator>();

		gainedPowerText.enabled = false;
		spentEnergyText.enabled = false;
		gainedEnergyText.enabled = false;
	}

    void Update()
    {
		powerText.text = GameManager.Power.ToString();
    }
	#endregion

	#region User Methods

	public void GainedPower(int powerGained)
	{
		gainedPowerText.text = "+" + powerGained;
		gainedPowerText.enabled = true;

	}

	public void SpentEnergy(int eSpent)
	{
		spentEnergyText.text = "-" + eSpent;
		spentEnergyText.enabled = true;
	}

	public void GainedEnergy(int energyGained)
	{
		energyAnim.SetTrigger("GainedEnergy");
		gainedEnergyText.enabled = true;

		GameManager.Energy += energyGained;

		energyText.text = "+" + energyGained;
	}

	#endregion
}

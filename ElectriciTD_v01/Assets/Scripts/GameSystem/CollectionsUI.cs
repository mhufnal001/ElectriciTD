using UnityEngine;
using UnityEngine.UI;

public class CollectionsUI : MonoBehaviour
{
	#region Variables

	public Text powerText;
	public Text gainedPowerText;
	public Text energyText;
	public Text spentEnergyText;

	public Animator energyAnim;
	public Animator gainedPowerAnim;

	BuildManager bm;

    #endregion

    #region Unity Methods
    void Start()
    {
		bm = BuildManager.instance;

		gainedPowerAnim = powerText.GetComponent<Animator>();
		gainedPowerText.enabled = false;

		energyAnim = energyText.GetComponent<Animator>();
		spentEnergyText.enabled = false;
	}

    void Update()
    {
		powerText.text = GameManager.Power.ToString();
    }
	#endregion

	#region User Methods

	public void GainedPowerAnimation(int powerGained)
	{
		gainedPowerText.text = "+" + powerGained;
		gainedPowerText.enabled = true;

	}

	public void SpentEnergyAnimation(int eSpent)
	{
		spentEnergyText.text = "-" + eSpent;
		spentEnergyText.enabled = true;
	}

	#endregion
}

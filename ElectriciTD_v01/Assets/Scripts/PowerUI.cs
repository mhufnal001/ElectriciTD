using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
	#region Variables

	public Text powerText;
	public Text gainedPowerText;

	public Animator gainedPowerAnim;

    #endregion

    #region Unity Methods
    void Start()
    {
		gainedPowerAnim = powerText.GetComponent<Animator>();
		gainedPowerText.enabled = false;
    }

    void Update()
    {
		powerText.text = GameManager.Power.ToString();
    }
	#endregion

	#region User Methods

	public void GainedPowerAnimation(int powerGained)
	{
		gainedPowerText.enabled = true;
		gainedPowerText.text = "+" + powerGained;

	}

	#endregion
}

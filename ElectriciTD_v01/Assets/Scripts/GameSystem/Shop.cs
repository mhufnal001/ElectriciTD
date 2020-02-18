using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
	#region Variables

	BuildManager bm;
	public TurretBlueprints[] turretTypes;
	private Color noEnergyColor, hasEnergyColor;

	public Text energyText;
	public Button sTurButton, hTurButton;
	public TextMeshProUGUI sTurCostTxt, hTurCostTxt;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;
		noEnergyColor = Color.red;
		hasEnergyColor = sTurCostTxt.faceColor;
    }

    void Update()
    {
		energyText.text = GameManager.Energy.ToString();
		sTurCostTxt.text = turretTypes[0].energyCost.ToString();
		hTurCostTxt.text = turretTypes[1].energyCost.ToString();

		if (GameManager.Energy < turretTypes[0].energyCost)
		{
			sTurCostTxt.faceColor = noEnergyColor;
			sTurButton.interactable = false;
		}
		else
		{
			sTurCostTxt.faceColor = hasEnergyColor;
			sTurButton.interactable = true;
		}
		if (GameManager.Energy < turretTypes[1].energyCost)
		{
			hTurCostTxt.faceColor = noEnergyColor;
			hTurButton.interactable = false;
		}
		else
		{
			hTurCostTxt.faceColor = hasEnergyColor;
			hTurButton.interactable = true;
		}
	}

    #endregion

    #region User Methods

	public void SelectedTurret(int turretIndex)
	{
		GameObject turretSelected = turretTypes[turretIndex].turretPrefab;

		bm.SetTurretToBuild(turretSelected);
	}

    #endregion	

}

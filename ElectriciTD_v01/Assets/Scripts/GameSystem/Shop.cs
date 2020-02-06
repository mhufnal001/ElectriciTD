using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	#region Variables

	BuildManager bm;
	public TurretBlueprints[] turretTypes;

	public Text energyText;
	public Text standardTurret, heavyTurret;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;
    }

    void Update()
    {
		energyText.text = GameManager.Energy.ToString();
		standardTurret.text = turretTypes[0].energyCost.ToString();
		heavyTurret.text = turretTypes[1].energyCost.ToString();
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

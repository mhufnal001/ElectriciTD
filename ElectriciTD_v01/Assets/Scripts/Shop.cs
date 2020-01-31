using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	#region Variables

	BuildManager bm;
	public TurretBlueprints[] turretTypes;

	public Text energyText;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;
    }

    void Update()
    {

		energyText.text = "Energy: " + GameManager.Energy.ToString();
        
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

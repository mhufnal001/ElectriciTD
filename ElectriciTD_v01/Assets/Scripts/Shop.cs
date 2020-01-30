using UnityEngine;

public class Shop : MonoBehaviour
{
	#region Variables

	BuildManager buildManager;
	public TurretBlueprints[] turretTypes;

    #endregion

    #region Unity Methods

    void Start()
    {
		buildManager = BuildManager.instance;
    }

    void Update()
    {
        
    }

    #endregion

    #region User Methods

	public void SelectedTurret(int turretIndex)
	{
		GameObject turretSelected = turretTypes[turretIndex].turretPrefab;

		buildManager.SetTurretToBuild(turretSelected);
	}

    #endregion	

}

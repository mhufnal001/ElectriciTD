﻿using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
	#region Singleton

	public static BuildManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}

		instance = this;
	}

	#endregion


	#region Variables

	private GameObject turretToBuild;
	private TurretBlueprints selectedTurret;

	public GameObject buildEffect;
	public Text spentEnergyText;
	public Animation spentEnergyAnim;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return GameManager.Energy >= selectedTurret.energyCost; } }

    #endregion

    #region Unity Methods
    void Start()
    {
		selectedTurret = null;
		spentEnergyAnim = spentEnergyText.GetComponent<Animation>();

		spentEnergyText.enabled = false;
    }

    void Update()
    {
        
    }
	#endregion

	#region User Methods

	public void SetTurretToBuild(GameObject _turret)
	{
		turretToBuild = _turret;

		selectedTurret = turretToBuild.GetComponent<EnemyTargeting>().currentTurret;
	}

	public void BuildTurretOn(Node node)
	{

		if (GameManager.Energy < selectedTurret.energyCost)
		{
			Debug.Log("Not Enough Money!");
			return;
		}

		GameManager.Energy -= selectedTurret.energyCost;

		//Build Turret
		GameObject turret = Instantiate(turretToBuild, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		GameObject turretBE = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);

		Debug.Log("Turret Built! Energy Left:" + GameManager.Energy);

	}

	public void SpentEnergyAnimation()
	{
		spentEnergyText.enabled = true;

		spentEnergyText.text = "-" + selectedTurret.energyCost;

		spentEnergyAnim.Play();

	}

	#endregion
}

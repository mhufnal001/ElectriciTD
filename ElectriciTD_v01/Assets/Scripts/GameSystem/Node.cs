using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
	#region Variables

	public Color hasEnergyColor;
	public Color noEnergyColor;
	private Color startColor;
	public Vector3 turretPosOffset;

	public GameObject turret;
	public Turret turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	[Header("NonOptional")]
	public CollectionsUI collections;
	public UpgradeUI upgrades;

	private Renderer r;
	[HideInInspector]
	public int upgradeLevel;

	[Header("Upgrade Modifers")]
	public int upgradeAV = 2;
	public int upgradeRange = 2;
	public int upgradeSP = 2;
	public int upgradeHP = 100;
	public float upgradeFR = 1.05f;

	BuildManager bm;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;

		r = GetComponent<Renderer>();
		startColor = r.material.color;
		upgradeLevel = 1;
    }

    void Update()
    {
        
    }

	private void OnMouseEnter()
	{
		if (!bm.CanBuild)
		{
			return;
		}

		if (bm.HasMoney)
		{
			r.material.color = hasEnergyColor;
		}
		else if (!bm.HasMoney)
		{
			r.material.color = noEnergyColor;
		}

	}

	private void OnMouseExit()
	{
		r.material.color = startColor;
		return;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!bm.CanBuild)
		{
			return;
		}

		if (turret != null)
		{
			bm.SelectNode(this);
			bm.canUpgrade = true;
			return;
		}

		BuildTurret(bm.GetTurretToBuild());
	}

	#endregion

	#region User Methods

	public Vector3 GetBuildPosition()
	{
		return transform.position + turretPosOffset;
	}

	void BuildTurret (Turret blueprint)
	{
		turretBlueprint = blueprint;

		if (GameManager.Energy < blueprint.currentBlueprint.energyCost)
		{
			Debug.Log("Not Enough Money!");
			collections.energyAnim.ResetTrigger("SpentEnergy");

			return;
		}

		collections.SpentEnergy(bm.selectedTurret.currentBlueprint.energyCost);
		GameManager.Energy -= turretBlueprint.currentBlueprint.energyCost;

		//Build Turret
		GameObject _turret = Instantiate(blueprint.currentBlueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	public void SellTurret()
	{
		collections.GainedEnergy(turretBlueprint.currentBlueprint.sellPrice);

		Destroy(turret);
	}

	public void UpgradeTurret()
	{
		Turret currTurret = turret.GetComponent<Turret>();
		if (GameManager.Energy < currTurret.currentBlueprint.upgradeCost[upgradeLevel - 1])
		{
			upgrades.upgradeButton.image.color = noEnergyColor;
			collections.energyAnim.ResetTrigger("SpentEnergy");

			return;
		}
		else
		{
			upgrades.upgradeButton.image.color = hasEnergyColor;
		}
		if (upgradeLevel >= currTurret.currentBlueprint.upgradeCost.Length)
		{
			upgrades.upgradeButton.enabled = false;
			return;
		}

		GameManager.Energy -= currTurret.currentBlueprint.upgradeCost[upgradeLevel - 1];
		collections.energyAnim.SetTrigger("SpentEnergy");
		isUpgraded = true;

		turretBlueprint.ad *= upgradeAV;
		turretBlueprint.range += upgradeRange;
		turretBlueprint.sellPrice *= upgradeSP;
		turretBlueprint.hp += upgradeHP;
		turretBlueprint.fireRate *= upgradeFR;


		upgradeLevel++;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	#endregion
}

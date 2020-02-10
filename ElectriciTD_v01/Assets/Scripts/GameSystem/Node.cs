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

		if (bm.HasMoney && bm.canUpgrade == false)
		{
			r.material.color = hasEnergyColor;
		}
		else if (!bm.HasMoney || bm.canUpgrade == true)
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
		collections.SpentEnergy(bm.selectedTurret.energyCost);
	}

	#endregion

	#region User Methods

	public Vector3 GetBuildPosition()
	{
		return transform.position + turretPosOffset;
	}

	void BuildTurret (Turret blueprint)
	{
		if (GameManager.Energy < blueprint.energyCost)
		{
			Debug.Log("Not Enough Money!");
			collections.energyAnim.ResetTrigger("SpentEnergy");

			return;
		}

		GameManager.Energy -= blueprint.energyCost;
		collections.energyAnim.SetTrigger("SpentEnergy");

		turretBlueprint = blueprint;
		//Build Turret
		GameObject _turret = Instantiate(blueprint.currentBlueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	public void SellTurret()
	{
		collections.GainedEnergy(turret.GetComponent<Turret>().currentBlueprint.sellPrice);

		Destroy(turret);
	}

	public void UpgradeTurret()
	{
		if (GameManager.Energy < turretBlueprint.currentBlueprint.upgradeCost[upgradeLevel - 1])
		{
			Debug.Log("Not Enough Money!");
			collections.energyAnim.ResetTrigger("SpentEnergy");

			return;
		}
		if (upgradeLevel >= turretBlueprint.currentBlueprint.upgradeCost.Length)
		{
			upgrades.upgradeButton.enabled = false;
			return;
		}

		GameManager.Energy -= turretBlueprint.currentBlueprint.upgradeCost[upgradeLevel - 1];
		collections.energyAnim.SetTrigger("SpentEnergy");
		isUpgraded = true;

		turretBlueprint.ad *= upgradeAV;
		turretBlueprint.range += upgradeRange;
		turretBlueprint.sellPrice *= upgradeSP;
		turretBlueprint.hp += upgradeHP;
		turretBlueprint.fireRate *= upgradeFR;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	#endregion
}

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
		if (turret != null)
		{
			turret.GetComponent<Turret>().rangeIndicator.SetActive(false);
		}
		r.material.color = startColor;
		return;
	}

	private void OnMouseDown()
	{

		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}


		if (turret != null)
		{
			turret.GetComponent<Turret>().rangeIndicator.SetActive(!turret.GetComponent<Turret>().rangeIndicator.activeInHierarchy);
			bm.SelectNode(this);
			bm.canUpgrade = true;
			return;
		}

		if (!bm.CanBuild)
		{
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
		turretBlueprint.rangeIndicator.SetActive(false);

		//Build Turret
		GameObject _turret = Instantiate(blueprint.currentBlueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	public void SellTurret()
	{
		collections.GainedEnergy(turretBlueprint.sellPrice);

		Destroy(turret);
	}

	public void UpgradeTurret()
	{
		Turret currTurret = turret.GetComponent<Turret>();

		if (upgradeLevel > currTurret.currentBlueprint.upgradeCost.Length && GameManager.Energy < currTurret.currentBlueprint.upgradeCost[upgradeLevel])
		{
			upgrades.upgradeButton.interactable = false;
			return;
		}
		else
		{
			upgrades.upgradeButton.interactable = true;
		}

		GameManager.Energy -= currTurret.currentBlueprint.upgradeCost[upgradeLevel - 1];
		collections.energyAnim.SetTrigger("SpentEnergy");
		isUpgraded = true;

		currTurret.ad *= upgradeAV;
		currTurret.range += upgradeRange;
		currTurret.sellPrice *= upgradeSP;
		currTurret.hp += upgradeHP;
		currTurret.fireRate *= upgradeFR;


		upgradeLevel++;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	#endregion
}

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
	public GameObject rangeInd;
	public Turret turretBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	[Header("NonOptional")]
	public CollectionsUI collections;
	public UpgradeUI upgrades;

	private Renderer r;

	[Header("Upgrade Modifers")]
	public int upgradeLevel;
	public float upgradeAD = 1.15f;
	public float upgradeRange = 1.25f;
	public int upgradeSP = 2;
	public int upgradeHP = 100;
	public float upgradeFR = 1.05f;
	public Vector3 turretRangeUpgrade;
	public bool maxLevel;

	BuildManager bm;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;
		maxLevel = false;

		r = GetComponent<Renderer>();
		startColor = r.material.color;

		upgradeLevel = 1;
    }

    void Update()
    {
		if (turret != null)
		{
			if (upgradeLevel >= turretBlueprint.currentBlueprint.upgradeCost.Length)
			{
				maxLevel = true;
				if (maxLevel)
				{
					upgrades.upgradeButton.image.color = Color.red;
					upgrades.upgradeButton.interactable = false;
					upgrades.upgradeText.text = "Max Level";

					
				}

			}
		}
		else if (turret == null)
		{
			return;
		}

		if (rangeInd != null)
		{
			upgrades.range.text = rangeInd.transform.localScale.x.ToString();
		}
		else
		{
			return;
		}

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
			turretBlueprint = turret.GetComponent<Turret>();
			rangeInd = turretBlueprint.rangeIndicator;
			rangeInd.SetActive(!rangeInd.activeInHierarchy);
							
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
		isUpgraded = true;

		currTurret.ad *= upgradeAD;
		currTurret.ad = Mathf.RoundToInt(currTurret.ad);
		currTurret.range += upgradeRange;
		currTurret.range = Mathf.RoundToInt(currTurret.range);
		currTurret.sellPrice *= upgradeSP;
		currTurret.hp += upgradeHP;
		currTurret.fireRate *= upgradeFR;
		currTurret.fireRate = Mathf.RoundToInt(currTurret.fireRate);

		upgradeLevel++;

		turretRangeUpgrade = new Vector3(currTurret.range, 0, currTurret.range);
		rangeInd.gameObject.transform.localScale += turretRangeUpgrade;
		
		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	#endregion
}

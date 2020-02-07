using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
	#region Variables

	private GameObject turretToUpgrade;
	public Animator upgradeUI;
	public Animator shopUI;
	public Button upgIcButton;
	public Button upgradeButton;
	public Button shopIcButton;
	public CollectionsUI collections;

	public int upgradeLevel;

	[Space]
	[Header("UpgradeUI")]
	public Image turretIcon;
	public Text turretName, upgradeCost, sellValue;


	 bool isUpgradeActive, isShopActive;

    #endregion

    #region Unity Methods
    void Start()
    {
		isUpgradeActive = false;
		isShopActive = true;
    }

    void Update()
    {
		if (isShopActive == true)
		{
			shopIcButton.enabled = false;
			shopIcButton.animator.SetTrigger("Disabled");

			upgIcButton.enabled = true;
		}
		else if (isUpgradeActive == true || turretToUpgrade == null)
		{
			upgIcButton.enabled = false;
			upgIcButton.animator.SetTrigger("Disabled");
			shopIcButton.enabled = true;
		}
		if (turretToUpgrade != null)
		{
			SetTurretToUpgrade();
			turretIcon.sprite = turretToUpgrade.GetComponent<Turret>().currentBlueprint.turretIcon;
			turretName.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.turretName;
			upgradeCost.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.upgradeCost[upgradeLevel - 1].ToString();
			sellValue.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.sellPrice.ToString();
		}

	}
	#endregion

	#region User Methods

	public void SetTurretToUpgrade()
	{
		Node node = BuildManager.instance.GetSelectedNode();
		turretToUpgrade = node.turret;
	}

	public void UpgradeActive()
	{
		SetTurretToUpgrade();
		upgradeUI.SetTrigger("Active");
		isUpgradeActive = true;

		if (isShopActive)
		{
			shopUI.SetTrigger("Inactive");

			isShopActive = false;
		}

	}
	public void ShopActive()
	{
		shopUI.SetTrigger("Active");
		isShopActive = true;

		if (isUpgradeActive)
		{
			upgradeUI.SetTrigger("Inactive");

			isUpgradeActive = false;
		}
	}
	public void UpgradeTurret()
	{
		int _upgrade = turretToUpgrade.GetComponent<Turret>().currentBlueprint.upgradeCost[upgradeLevel - 1];

		if (upgradeLevel >= turretToUpgrade.GetComponent<Turret>().currentBlueprint.upgradeCost.Length)
		{
			upgradeButton.enabled = false;
			return;
		}
		else
		{
			if (GameManager.Energy > _upgrade)
			{
				GameManager.Energy -= _upgrade;
				GameManager.instance.energyAnim.SetTrigger("SpentEnergy");
				collections.SpentEnergyAnimation(_upgrade);
				upgradeLevel++;
			}
			else
			{
				return;
			}
		}
	}

    #endregion	
}

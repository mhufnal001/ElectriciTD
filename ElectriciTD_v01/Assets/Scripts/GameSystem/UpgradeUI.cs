using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
	#region Variables

	private GameObject turretToUpgrade;
	private Turret turret;
	public Animator upgradeUI;
	public Animator shopUI;
	public Animator settingsUI;
	public Button upgradeButton;
	public Button upgIcButton;
	public Button shopIcButton;
	public Button settIcButton;
	public CollectionsUI collections;
	public Node target;

	[Space]
	[Header("UpgradeUI")]
	public Image turretIcon;
	public TextMeshProUGUI turretName;
	public TextMeshProUGUI damage, health, range, fireRate, upgradeCost, sellValue;


	bool isUpgradeActive, isShopActive, isSettingsActive;

    #endregion

    #region Unity Methods
    void Start()
    {
		isUpgradeActive = false;
		isShopActive = true;
		isSettingsActive = true;
		
    }

    void Update()
    {
		if (isShopActive == true)
		{
			shopIcButton.animator.SetTrigger("Disabled");
			upgIcButton.animator.SetTrigger("Normal");
			settIcButton.animator.SetTrigger("Normal");

			isSettingsActive = false;
			isUpgradeActive = false;

		}
		else if (isUpgradeActive == true || turretToUpgrade == null)
		{
			upgIcButton.animator.SetTrigger("Disabled");
			shopIcButton.animator.SetTrigger("Normal");
			isSettingsActive = false;
			isShopActive = false;
		}
		else if (isSettingsActive == true)
		{
			shopIcButton.animator.SetTrigger("Normal");
			settIcButton.animator.SetTrigger("Disabled");
			upgIcButton.animator.SetTrigger("Disabled");

			isShopActive = false;
			isUpgradeActive = false;
		}

		if (turretToUpgrade != null)
		{
			turret = turretToUpgrade.GetComponent<Turret>();

			turretIcon.sprite = turret.currentBlueprint.turretIcon;
			turretName.text = turret.currentBlueprint.turretName;
			upgradeCost.text = turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1].ToString();
			sellValue.text = turret.sellPrice.ToString();
		}
		else
		{
			return;
		}

		damage.text = turret.ad.ToString();
		health.text = turret.hp.ToString();
		range.text = turret.range.ToString();
		fireRate.text = turret.fireRate.ToString();

	}
	#endregion

	#region User Methods

	public void SetNode(Node _target)
	{
		target = _target;
	}

	public void SetTurretToUpgrade()
	{
		Node node = BuildManager.instance.GetSelectedNode();
		turretToUpgrade = node.turret;
	}

	public void UpgradeActive()
	{
		if (BuildManager.instance.GetSelectedNode() == null)
		{
			return;
		}
		
		SetTurretToUpgrade();
		upgradeUI.SetTrigger("Active");
		isUpgradeActive = true;

		if (isShopActive)
		{
			shopUI.SetTrigger("Inactive");

			isShopActive = false;
		}
		else if (isSettingsActive)
		{
			settingsUI.SetTrigger("Inactive");
			isSettingsActive = false;

		}

	}
	public void ShopActive()
	{
		shopUI.SetTrigger("Active");
		isShopActive = true;
		BuildManager.instance.DeselectNode();

		if (isUpgradeActive)
		{
			upgradeUI.SetTrigger("Inactive");

			isUpgradeActive = false;
		}
		else if (isSettingsActive)
		{
			settingsUI.SetTrigger("Inactive");
			isSettingsActive = false;

		}
	}

	public void OptionsActive()
	{
		settingsUI.SetTrigger("Active");
		isSettingsActive = true;
		BuildManager.instance.DeselectNode();

		if (isUpgradeActive)
		{
			upgradeUI.SetTrigger("Inactive");

			isUpgradeActive = false;
		}
		else if (isShopActive)
		{
			shopUI.SetTrigger("Inactive");
			isShopActive = false;

		}
	}
	public void UpgradeTurret()
	{
		target.UpgradeTurret();
	}

	public void Sell()
	{
		Node node = BuildManager.instance.GetSelectedNode();
		ShopActive();
		node.SellTurret();
	}

    #endregion	
}

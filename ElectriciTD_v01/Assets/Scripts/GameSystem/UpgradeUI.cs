using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeUI : MonoBehaviour
{
	#region Variables

	private GameObject turretToUpgrade;
	public Animator upgradeUI;
	public Animator shopUI;
	public Animator settingsUI;
	public Button upgIcButton;
	public Button upgradeButton;
	public Button shopIcButton;
	public Button settIcButton;
	public CollectionsUI collections;
	public Node target;

	[Space]
	[Header("UpgradeUI")]
	public Image turretIcon;
	public Text upgradeCost, sellValue;
	public TextMeshProUGUI turretName;


	bool isUpgradeActive, isShopActive, isSettingsActive;

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
			turretIcon.sprite = turretToUpgrade.GetComponent<Turret>().currentBlueprint.turretIcon;
			turretName.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.turretName;
			upgradeCost.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.upgradeCost[target.upgradeLevel - 1].ToString();
			sellValue.text = turretToUpgrade.GetComponent<Turret>().currentBlueprint.sellPrice.ToString();
		}
		else
		{
			return;
		}

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

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
	private string currText;

	[Space]
	[Header("UpgradeUI")]
	public Image turretIcon;
	public TextMeshProUGUI turretName, upgradeText;
	public TextMeshProUGUI damage, health, range, fireRate, upgradeCost, sellValue;


	 public bool isUpgradeActive, isShopActive, isSettingsActive;

    #endregion

    #region Unity Methods
    void Start()
    {
		isUpgradeActive = false;
		isShopActive = true;
		isSettingsActive = false;

		currText = upgradeText.text;
		
    }

    void Update()
    {
		if (isShopActive == true)
		{
			shopIcButton.animator.SetTrigger("Disabled");
			if (BuildManager.instance.GetTurretToBuild() != null)
			{
				upgIcButton.animator.SetTrigger("Normal");
			}
			else
			{
				upgIcButton.animator.SetTrigger("Disabled");

			}
			settIcButton.animator.SetTrigger("Normal");

		}
		else if (isUpgradeActive == true || turretToUpgrade == null)
		{
			upgIcButton.animator.SetTrigger("Disabled");
			shopIcButton.animator.SetTrigger("Normal");
			settIcButton.animator.SetTrigger("Normal");
		}
		else if (isSettingsActive == true)
		{
			shopIcButton.animator.SetTrigger("Normal");
			settIcButton.animator.SetTrigger("Disabled");
			upgIcButton.animator.SetTrigger("Disabled");
		}

		if (turretToUpgrade != null)
		{
			turret = turretToUpgrade.GetComponent<Turret>();

			turretIcon.sprite = turret.currentBlueprint.turretIcon;
			turretName.text = turret.currentBlueprint.turretName;
			if (target.maxLevel == false)
			{
				upgradeCost.text = turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1].ToString();
			}
			else
			{
				upgradeCost.text = "";
				return;
			}
			sellValue.text = turret.sellPrice.ToString();
		}
		else
		{
			return;
		}

		if (GameManager.Energy < turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1])
		{
			upgradeText.text = "Need " + (turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1] - GameManager.Energy).ToString() + " more Energy.";
			upgradeButton.image.color = Color.red;
			upgradeButton.interactable = false;
		}
		else
		{
			upgradeText.text = currText;
			upgradeButton.image.color = Color.green;
			upgradeButton.interactable = true;
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
		turretToUpgrade = target.turret;
	}

	public void UpgradeActive()
	{
		if (BuildManager.instance.GetSelectedNode() == null)
		{
			return;
		}
		
		isUpgradeActive = true;
		SetTurretToUpgrade();
		upgradeUI.SetTrigger("Active");

		if (isShopActive)
		{
			shopUI.SetTrigger("Inactive");

			isShopActive = false;
		}
		if (isSettingsActive)
		{
			settingsUI.SetTrigger("Inactive");
			isSettingsActive = false;

		}

	}
	public void ShopActive()
	{
		isShopActive = true;
		shopUI.SetTrigger("Active");
		BuildManager.instance.DeselectNode();

		if (isUpgradeActive)
		{
			upgradeUI.SetTrigger("Inactive");

			isUpgradeActive = false;
		}
		if (isSettingsActive == true)
		{
			settingsUI.SetTrigger("Inactive");
			isSettingsActive = false;

		}
	}

	public void OptionsActive()
	{
		isSettingsActive = true;
		settingsUI.SetTrigger("Active");
		BuildManager.instance.DeselectNode();

		if (isUpgradeActive)
		{
			upgradeUI.SetTrigger("Inactive");

			isUpgradeActive = false;

		}
		if (isShopActive)
		{
			shopUI.SetTrigger("Inactive");
			isShopActive = false;

		}
	}

	public void UpgradeTurret()
	{

		if (GameManager.Energy >= turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1])
		{
			GameManager.Energy -= turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1];
			collections.SpentEnergy(turret.currentBlueprint.upgradeCost[target.upgradeLevel - 1]);
			collections.energyAnim.SetTrigger("SpentEnergy");
			target.UpgradeTurret();
		}

		damage.text = turret.ad.ToString();
		health.text = turret.hp.ToString();
		fireRate.text = turret.fireRate.ToString();

	}

	public void Sell()
	{
		ShopActive();
		target.SellTurret();
	}

    #endregion	
}

using UnityEngine;
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
	[HideInInspector]
	private GameObject turretToBuild;
	[HideInInspector]
	public Turret selectedTurret;
	public Node selectedNode;

	public GameObject buildEffect;
	public GameObject upgradeUI;
	public GameObject shopUI;
	public UpgradeUI upgrade;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return GameManager.Energy >= selectedTurret.currentBlueprint.energyCost; } }
	public bool canUpgrade;

    #endregion

    #region Unity Methods

    void Start()
    {
		selectedTurret = null;
		canUpgrade = false;
	}

    void Update()
    {
        
    }
	#endregion

	#region User Methods

	public void SetTurretToBuild(GameObject _turret)
	{
		turretToBuild = _turret;
		selectedTurret = turretToBuild.GetComponent<Turret>();

		DeselectNode();
	}

	public void SelectNode(Node node)
	{
		selectedNode = node;
		turretToBuild = null;
		upgrade.SetNode(node);
;	}
	public void DeselectNode()
	{
		selectedNode = null;
		canUpgrade = false;
	}

	public Turret GetTurretToBuild()
	{
		return selectedTurret;
	}
	public Node GetSelectedNode()
	{
		return selectedNode;
	}
	#endregion
}

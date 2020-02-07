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
	public TurretBlueprints selectedTurret;
	private Node selectedNode;

	public GameObject buildEffect;
	public GameObject upgradeUI;
	public GameObject shopUI;
	public UpgradeUI upgrade;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return GameManager.Energy >= selectedTurret.energyCost; } }
	public bool cUpgrades;

    #endregion

    #region Unity Methods

    void Start()
    {
		selectedTurret = null;
		cUpgrades = false;
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

		selectedNode = null;
	}

	public void SelectNode(Node node)
	{
		selectedNode = node;
		turretToBuild = null;
;	}

	public TurretBlueprints GetTurretToBuild()
	{
		return selectedTurret;
	}
	public Node GetSelectedNode()
	{
		return selectedNode;
	}
	#endregion
}

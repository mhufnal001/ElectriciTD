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

	private GameObject turretToBuild;
	private TurretBlueprints selectedTurret;

	public GameObject buildEffect;
	public Text spentEnergyText;
	public Text energyText;
	public Animator spentEnergyAnim;

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return GameManager.Energy >= selectedTurret.energyCost; } }

    #endregion

    #region Unity Methods
    void Start()
    {
		selectedTurret = null;
		spentEnergyAnim = energyText.GetComponent<Animator>();
		spentEnergyText.enabled = false;
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
	}

	public void BuildTurretOn(Node node)
	{

		if (GameManager.Energy < selectedTurret.energyCost)
		{
			Debug.Log("Not Enough Money!");
			return;
		}

		GameManager.Energy -= selectedTurret.energyCost;

		//Build Turret
		GameObject turret = Instantiate(turretToBuild, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

		GameObject turretBE = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);

	}

	public void SpentEnergyAnimation()
	{

		spentEnergyText.text = "-" + selectedTurret.energyCost;
		spentEnergyText.enabled = true;
	}

	#endregion
}

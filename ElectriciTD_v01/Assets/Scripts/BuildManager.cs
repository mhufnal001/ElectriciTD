using UnityEngine;

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

	public bool CanBuild { get { return turretToBuild != null; } }
	public bool HasMoney { get { return GameManager.Energy >= selectedTurret.energyCost; } }

    #endregion

    #region Unity Methods
    void Start()
    {
		selectedTurret = null;
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

		Debug.Log("Turret Built! Energy Left:" + GameManager.Energy);

	}

	#endregion
}

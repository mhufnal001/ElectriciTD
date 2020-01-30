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

	public bool CanBuild { get { return turretToBuild != null; } }

    #endregion

    #region Unity Methods
    void Start()
    {

    }

    void Update()
    {
        
    }
	#endregion

	#region User Methods

	public void SetTurretToBuild(GameObject _turret)
	{
		turretToBuild = _turret;
	}

	public void BuildTurretOn(Node node)
	{
		//Build Turret
		GameObject turret = Instantiate(turretToBuild, node.GetBuildPosition(), Quaternion.identity);
		node.turret = turret;

	}

	#endregion
}

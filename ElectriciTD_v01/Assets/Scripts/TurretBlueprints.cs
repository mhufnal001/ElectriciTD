using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turrets")]
public class TurretBlueprints : ScriptableObject
{
	#region Variables

	[Header("Turret Model")]
	public GameObject turretPrefab;

	[Header("Turret Stats")]
	public float range;
	public float fireRate;
	public float turnSpeed;

	public int attackValue;
	public int health;
	public int value;
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

	#endregion
}

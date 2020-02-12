using UnityEngine;

public class Turret : MonoBehaviour
{
	#region Variables

	public TurretBlueprints currentBlueprint;

	public float range;
	public float fireRate;
	public float turnSpeed;

	public int ad;
	public int hp;
	public int energyCost;
	public int sellPrice;

	#endregion

	#region Unity Methods
	void Awake()
    {
		range = currentBlueprint.range;
		fireRate = currentBlueprint.fireRate;
		turnSpeed = currentBlueprint.turnSpeed;
		ad = currentBlueprint.attackValue;
		hp = currentBlueprint.health;
		energyCost = currentBlueprint.energyCost;
		sellPrice = currentBlueprint.sellPrice;
	}

    void Update()
    {
        
    }
    #endregion

    #region User Methods
    #endregion	
}

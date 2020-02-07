using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	#region Variables

	public Color hasEnergyColor;
	public Color noEnergyColor;
	public Vector3 turretPosOffset;

	[Header("Optional")]
	public GameObject turret;
	[Header("NonOptional")]
	public CollectionsUI collections;

	private Renderer r;
	private Color startColor;

	BuildManager bm;

    #endregion

    #region Unity Methods

    void Start()
    {
		bm = BuildManager.instance;

		r = GetComponent<Renderer>();
		startColor = r.material.color;
    }

    void Update()
    {
        
    }

	private void OnMouseEnter()
	{
		if (!bm.CanBuild)
		{
			return;
		}

		if (bm.HasMoney && bm.cUpgrades == false)
		{
			r.material.color = hasEnergyColor;
		}
		else if (!bm.HasMoney || bm.cUpgrades == true)
		{
			r.material.color = noEnergyColor;
		}

	}

	private void OnMouseExit()
	{
		r.material.color = startColor;
		return;
	}

	private void OnMouseDown()
	{
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!bm.CanBuild)
		{
			return;
		}

		if (turret != null)
		{
			bm.SelectNode(this);
			return;
		}

		BuildTurret(bm.GetTurretToBuild());
		collections.SpentEnergyAnimation(bm.selectedTurret.energyCost);
	}

	#endregion

	#region User Methods

	public Vector3 GetBuildPosition()
	{
		return transform.position + turretPosOffset;
	}

	void BuildTurret (TurretBlueprints blueprint)
	{
		if (GameManager.Energy < blueprint.energyCost)
		{
			Debug.Log("Not Enough Money!");
			collections.energyAnim.ResetTrigger("SpentEnergy");

			return;
		}

		GameManager.Energy -= blueprint.energyCost;
		collections.energyAnim.SetTrigger("SpentEnergy");


		//Build Turret
		GameObject _turret = Instantiate(blueprint.turretPrefab, GetBuildPosition(), Quaternion.identity);
		turret = _turret;

		GameObject turretBE = Instantiate(bm.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(turretBE, 5f);
	}

	#endregion
}

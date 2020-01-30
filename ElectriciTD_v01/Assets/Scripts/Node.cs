﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	#region Variables

	public Color hoverColor;
	public Vector3 turretPosOffset;

	[Header("Optional")]
	public GameObject turret;

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
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		if (!bm.CanBuild)
		{
			return;
		}
		r.material.color = hoverColor;
	}

	private void OnMouseExit()
	{
		r.material.color = startColor;
	}

	private void OnMouseDown()
	{
		if (!bm.CanBuild)
		{
			return;
		}

		if (turret != null)
		{
			Debug.Log("Cannot Place Turret Here.");
			return;
		}

		bm.BuildTurretOn(this);

	}

	#endregion

	#region User Methods

	public Vector3 GetBuildPosition()
	{
		return transform.position + turretPosOffset;
	}

	#endregion
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
	#region Variables
	public static Transform[] points;

	#endregion

	#region Functions

	private void Awake()
	{
		points = new Transform[transform.childCount];
		for (int i = 0; i < points.Length; i++)
		{
			points[i] = transform.GetChild(i);

		}
	}

	#endregion	
}

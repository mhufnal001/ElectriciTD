using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies")]
public class EnemyTypes : ScriptableObject
{
	#region Variables

	[Header("Enemy Model")]
	public GameObject enemyPrefab;

	[Header("Enemy Stats")]
	public int attackValue;
	public int health;
	public int energyValue;

	#endregion
}

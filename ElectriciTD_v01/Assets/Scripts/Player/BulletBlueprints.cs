using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullets")]
public class BulletBlueprints : ScriptableObject
{
	#region Variables

	[Header("Bullet Model")]
	public GameObject bulletPrefab;
	public GameObject impactEffect;

	[Header("Bullet Stats")]
	public int bulletDamage;
	public float bulletSpeed;

	#endregion
}

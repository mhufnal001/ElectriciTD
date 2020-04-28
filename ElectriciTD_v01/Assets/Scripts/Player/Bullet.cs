using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region Variables

	private Transform target;
	public BulletBlueprints currentBullet;
	public Turret turret;

	public int bulletDamage;
	public float bulletSpeed;

	#endregion

	#region Unity Methods
	void Start()
    {
		bulletDamage = currentBullet.bulletDamage;
		bulletSpeed = currentBullet.bulletSpeed;

		turret = FindObjectOfType<Turret>();
	}

    void Update()
    {
		if (bulletDamage != turret.ad)
		{
			bulletDamage = turret.ad;
		}


		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		//track bullet distance and make it face target while travelling
		Vector3 dir = target.position - transform.position;
		float distanceThisFrame = bulletSpeed * Time.deltaTime;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 25f).eulerAngles;
		transform.rotation = Quaternion.Euler(90f, rotation.y, 90f);

		if (dir.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
    #endregion

    #region User Methods

	public void Chase (Transform _target)
	{
		target = _target;
	}

	void HitTarget()
	{
		GameObject effectInstance = Instantiate(currentBullet.impactEffect, transform.position, transform.rotation);

		Damage(target);

		Destroy(effectInstance, 1f);
		Destroy(gameObject);
		return;
	}

	void Damage(Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		e.TakeDamage(bulletDamage);

	}

    #endregion	
}

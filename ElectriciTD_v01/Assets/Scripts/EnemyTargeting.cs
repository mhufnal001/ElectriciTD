using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
	#region Variables

	public GameObject bulletPrefab;

	public Transform target;
	public Transform pivotPoint;
	public Transform firePoint;

	public TurretBlueprints currentTurret;
	public string enemyTag = "Enemy";

	private float fireCountdown = 0f;
    #endregion

    #region Unity Methods
    void Start()
    {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
		if (target == null)
		{
			return;
		}

		//find target direction, convert to euler in order to only manipulate y-axis, use Lerp to smooth turret look speed
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * currentTurret.turnSpeed).eulerAngles;
		pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (fireCountdown <= 0f)
		{
			Shoot();
			fireCountdown = 1f / currentTurret.fireRate;

		}

		fireCountdown -= Time.deltaTime;

    }

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, currentTurret.range);

	}
	#endregion

	#region User Methods

	void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;

		foreach (var enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= currentTurret.range)
		{
			target = nearestEnemy.transform;
		}
		else
		{
			target = null;
		}

	}

	void Shoot()
	{

		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(90f,0f,90f));

		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Chase(target);
		}

	}

    #endregion	
}

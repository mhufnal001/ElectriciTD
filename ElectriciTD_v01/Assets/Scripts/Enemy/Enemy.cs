using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Variables

	public EnemyTypes currentType;
	public GameObject enemyDeathEffect;

	public int hp;
    #endregion

    #region Unity Methods
    void Start()
    {
		hp = currentType.health;
    }

    void Update()
    {
        
    }
    #endregion

    #region User Methods

	public void TakeDamage(int amount)
	{
		hp -= amount;

		if (hp <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		GameManager.instance.collections.GainedEnergy(currentType.energyValue);
		GameObject effect = Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);

		WaveSpawner.EnemiesAlive--;
		Destroy(gameObject);
		Destroy(effect, 5f);
	}

    #endregion	
}

using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Variables

	public EnemyTypes currentType;

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

	public void TakeDamage(int amount)
	{
		currentType.health -= amount;

		if (currentType.health <= 0)
		{
			Die();
		}
	}

	void Die()
	{
		Destroy(gameObject);

	}

    #endregion	
}

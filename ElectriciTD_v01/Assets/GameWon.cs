using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWon : MonoBehaviour
{
	#region Variables

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

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Menu()
	{
		Debug.Log("Go To Menu.");
	}

	#endregion
}

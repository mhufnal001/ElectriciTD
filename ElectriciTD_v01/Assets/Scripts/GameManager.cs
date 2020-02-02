using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Variables

	public static int Energy;
	public static int Power;

	public int startEnergy = 450;
	public int startPower = 0;

	public static int maxEnergy = 9999;
	public static int maxPower = 100000;

    #endregion

    #region Unity Methods
    void Start()
    {
		Energy = startEnergy;
		Power = startPower;
    }

    void Update()
    {
        
    }

    #endregion

    #region User Methods



    #endregion	
}

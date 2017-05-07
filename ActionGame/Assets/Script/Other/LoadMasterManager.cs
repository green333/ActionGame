using UnityEngine;
using System.Collections;

public class LoadMasterManager : MonoBehaviour {

	void Awake()
    {
        LoadEnemyBaseMaster.Instace.Initialize();
        LoadEnemyGrowthMaster.Instace.Initialize();
        LoadEnemySpawnMaster.Instace.Initialize();
        LoadPlayerBaseMaster.Instace.Initialize();
        LoadWeaponMaster.Instace.Initialize();
    }
}

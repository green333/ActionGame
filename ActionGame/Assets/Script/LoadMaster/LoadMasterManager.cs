using UnityEngine;
using System.Collections;

public class LoadMasterManager : MonoBehaviour {

	void Awake()
    {
        // マスタデータをここで読み込む。
        // マスタデータを読み込むクラスを追加したときは、ここに同じように初期化を行ってください。
        LoadEnemyBaseMaster.Instance.Initialize();
        LoadEnemyGrowthMaster.Instance.Initialize();
        LoadEnemySpawnMaster.Instance.Initialize();
        LoadPlayerBaseMaster.Instance.Initialize();
        LoadWeaponMaster.Instance.Initialize();
        LoadItemMaster.Instance.Initialize();
        LoadStageMaster.Instance.Initialize();
    }
}

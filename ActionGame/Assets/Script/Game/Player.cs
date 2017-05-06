using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    /*
     * セーブデータがない場合はマスタﾃﾞｰﾀから取得するようにする。
     * PlayerBaseMaster.ParamとWeaponMaster.Paramを変数でPlayerクラスにもたせる。
     * 
     * const PLAYER_INIT_LEVEL = 1;
     * const string PLAYER_INIT_WEAPON = "武器名";
     */

    const int PLAYER_INIT_LEVEL = 1;
    const string PLAYER_INIT_WEAPON = "マシュ";

    PlayerBaseMaster.Param baseParam;
    WeaponMaster.Param weaponParam;

    // Use this for initialization
	void Start () {

        //  ロード出来たらロードしてnullならマスターから取得
        if (SaveData.Instance.Load(SaveData.KEY_SLOT_1) == null)
        {
            baseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(PLAYER_INIT_LEVEL);
            weaponParam = LoadWeaponMaster.Instace.getWeaponInfo(PLAYER_INIT_WEAPON);
        }
        else
        {
            baseParam = SaveData.Instance.Load(SaveData.KEY_SLOT_1).playerParam;
            weaponParam = SaveData.Instance.Load(SaveData.KEY_SLOT_1).playerWeaponParam;
        }

        Debug.Log("-----------プレイヤーデータ-----------");
        Debug.Log("level：" + this.baseParam.level);
        Debug.Log("hp：" + this.baseParam.hp);
        Debug.Log("atk：" + this.baseParam.atk);
        Debug.Log("def：" + this.baseParam.def);
        Debug.Log("mgc：" + this.baseParam.mgc);
        Debug.Log("spd：" + this.baseParam.spd);
        Debug.Log("next_exp：" + this.baseParam.next_exp);
        Debug.Log("--------------------------------------");

        Debug.Log("-----------プレイヤー武器データ-----------");
        Debug.Log("name：" + this.weaponParam.name);
        Debug.Log("id：" + this.weaponParam.id);
        Debug.Log("type：" + this.weaponParam.type);
        Debug.Log("atk：" + this.weaponParam.atk);
        Debug.Log("--------------------------------------");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

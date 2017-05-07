using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    /*
     * セーブデータがない場合はマスタﾃﾞｰﾀから取得するようにする。
     * PlayerBaseMaster.ParamとWeaponMaster.Paramを変数でPlayerクラスにもたせる。
     * 
     * const PLAYER_INIT_LEVEL = 1;
     * const string PLAYER_INIT_WEAPON = "武器名";
     * 
     * PlayerクラスにレベルアップフラグとPlayerBaseMasterParamの変数をもう一つ追加する。
     * プレイヤー基本マスタからデータから現在のレベルのステータスを取得する際、次のレベルのステータスも取得しておく。
     * Playerクラスに経験値加算関数を追加。
     * 
     * 現在セーブ処理は全くしていないのでセーブはされていない
     */

    /// <summary>
    /// マスター取得時初期レベルと武器用定数
    /// 必要があれば後でしかるべきところに定義しなおす
    /// </summary>
    const int PLAYER_INIT_LEVEL = 1;
    const string PLAYER_INIT_WEAPON = "マシュ";

    /// <summary> プレイヤー基本パラメータ </summary>
    PlayerBaseMaster.Param baseParam;
    /// <summary> プレイヤー進化後パラメータ </summary>
    PlayerBaseMaster.Param nextBaseParam;
    /// <summary> プレイヤー武器パラメータ </summary>
    WeaponMaster.Param weaponParam;

    /// <summary> レベルアップフラグ </summary>
    bool isLevelUp;
    /// <summary> 経験値 </summary>
    int exp;

    // Use this for initialization
	void Start () {

        exp = 0;

        //  ロード出来たらロードしてnullならマスターから取得
        if (SaveData.Instance.Load(SaveData.KEY_SLOT_1) == null)
        {
            //  初期レベル
            baseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(PLAYER_INIT_LEVEL);
            weaponParam = LoadWeaponMaster.Instace.getWeaponInfo(PLAYER_INIT_WEAPON);

            //  次レベル
            nextBaseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(PLAYER_INIT_LEVEL + 1);
        }
        else
        {
            //  現在レベル
            baseParam = SaveData.Instance.Load(SaveData.KEY_SLOT_1).characterStatus.param;
            weaponParam = SaveData.Instance.Load(SaveData.KEY_SLOT_1).characterStatus.weaponParam;

            //  次レベル
            int nextLv = SaveData.Instance.Load(SaveData.KEY_SLOT_1).characterStatus.param.level + 1;
            nextBaseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(nextLv);
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
	void Update ()
    {
        //  Aキーで経験値加算
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddExp(1);
        }

        //  レベルアップに必要な経験値を得たらレベルアップフラグを立てる
        if (this.exp >= this.baseParam.next_exp)
        {
            this.isLevelUp = true;
        }

        //  レベルアップフラグが立っていたら
        if (this.isLevelUp)
        {
            //  マスターから現在レベル +1 の情報を取得
            baseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(this.baseParam.level + 1);
            Debug.Log("-----------レベルアップ後プレイヤーデータ-----------");
            Debug.Log("level：" + this.baseParam.level);
            Debug.Log("hp：" + this.baseParam.hp);
            Debug.Log("atk：" + this.baseParam.atk);
            Debug.Log("def：" + this.baseParam.def);
            Debug.Log("mgc：" + this.baseParam.mgc);
            Debug.Log("spd：" + this.baseParam.spd);
            Debug.Log("next_exp：" + this.baseParam.next_exp);
            Debug.Log("--------------------------------------");

            nextBaseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(this.baseParam.level + 1);
            Debug.Log("-----------レベルアップ後次レベルプレイヤーデータ-----------");
            Debug.Log("level：" + this.nextBaseParam.level);
            Debug.Log("hp：" + this.nextBaseParam.hp);
            Debug.Log("atk：" + this.nextBaseParam.atk);
            Debug.Log("def：" + this.nextBaseParam.def);
            Debug.Log("mgc：" + this.nextBaseParam.mgc);
            Debug.Log("spd：" + this.nextBaseParam.spd);
            Debug.Log("next_exp：" + this.nextBaseParam.next_exp);
            Debug.Log("--------------------------------------");

            //  取得経験値が次レベルのレベルアップ必要値未満ならレベルアップを止める
            if(this.exp < nextBaseParam.next_exp)
            {
                this.isLevelUp = false;
            }
        }
	}

    /// <summary>
    /// 経験値加算
    /// </summary>
    /// <param name="value"></param>
    void AddExp(int value)
    {
        this.exp += value;
    }
}

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

    /// <summary>
    /// セーブデータが存在する場合は、セーブデータから取得し、なければプレイヤーの初期情報を取得する
    /// </summary>
    void LoadPlayerData()
    {
        //  ロード出来たらロードしてnullならマスターから取得
        if (SaveData.Instance.Load(SaveData.KEY_SLOT_1) == null)
        {
            //  初期レベル
            baseParam       = LoadPlayerBaseMaster.Instace.GetPlayerInfo(PLAYER_INIT_LEVEL);
            baseParam.exp  = 0;
            weaponParam     = LoadWeaponMaster.Instace.getWeaponInfo(PLAYER_INIT_WEAPON);
        }
        else
        {
            //  現在レベル
            SaveData saveData = SaveData.Instance.Load(SaveData.KEY_SLOT_1);
            baseParam       = saveData.playerParam;
            weaponParam     = saveData.weaponParam;
        }

        // 次のレベルのステータスを取得する
        nextBaseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(baseParam.level + 1);
    }

    // Use this for initialization
    void Start () {

        // プレイヤーのデータを読み込む
        LoadPlayerData();
    }
	
	// Update is called once per frame
	void Update ()
    {
        LevelUp();

        //  Aキーで経験値加算
        if(Input.GetKeyDown(KeyCode.A))
        {
            AddExp(1000);
        }

	}

    /// <summary>
    /// 経験値加算(レベルアップが可能ならフラグを立てる)
    /// </summary>
    /// <param name="value"></param>
    void AddExp(int value)
    {
        // すでにレベルがMAXなら加算処理はする必要ないのでreturn
        if(this.baseParam.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
        {
            return;
        }

        // 加算
        this.baseParam.exp += value;

        // レベルアップが可能ならフラグを立てる
        if(this.baseParam.exp >= this.nextBaseParam.exp)
        {
            isLevelUp = true;
        }
    }

    /// <summary>
    /// レベルアップが可能ならレベルアップを行う
    /// </summary>
    void LevelUp()
    {
        // レベルアップが可能でなければreturn
        if(isLevelUp == false)
        {
            return;
        }

        int subExp = 0;
        
        while(true)
        {
            
            // 必要経験値に満たしているかを算出する
            subExp = this.baseParam.exp - this.nextBaseParam.exp;

            // 0を下回す場合、レベルアップに必要な経験値に到達していないのでbreak;
            if (subExp < 0) { break; }

            Debug.Log("----------------------レベルアップ前のプレイヤーのステータス-----------------------------");
            LoadPlayerBaseMaster.Instace.DebugLog(this.baseParam);
            Debug.Log("-----------------------------------------------------------------------------------------");

            // 次のレベルのステータスを現在のステータスに反映させる。
            this.baseParam     = this.nextBaseParam;

            Debug.Log("----------------------レベルアップ後のプレイヤーのステータス-----------------------------");
            LoadPlayerBaseMaster.Instace.DebugLog(this.baseParam);
            Debug.Log("-----------------------------------------------------------------------------------------");

            // 現在のレベルが上限かどうかをチェック
            if (this.baseParam.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
            {
                this.baseParam.exp = 0;
                break;
            }

            // 差分を現在の経験値に格納
            this.baseParam.exp = subExp;

            // 次のレベルのステータスを取得
            this.nextBaseParam = LoadPlayerBaseMaster.Instace.GetPlayerInfo(this.nextBaseParam.level + 1);

        }

        isLevelUp = false;
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Player : BaseBehaviour
{
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
    private const int PLAYER_INIT_LEVEL = 1;
    private const string PLAYER_INIT_WEAPON = "マシュ";

    /// <summary> アイテムリスト </summary>
    private List<ItemInfo> itemList = null;

    /// <summary> プレイヤー基本パラメータ </summary>
    private CharacterStatus status = null;

    /// <summary> プレイヤー武器パラメータ </summary>
    private WeaponMaster.Param weaponParam = null;

    /// <summary> リジッドボディ </summary>
    private Rigidbody rig = null;

    /// <summary> レベルアップのエフェクト処理の実行制御フラグ(trueなら実行可能) </summary>
    public bool enableLvUpEffectExecute { get; set; }

    /// <summary> プレイヤーの状態 </summary>
    private enum STATE : int {
        WAIT = 0,
        MOVE,
    }
    private STATE state;

    /// <summary> 回転方向 </summary>
    private enum ROTDIR : int {
        NONE = 0,
        LEFT,
        RIGHT,
    }
    private ROTDIR rotDir;
    
    /// <summary>
    /// Start時にGameMainで呼ばれる初期化関数
    /// </summary>
    public override void BaseStart()
    {
        this.rotDir = ROTDIR.NONE;
        this.state = STATE.WAIT;
        this.rig = this.gameObject.GetComponent<Rigidbody>();
        this.enableLvUpEffectExecute = false;
        LoadPlayerData();
    }

    /// <summary>
    /// 固定フレームレートの更新
    /// </summary>
    public override void BaseFixedUpdate()
    {
        Move();
        Rotate();
    }

    /// <summary>
    /// 更新
    /// </summary>
    public override void BaseUpdate()
    {
        //  入力受け付け
        InputReception();
    }

    /// <summary>
    /// 入力受け付け
    /// </summary>
    private void InputReception()
    {
        this.state = STATE.WAIT;
        this.rotDir = ROTDIR.NONE;

        //  移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.rotDir = ROTDIR.NONE;
            this.state = STATE.MOVE;
        }

        //  回転
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.rotDir = ROTDIR.LEFT;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.rotDir = ROTDIR.RIGHT;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    public void Move()
    {
        switch(this.state)
        {
            case STATE.WAIT:
                this.rig.velocity = Vector3.zero;
                break;
            case STATE.MOVE:
                this.rig.AddForce(this.transform.forward);
                break;
        }
    }

    /// <summary>
    /// 回転
    /// </summary>
    public void Rotate()
    {
        switch (this.rotDir)
        {
            case ROTDIR.NONE:
                this.rig.angularVelocity = Vector3.zero;
                break;
            case ROTDIR.LEFT:
                this.rig.AddTorque(Vector3.down);
                break;
            case ROTDIR.RIGHT:
                this.rig.AddTorque(Vector3.up);
                break;
        }
    }

    /// <summary>
    /// セーブデータが存在する場合はセーブデータから取得し、なければプレイヤーの初期情報を取得する
    /// </summary>
    private void LoadPlayerData()
    {
        //  ロード出来たらロードしてnullならマスターから取得
        if (SaveData.Instance.Load(SaveData.KEY_SLOT_1) == null)
        {
            status = new CharacterStatus();

            //  初期レベル
            status.param = LoadPlayerBaseMaster.instance.GetPlayerInfo(PLAYER_INIT_LEVEL);
            status.exp   = 0;
            weaponParam = LoadWeaponMaster.instance.GetWeaponInfo(PLAYER_INIT_WEAPON);
        }
        else
        {
            //  現在レベル
            SaveData saveData = SaveData.Instance.Load(SaveData.KEY_SLOT_1);
            status          = saveData.playerParam;
            weaponParam     = saveData.weaponParam;
        }

        //　ステータスをログに表示
        LoadPlayerBaseMaster.instance.DebugLog(this.status.param);
        // 武器情報をログに表示
        LoadWeaponMaster.instance.DebugLog(this.weaponParam);
    }

    /// <summary>
    /// 経験値の加算を行う。
    /// レベルアップが可能ならレベルアップを行い、レベルアップのエフェクト処理を実行可能にする。
    /// </summary>
    /// <param name="value"></param>
    public void AddExp(int addExp)
    {
        do
        {
            // すでにレベルがMAXなら加算処理はする必要ないのでreturn
            if (this.status.param.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
            {
                break;
            }

            // 加算
            this.status.exp += addExp;

            // レベルアップに必要な経験値に達していない場合はbreak
            if (this.status.exp < this.status.param.next_exp)
            {
                break;
            }

            // レベルが上がったのでレベルアップのエフェクト処理を実行可能にする。
            enableLvUpEffectExecute = true;

            // レベルアップ処理。
            LevelUp();

        } while (false);
    }

    /// <summary>
    /// レベルアップを行う
    /// </summary>
    private void LevelUp()
    {
        int subExp = 0;

        LogExtensions.OutputInfo("----------------------レベルアップ前のプレイヤーのステータス-----------------------------");
        LoadPlayerBaseMaster.instance.DebugLog(this.status.param);
        LogExtensions.OutputInfo("-----------------------------------------------------------------------------------------");

        while (true)
        {
            
            // 必要経験値に満たしているかを算出する
            subExp = this.status.exp - this.status.param.next_exp;

            // 0を下回す場合、レベルアップに必要な経験値に到達していないのでbreak;
            if (subExp < 0) { break; }

            // 差分を現在の経験値に格納
            this.status.exp = subExp;
           
            // 次のレベルのステータスを取得
            this.status.param = LoadPlayerBaseMaster.instance.GetPlayerInfo(this.status.param.level + 1);

            if(this.status.param.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
            {
                this.status.exp = 0;
                break;
            }

        }

        LogExtensions.OutputInfo("----------------------レベルアップ後のプレイヤーのステータス-----------------------------");
        LoadPlayerBaseMaster.instance.DebugLog(this.status.param);
        LogExtensions.OutputInfo("-----------------------------------------------------------------------------------------");
    }
}

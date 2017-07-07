﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
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
    private const string PLAYER_INIT_HAVE_ITEM_NAME = "薬草";
    private const int PLAYER_INIT_HAVE_ITEM_NUM = 3;

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

    /// <summary>
    /// 移動と回転スピード
    /// エディタで弄りたいからpublicにしてる
    /// </summary>
    public float moveSpeed = 3.0f;
    public float rotSpeed = 2.0f;

    /// <summary> 傾き </summary>
    private float slopeHorizontal;
    private float slopeVertical;
    
    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        this.rig = this.gameObject.GetComponent<Rigidbody>();
        this.enableLvUpEffectExecute = false;
        this.slopeHorizontal = this.slopeVertical = 0.0f;
        LoadPlayerData();
    }

    /// <summary>
    /// 固定フレームレートの更新
    /// </summary>
    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        Rotate();
    }

    /// <summary>
    /// 回転
    /// アナログスティックを傾けた角度に徐々に回転させる
    /// 
    /// このサイトぱくった
    /// http://dev3104.hateblo.jp/entry/2016/04/07/185529
    /// </summary>
    private void Rotate()
    {
        this.slopeHorizontal = Input.GetAxis("Horizontal") * Time.deltaTime * this.moveSpeed;
        this.slopeVertical = Input.GetAxis("Vertical") * Time.deltaTime * this.moveSpeed;
        Vector3 dir = new Vector3(this.slopeHorizontal, 0, this.slopeVertical);

        //  一定以上動かしているか
        if (dir.magnitude > 0.01f)
        {
            float step = this.rotSpeed * Time.deltaTime;
            Quaternion q = Quaternion.LookRotation(dir);

            //  線形補間
            this.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, q, step);
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        this.rig.velocity = new Vector3(this.slopeHorizontal, 0.0f, this.slopeVertical);
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
            itemList = new List<ItemInfo>();
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.param = LoadItemMaster.instance.GetItemInfo(PLAYER_INIT_HAVE_ITEM_NAME);
            itemInfo.num = PLAYER_INIT_HAVE_ITEM_NUM;
            itemList.Add(itemInfo);
        }
        else
        {
            //  現在レベル
            SaveData saveData = SaveData.Instance.Load(SaveData.KEY_SLOT_1);
            status          = saveData.playerParam;
            weaponParam     = saveData.weaponParam;
            itemList        = saveData.itemList;
        }

        //　ステータスをログに表示
        LoadPlayerBaseMaster.instance.DebugLog(this.status.param);
        // 武器情報をログに表示
        LoadWeaponMaster.instance.DebugLog(this.weaponParam);
        // アイテム情報をログに表示
        for(int i = 0; i < itemList.Count; ++i)
        {
            LoadItemMaster.instance.DebugLog(itemList[i]);
        }
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

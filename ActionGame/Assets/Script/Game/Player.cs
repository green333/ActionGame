using UnityEngine;
using System;
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

    /// <summary> ゲームパッドのキーコード </summary>
    private Dictionary<string, KeyCode> PAD_KEYCODE = new Dictionary<string, KeyCode>()
    {
                           {"CIRCLE",   KeyCode.Joystick1Button2},
                           {"CROSS",    KeyCode.Joystick1Button1},
                           {"SQUARE",   KeyCode.Joystick1Button0},
                           {"TRIANGLE", KeyCode.Joystick1Button3},
                           {"L1",       KeyCode.Joystick1Button4},
                           {"L2",       KeyCode.Joystick1Button6},
                           {"L3",       KeyCode.Joystick1Button10},
                           {"R1",       KeyCode.Joystick1Button5},
                           {"R2",       KeyCode.Joystick1Button7},
                           {"R3",       KeyCode.Joystick1Button11},
                           {"SELECT",   KeyCode.Joystick1Button8},
                           {"START",    KeyCode.Joystick1Button9},
                           {"PS",        KeyCode.Joystick1Button12},
    };

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

        PadTest();
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
            LogExtensions.OutputInfo("プレイヤー初期データの読み込みを開始します");
            status = new CharacterStatus();

            // プレイヤーステータスを読み込む
            // (所持経験値パラムは0にしておくしておく)
            status.param = LoadPlayerBaseMaster.instance.GetPlayerInfo(PLAYER_INIT_LEVEL);
            status.exp   = 0;

            // 武器情報を読み込む
            weaponParam = LoadWeaponMaster.instance.GetWeaponInfo(PLAYER_INIT_WEAPON);

            // アイテム情報を読み込む
            itemList = new List<ItemInfo>();
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.param = LoadItemMaster.instance.GetItemInfo(PLAYER_INIT_HAVE_ITEM_NAME);
            itemInfo.num = PLAYER_INIT_HAVE_ITEM_NUM;
            itemList.Add(itemInfo);

        }
        else
        {
            LogExtensions.OutputInfo("セーブデータからプレイヤーデータの読み込みを開始します");
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

        LogExtensions.OutputInfo("プレイヤーデータの読み込みが終了しました");
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

    /// <summary>
    /// ゲームパッド関係のテストコード
    /// 後で捨てる
    /// </summary>
    private void PadTest()
    {
        //  ボタンのテスト
        if (Input.GetKeyDown(PAD_KEYCODE["CIRCLE"]))
        {
            Debug.Log("〇ボタン");
        }
        //foreach (KeyValuePair<string, KeyCode> dic in PAD_KEYCODE)
        //{
        //    if (Input.GetButtonDown(dic.Key))
        //    {
        //        Debug.Log(dic.Key + "ボタン押下");
        //    }
        //}

        //  右スティックのテスト
        if (Input.GetAxis("Horizontal2") != 0)
        {
            Debug.Log("右スティック操作(横)");
        }
        if (Input.GetAxis("Vertical2") != 0)
        {
            Debug.Log("右スティック操作(縦)");
        }

        ////  十字キーのテスト
        //string[] jyuuji = {"LEFT", "RIGHT", "TOP", "BOTTOM" };
        //foreach (string str in jyuuji)
        //{
        //    if (Input.GetAxis(str) == 1)
        //    {
        //        Debug.Log(str+"ボタン押下");
        //        break;
        //    }
        //}

        //if (Input.anyKeyDown)
        //{
        //    foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(code))
        //        {
        //            Debug.Log(code);
        //            break;
        //        }
        //    }
        //}
    }
}

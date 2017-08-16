using UnityEngine;
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

    /// <summary> 移動と回転スピード </summary>
    private float moveSpeed;
    private float rotSpeed;

    /// <summary> 移動量 </summary>
    private float moveH;
    private float moveV;

    /// <summary> ゲームパッドのキーコード </summary>
    private enum PadKeyCode {
        CIRCLE = KeyCode.Joystick1Button2,
        CROSS = KeyCode.Joystick1Button1,
        SQUARE = KeyCode.Joystick1Button0,
        TRIANGLE = KeyCode.Joystick1Button3,
        L1 = KeyCode.Joystick1Button4,
        L2 = KeyCode.Joystick1Button6,
        L3 = KeyCode.Joystick1Button10,
        R1 = KeyCode.Joystick1Button5,
        R2 = KeyCode.Joystick1Button7,
        R3 = KeyCode.Joystick1Button11,
        SELECT = KeyCode.Joystick1Button8,
        START = KeyCode.Joystick1Button9,
        PS = KeyCode.Joystick1Button12,
    }

    /// <summary> アニメーション関連 </summary>
    private Animator animator;
    private enum ANIMATION : int { WAIT = 0, WALK, RUN, JUMP, ATTACK }
    private float normalizedTime;

    /// <summary> 状態遷移管理用 </summary>
    private enum STEP : int { MOVE = 0, JUMP, ATTACK }
    private STEP step = STEP.MOVE;

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        enableLvUpEffectExecute = false;
        moveH = moveV = 0.0f;
        animator = GetComponent<Animator>();
        LoadPlayerData();
    }

    /// <summary>
    /// 固定フレームレートの更新
    /// </summary>
    private void FixedUpdate()
    {
        // 移動
        rig.velocity = new Vector3(moveH, 0.0f, moveV);
    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        moveH = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        moveV = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        StateCtrl();
    }

    /// <summary>
    /// 回転
    /// アナログスティックを傾けた角度に徐々に回転させる
    /// </summary>
    private void Rotate()
    {
        Vector3 dir = new Vector3(moveH, 0.0f, moveV);

        if (dir.magnitude > 0.01f)
        {
            float step = rotSpeed * Time.deltaTime;
            Quaternion q = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, q, step);
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (Input.GetKeyDown((KeyCode)PadKeyCode.CROSS)) step = STEP.JUMP;
        if (Input.GetKeyDown((KeyCode)PadKeyCode.CIRCLE)) step = STEP.ATTACK;

        float h = Mathf.Abs(Input.GetAxis("Horizontal"));
        float v = Mathf.Abs(Input.GetAxis("Vertical"));

        //  走りに移行する値
        float runTrigger = 1.0f;

        if (h >= runTrigger || v >= runTrigger)
        {
            moveSpeed = 200.0f;
            rotSpeed = 30.0f;
            SetAnimation((int)ANIMATION.RUN);
        }
        else if (h >= runTrigger * 0.5f || v >= runTrigger * 0.5f)
        {
            moveSpeed = 100.0f;
            rotSpeed = 15.0f;
            SetAnimation((int)ANIMATION.WALK);
        }
        else// if (h != 0 && v != 0)
        {
            moveSpeed = 0.0f;
            rotSpeed = 0.0f;
            SetAnimation((int)ANIMATION.WAIT);
        }
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump()
    {
        SetAnimation((int)ANIMATION.JUMP);

        //  他のモーションの状態が残っていれば処理しない
        bool rb = animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run");
        bool wb = animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk");
        if (rb || wb) return;

        normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (normalizedTime <= 0.2f)         //  踏ん張り
        {
            moveSpeed = 0.0f;
            rotSpeed = 0.0f;
        }
        else if (normalizedTime <= 0.7f)    //  空中
        {
            moveSpeed = 200.0f;
            rotSpeed = 15.0f;
        }
        else if (normalizedTime <= 0.8f)    //  着地開始
        {
            moveSpeed = 0.0f;
            rotSpeed = 0.0f;
        }
        else                                //  着地完了
        {
            moveSpeed = 0.0f;
            rotSpeed = 0.0f;
            step = STEP.MOVE;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void Attack()
    {
        SetAnimation((int)ANIMATION.ATTACK);

        //  他のモーションの状態が残っていれば処理しない
        bool rb = animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Run");
        bool wb = animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Walk");
        bool jb = animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump");
        if (rb || wb || jb) return;

        moveSpeed = 0.0f;
        rotSpeed = 0.0f;

        normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if (normalizedTime >= 1.0f)
        {
            moveSpeed = 100.0f;
            rotSpeed = 15.0f;
            step = STEP.MOVE;
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
        LoadPlayerBaseMaster.instance.DebugLog(status.param);
        // 武器情報をログに表示
        LoadWeaponMaster.instance.DebugLog(weaponParam);
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
            if (status.param.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
            {
                break;
            }

            // 加算
            status.exp += addExp;

            // レベルアップに必要な経験値に達していない場合はbreak
            if (status.exp < status.param.next_exp)
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
        LoadPlayerBaseMaster.instance.DebugLog(status.param);
        LogExtensions.OutputInfo("-----------------------------------------------------------------------------------------");

        while (true)
        {
            
            // 必要経験値に満たしているかを算出する
            subExp = status.exp - status.param.next_exp;

            // 0を下回す場合、レベルアップに必要な経験値に到達していないのでbreak;
            if (subExp < 0) { break; }

            // 差分を現在の経験値に格納
            status.exp = subExp;
           
            // 次のレベルのステータスを取得
            status.param = LoadPlayerBaseMaster.instance.GetPlayerInfo(status.param.level + 1);

            if(status.param.level == LoadPlayerBaseMaster.PLAYER_LEVEL_MAX)
            {
                status.exp = 0;
                break;
            }

        }

        LogExtensions.OutputInfo("----------------------レベルアップ後のプレイヤーのステータス-----------------------------");
        LoadPlayerBaseMaster.instance.DebugLog(status.param);
        LogExtensions.OutputInfo("-----------------------------------------------------------------------------------------");
    }



    /// <summary>
    /// 状態遷移
    /// </summary>
    private void StateCtrl()
    {
        switch (step)
        {
            case STEP.MOVE:
                Move();
                Rotate();
                break;

            case STEP.JUMP:
                Jump();
                Rotate();
                break;

            case STEP.ATTACK:
                Attack();
                break;
        }
    }

    /// <summary>
    /// アニメーションをセットする
    /// </summary>
    /// <param name="value">アニメーション番号</param>
    private void SetAnimation(int value)
    {
        if (animator.GetInteger("state") != value) animator.SetInteger("state", value);
    }
}

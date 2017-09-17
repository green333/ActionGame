using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 全エネミーの親クラスとなる
/// 
/// 敵成長マスタパラメーターは保持するが、敵基本マスタパラメーターは保持しない。
/// 生成される敵は各々レベルが違うためパラメーターが変わるから保持する必要性があるが、
/// 敵基本は敵の名前の敵が落とすアイテム情報を管理しており、これはレベルによって変わることはないため、
/// ここでは管理しない
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>　敵のパラメータークラス </summary>
    protected class Parameter : EnemyGrowthMaster.Param
    {
        public int nowHp;
        public Parameter(EnemyGrowthMaster.Param param)
        {
            id     = param.id;
            level  = param.level;
            hp     = param.hp;
            atk    = param.atk;
            def    = param.def;
            spd    = param.spd;
            exp    = param.exp;
            nowHp  = param.hp;
        }
    }

    /// <summary> 敵パラメーター </summary>
    protected Parameter m_param = null;

    /// <summary> 敵UIのプレハブデータ </summary>
    [SerializeField] private GameObject m_enemyUIPrefab = null;

    /// <summary> プレハブのインスタンスデータ </summary>
    private GameObject m_enemyUIObj = null;

    /// <summary> プレハブにアタッチされたスクリプト </summary>
    protected EnemyUI m_enemyUI = null;
 
    /// <summary>
    /// 生成時に敵情報を初期化
    /// </summary>
    /// <param name="param"></param>
    public void Initialize(EnemyGrowthMaster.Param param)
    {
        m_param = new Parameter(param);

        // TODO:今は座標を適当に決める
        this.transform.position = new Vector3(UnityEngine.Random.Range(-50.0f, 50.0f),0.0f, UnityEngine.Random.Range(-50.0f, 50.0f));

        // UI情報を初期化
        m_enemyUIObj = Instantiate(m_enemyUIPrefab);
        m_enemyUIObj.transform.parent = gameObject.transform;
        m_enemyUI = m_enemyUIObj.GetComponent<EnemyUI>();
        m_enemyUI.Initialize(m_param.hp, transform.position + new Vector3(0, 2.5f, 0));

        // タグ名を設定する
        this.gameObject.tag = "Enemy";
    }

    /// <summary>
    /// 初期化
    /// </summary>
    private void Start()
    {
  
    }

    /// <summary> 敵が死んだかどうか </summary>
    /// <returns></returns>
    public bool IsDead() { return m_param.nowHp == 0; }

    /// <summary>死んだ時の行動を記述する </summary>
    public virtual void DeadAction() { LogExtensions.OutputWarn("DeadAction()をオーバーライドしていません。 enemyId = " + m_param.id + ",level = " + m_param.level); }

    /// <summary>
    /// 敵にダメージを与える。TODO:引数は仮
    /// </summary>
    /// <param name="playerAtk"></param>
    public void AddDamage(int playerAtk)
    {
        // TODO:エフェクトがないため当たったか当たってないかがわかりづらいため、ログ出力する
        LogExtensions.OutputInfo("攻撃がヒットしました！");

        // TODO:仮
        m_param.nowHp -= playerAtk;
        if(m_param.nowHp < 0) { m_param.nowHp = 0; }
            m_enemyUI.SubHPValue(playerAtk);
    }

}

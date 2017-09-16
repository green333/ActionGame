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

    /// <summary>
    /// 生成時に敵情報を初期化
    /// </summary>
    /// <param name="param"></param>
    public void Initialize(EnemyGrowthMaster.Param param)
    {
        m_param = new Parameter(param);
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

    /// <summary>
    /// 敵にダメージを与える。TODO:引数は仮
    /// </summary>
    /// <param name="playerAtk"></param>
    public void AddDamage(int playerAtk)
    {
        // TODO:仮
        m_param.nowHp -= playerAtk;
    }
}

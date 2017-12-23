using UnityEngine;
using System.Collections;

public class LoadWeaponMaster : TextMasterManager
{
    /// <summary>
    /// 自身のインスタンス
    /// </summary>
    static LoadWeaponMaster _instance = new LoadWeaponMaster();

    /// <summary>
    /// インスタンス取得プロパティ
    /// </summary>
    static public LoadWeaponMaster instance { get { return _instance; } }

    /// <summary>
    /// 武器マスタのファイルパス
    /// </summary>
    const string filename = "Assets/Resources/MasterData/武器マスタ.txt";

    /// <summary>
    /// 指定した名前に一致する武器情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public WeaponMaster.Param GetWeaponInfo(string name)
    {
        WeaponMaster.Param ret = null;

        base.Open(filename);

        string getJsonStr = base.Search(name);
        if(getJsonStr != null)
        {
            ret = JsonUtility.FromJson<WeaponMaster.Param>(getJsonStr);
        }
        base.Close();

        return ret;
    }

    /// <summary>
    /// 武器パラメーターをログに出力する
    /// </summary>
    /// <param name="parma"></param>
    public void DebugLog(WeaponMaster.Param param)
    {
        LogExtensions.OutputInfo("[武器マスタ] => " +
            "[id:"      + param.Id   + "] " +
            "[name:"    + param.Name + "] " +
            "[type:"    + param.Type + "] " +
            "[atk:"     + param.Atk  + "] " +
            "[spd:"     + param.Spd  + "] "
        );
    }
}

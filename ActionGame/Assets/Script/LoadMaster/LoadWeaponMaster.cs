using UnityEngine;
using System.Collections;

public class LoadWeaponMaster : TextMasterManager
{
    const string filename = "Resources/MasterData/武器マスタ.txt";

    /// <summary>
    /// 指定した名前に一致する武器情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public WeaponMaster.Param getWeaponInfo(string name)
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
    public void DebugLog(WeaponMaster.Param parma)
    {
        LogExtensions.OutputInfo("id   =" + parma.id);
        LogExtensions.OutputInfo("name =" + parma.name);
        LogExtensions.OutputInfo("type =" + parma.type);
        LogExtensions.OutputInfo("atk  =" + parma.atk);
    }
}

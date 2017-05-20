using UnityEngine;
using System.Collections;

public class LoadWeaponMaster
{

    /// <summary>
    /// 指定した名前に一致する武器情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public WeaponMaster.Param getWeaponInfo(string name)
    {
        return null;
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

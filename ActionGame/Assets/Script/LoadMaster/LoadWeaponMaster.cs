using UnityEngine;
using System.Collections;

public class LoadWeaponMaster : BaseSingleton<LoadWeaponMaster>
{

    /// <summary>
    /// 武器マスタを格納する変数
    /// </summary>
    WeaponMaster master = null;

    /// <summary>
    /// 初期化時に武器マスタを読み込む
    /// </summary>
    public void Initialize()
    {
        if (null == (master = Resources.Load<WeaponMaster>("MasterData/WeaponMaster")))
        {
            LogExtensions.OutputError("failed to resources load weapon master");
        }
    }

    /// <summary>
    /// 指定した名前に一致する武器情報を取得する
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public WeaponMaster.Param getWeaponInfo(string name)
    {
        int index = 0;
        bool bChecked = false;

        foreach (WeaponMaster.Param param in master.list)
        {
            if (param.name == name)
            {
                bChecked = true;
                break;
            }
            ++index;
        }

        if(bChecked == false)
        {
            // 指定した名前に一致するデータがない
            LogExtensions.OutputWarn("there is no weapon master matching the There is no data matching the specified name, name = " + name);
            return null;
        }
        WeaponMaster.Param temp = new WeaponMaster.Param();
        temp.name   = master.list[index].name;
        temp.id     = master.list[index].id;
        temp.type   = master.list[index].type;
        temp.atk    = master.list[index].atk;

        return temp;
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

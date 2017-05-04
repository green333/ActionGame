using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ScriptableObjectCreater : UnityEditor.Editor
{
    //    CSVファイルの読み込みパス
    static string load_file_path = Application.dataPath + "/Resources/MasterData/";

    //    出力先
    static string output_file_path = "Assets/Resources/";


    /// <summary>
    /// 敵基本マスタをAsset化します
    /// <summary>
    [UnityEditor.MenuItem("CreateScriptableObject/敵基本マスタ")]
    static void CreateEnemyBaseMasterSO()
    {
        EnemyBaseMaster instance = CreateInstance<EnemyBaseMaster>();

        List<string> list = CSVLoader.Instance.LoadCSVAll(load_file_path + "敵基本マスタ.csv");
        if(list == null)
        {
            Debug.Log("敵基本マスタ.csv not found");
            return;
        }

        int columnNum = 24;

        for(int i = columnNum; i < list.Count -1;)
        {
            EnemyBaseMaster.Param param = new EnemyBaseMaster.Param();

            param.name = list[i++];
            param.id = int.Parse(list[i++]);
            param.level = int.Parse(list[i++]);
            param.hp = int.Parse(list[i++]);
            param.atk = int.Parse(list[i++]);
            param.def = int.Parse(list[i++]);
            param.spd = int.Parse(list[i++]);
            param.exp = int.Parse(list[i++]);
            instance.list.Add(param);
        }

        UnityEditor.AssetDatabase.CreateAsset(instance, output_file_path + "EnemyBaseMaster.asset");
        UnityEditor.AssetDatabase.SaveAssets();
    }


    /// <summary>
    /// ステージマスタをAsset化します
    /// <summary>
    [UnityEditor.MenuItem("CreateScriptableObject/ステージマスタ")]
    static void CreateStageMasterSO()
    {
        StageMaster instance = CreateInstance<StageMaster>();

        List<string> list = CSVLoader.Instance.LoadCSVAll(load_file_path + "ステージマスタ.csv");
        if(list == null)
        {
            Debug.Log("ステージマスタ.csv not found");
            return;
        }

        int columnNum = 18;

        for(int i = columnNum; i < list.Count -1;)
        {
            StageMaster.Param param = new StageMaster.Param();

            param.name = list[i++];
            param.id = int.Parse(list[i++]);
            param.chapter1_id = int.Parse(list[i++]);
            param.chapter2_id = int.Parse(list[i++]);
            param.chapter3_id = int.Parse(list[i++]);
            param.chapter4_id = int.Parse(list[i++]);
            instance.list.Add(param);
        }

        UnityEditor.AssetDatabase.CreateAsset(instance, output_file_path + "StageMaster.asset");
        UnityEditor.AssetDatabase.SaveAssets();
    }


    /// <summary>
    /// 敵出現マスタをAsset化します
    /// <summary>
    [UnityEditor.MenuItem("CreateScriptableObject/敵出現マスタ")]
    static void CreateEnemySpawnMasterSO()
    {
        EnemySpawnMaster instance = CreateInstance<EnemySpawnMaster>();

        List<string> list = CSVLoader.Instance.LoadCSVAll(load_file_path + "敵出現マスタ.csv");
        if(list == null)
        {
            Debug.Log("敵出現マスタ.csv not found");
            return;
        }

        int columnNum = 24;

        for(int i = columnNum; i < list.Count -1;)
        {
            EnemySpawnMaster.Param param = new EnemySpawnMaster.Param();

            param.stage_id = int.Parse(list[i++]);
            param.chapter_id = int.Parse(list[i++]);
            param.enemy1_name = list[i++];
            param.enemy1_lvpm = int.Parse(list[i++]);
            param.enemy2_name = list[i++];
            param.enemy2_lvpm = int.Parse(list[i++]);
            param.enemy3_name = list[i++];
            param.enemy3_lvpm = int.Parse(list[i++]);
            instance.list.Add(param);
        }

        UnityEditor.AssetDatabase.CreateAsset(instance, output_file_path + "EnemySpawnMaster.asset");
        UnityEditor.AssetDatabase.SaveAssets();
    }


}
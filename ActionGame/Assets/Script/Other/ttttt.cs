using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Hoge
{
    public string name;
    public int coin;

    public Hoge(string name, int coin)
    {
        this.name = name;
        this.coin = coin;
    }
}

public class ttttt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsWrap.Instance.DeleteAll();
        //List<int> tinko = new List<int>();
        //tinko.Add(11);
        //tinko.Add(22);
        //tinko.Add(33);
        //tinko.Add(44);
        //tinko.Add(55);
        //Debug.Log("tinko:" + tinko.Count);
        //PlayerPrefsWrap.Instance.DeleteAll();
        //PlayerPrefsWrap.Instance.SaveGeneric<List<int>>("list", tinko);
        //List<int> intList = new List<int>(PlayerPrefsWrap.Instance.LoadGeneric<List<int>>("list"));
        //Debug.Log(intList.Count);
        //for (int i = 0; i < intList.Count; i++)
        //{
        //    Debug.Log(i.ToString() + "：" + intList[i]);
        //}
        Hoge hoge = new Hoge("名前", 1000);
        PlayerPrefsWrap.Instance.SaveGeneric<Hoge>("me", hoge);
        Hoge fuga = PlayerPrefsWrap.Instance.LoadGeneric<Hoge>("me");
        Debug.Log(fuga.name);
        Debug.Log(fuga.coin);
        
        //  BGM
        SoundUtil.Instance.PlayBGM("bgm01");
    }
	
	// Update is called once per frame
	void Update () {

        //  SE
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundUtil.Instance.PlaySE("se01");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SoundUtil.Instance.PlaySE("se02");
        }
	}
}

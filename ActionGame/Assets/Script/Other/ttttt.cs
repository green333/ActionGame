using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Hoge
{
    public string name { get; set; }
    public int coin { get; set; }

    private Hoge() { }

    private static Hoge instance = new Hoge();
    public static Hoge Instance { get { return instance; } }

    public void Save()
    {
        PlayerPrefsWrap.Instance.SaveGeneric<Hoge>("hoge", instance);
    }

    public Hoge Load()
    {
        return PlayerPrefsWrap.Instance.LoadGeneric<Hoge>("hoge");
    }
}

public class ttttt : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsWrap.Instance.DeleteAll();

        Hoge.Instance.name = "name01";
        Hoge.Instance.coin = 999;
        //Hoge.Instance.Save();

        if (Hoge.Instance.Load() != null)
        {
            Debug.Log("coin : " + Hoge.Instance.Load().coin);
            Debug.Log("name : " + Hoge.Instance.Load().name);
        }

        //  BGM
        SoundUtil.Instance.LoadBGM();
        SoundUtil.Instance.PlayBGM(1);
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

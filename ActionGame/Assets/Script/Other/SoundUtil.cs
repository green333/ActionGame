﻿using UnityEngine;
using System.Collections;

/// <summary>
/// サウンド周りの便利クラス
/// 
/// TODO: [ 未実装項目 ]
/// 　一時停止・停止
/// 　フェードイン・フェードアウト
/// </summary>
public class SoundUtil{

    /// <summary> seフォルダ </summary>
    private const string SE_BASE_PASH = "Sound/SE/";
    /// <summary> bgmフォルダ </summary>
    private const string BGM_BASE_PASH = "Sound/BGM/";

    /// <summary>
    /// コンストラクタ
    /// </summary>
    private SoundUtil()
    {
    }

    /// <summary> シングルトン </summary>
    private static SoundUtil instance = new SoundUtil();
    public static SoundUtil Instance
    {
        get { return instance; }
    }

    /// <summary> 音鳴らす用 </summary>
    private GameObject soundPlayerObj;

    /// <summary> オーディオクリップ </summary>
    private AudioClip audioClip;

    /// <summary> オーディオソース </summary>
    private AudioSource audioSource;

    /// <summary>
    /// SE再生
    /// </summary>
    /// <param name="filePath"></param>
    public void PlaySE(string filePath)
    {
        if (soundPlayerObj == null)
        {
            //  AudioSource コンポーネントが付いたゲームオブジェクトを生成
            soundPlayerObj = new GameObject("SoundObj");
            soundPlayerObj.AddComponent<AudioSource>();

            //  生成したオブジェクトから AudioSource コンポーネントを取得
            audioSource = soundPlayerObj.GetComponent<AudioSource>();
        }

        //  リソースからサウンドデータをロード
        audioClip = (AudioClip)Resources.Load(SE_BASE_PASH + filePath);
        if (audioClip == null)
        {
            return;
        }

        //  再生
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// BGM再生
    /// </summary>
    /// <param name="filePath"></param>
    public void PlayBGM(string filePath)
    {
        if (soundPlayerObj == null)
        {
            //  AudioSource コンポーネントが付いたゲームオブジェクトを生成
            soundPlayerObj = new GameObject("SoundObj");
            soundPlayerObj.AddComponent<AudioSource>();

            //  生成したオブジェクトから AudioSource コンポーネントを取得
            audioSource = soundPlayerObj.GetComponent<AudioSource>();
        }

        //  リソースからサウンドデータをロード
        audioClip = (AudioClip)Resources.Load(BGM_BASE_PASH + filePath);
        if (audioClip == null)
        {
            return;
        }

        //  AudioClip を登録
        audioSource.clip = audioClip;
        //  ループ再生設定
        audioSource.loop = true;
        //  再生
        audioSource.Play();
    }

}

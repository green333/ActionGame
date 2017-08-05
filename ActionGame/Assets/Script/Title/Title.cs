using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class Title : MonoBehaviour
{
    /// <summary> タイトル画面のボタン </summary>
    [SerializeField] private Button btnNewGame;
    [SerializeField] private Button btnLoading;
    [SerializeField] private Button btnOption;
    [SerializeField] private Button btnExit;

    /// <summary> フェード </summary>
    private FadeManager fade;

    /// <summary>
    /// 初期化
    /// </summary>
	void Start ()
    {
        ButtonEvent();

        fade = new FadeManager();
	}

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        fade.FadeUpdate();
    }

    /// <summary>
    /// ボタンクリックイベント
    /// </summary>
    private void ButtonEvent()
    {
        this.btnNewGame.onClick.AddListener(() => {
            //  NewGameを選んだときの処理
            LogExtensions.OutputInfo("onClickNewGame.");
            fade.SetFadeMode(FadeManager.FADE_STATUS.FADE_IN);
        });

        this.btnLoading.onClick.AddListener(() => {
            //  Loadingを選んだ時の処理
            LogExtensions.OutputInfo("onClickLoading.");
        });

        this.btnOption.onClick.AddListener(() => {
            //  Optionを選んだ時の処理
            LogExtensions.OutputInfo("onClickOption.");
        });

        this.btnExit.onClick.AddListener(() => {
            //  Exitを選んだ時の処理
            LogExtensions.OutputInfo("onClickExit.");
            //  アプリを終了する。
            //  エディタでは終了しない。
            Application.Quit();
        });
    }
}

using UnityEngine;
using UnityEngine.UI;

public sealed class FadeManager
{
    private const float DEFAULT_FADE_SPEED = 0.01f;
    private const string FADE_CANVAS_PATH = "Prefab/CanvasFade";

    private Image fadeImg;
    private float alpha;

    public enum FADE_STATUS : int
    {
        NONE = 0,
        FADE_IN,
        FADE_OUT,
        FADE_FINISH,
    }
    private FADE_STATUS status = FADE_STATUS.NONE;

    /// <summary>
    /// フェード終了イベント
    /// </summary>
    public delegate void OnFadeFinish();
    public OnFadeFinish onFadeFinish;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    FadeManager() { }

    /// <summary>
    /// シングルトン
    /// </summary>
    private static FadeManager instance;
    public static FadeManager Instance {
        get
        {
            if (instance == null)
            {
                GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(FADE_CANVAS_PATH));
                obj.GetComponent<Canvas>().worldCamera = Camera.main;

                instance = new FadeManager();
                if (instance.fadeImg == null)
                {
                    instance.fadeImg = obj.GetComponentInChildren<Image>();
                }
            }

            return instance;
        }
    }

    public void FadeUpdate()
    {
        switch(this.status)
        {
            case FADE_STATUS.FADE_IN:
                FadeIn();
                break;
            case FADE_STATUS.FADE_OUT:
                FadeOut();
                break;
            case FADE_STATUS.FADE_FINISH:
                FadeFinish();
                break;
        }
    }

    public void SetFadeMode(FADE_STATUS status)
    {
        this.status = status;
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    private void FadeIn()
    {
        if (this.alpha < 1.0f)
        {
            this.alpha += DEFAULT_FADE_SPEED;
            this.fadeImg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
        else
        {
            this.status = FADE_STATUS.FADE_FINISH;
        }
    }

    /// <summary>
    /// フェードアウト
    /// </summary>
    private void FadeOut()
    {
        if (this.alpha > 0.0f)
        {
            this.alpha -= DEFAULT_FADE_SPEED;
            this.fadeImg.color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
        else
        {
            this.status = FADE_STATUS.FADE_FINISH;
        }
    }

    /// <summary>
    /// フェード終了時の処理
    /// </summary>
    private void FadeFinish()
    {
        if (onFadeFinish == null)
        {
            return;
        }

        onFadeFinish();
        onFadeFinish = null;
    }

}

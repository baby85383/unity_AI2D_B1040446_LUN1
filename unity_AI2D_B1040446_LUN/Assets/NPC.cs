using UnityEngine;
using UnityEngine.UI;   //引用 介面 API
using System.Collections;

public class NPC : MonoBehaviour
{
    #region 欄位
    // 定義列舉
    //修飾詞 列舉 列舉名稱 { 列舉內容, .... }
    public enum state
    {
        //一般、尚未完成、完成
        normal, notcomplete, complete
    }
    //使用列舉
    //修飾詞 類型 名稱
    public state _state;

    [Header("對話")]
    public string sayStart = "嗨，你好，我可以請你幫我蒐集六枚金幣嗎?";
    public string sayNotComplete = "你還沒找到六枚金幣喔...";
    public string sayComplete = "感謝你幫我找到六枚金幣~~";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion

    public AudioClip soundSay;
    public float soundspeed = 0.5f;
    private AudioSource aud;
    internal static NPC score;

    public GameObject END;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
        score = this; 
        
    }
    //2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰到物件為"QQ"
        if (collision.name == "e04")
        {
            Say();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "e04")
        {
            SayClose();
        }
    }

    /// <summary>
    /// 對話:打字效果
    /// </summary>
    private void Say()
    {
        //畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish)
        {
            _state = state.complete;

            Invoke("End", 4f);
        }

        //文字介面.文字 = 對話1
        switch (_state)
        {
            case state.normal:
                StartCoroutine(ShowDialog(sayStart));               //開始對話
                _state = state.notcomplete;
                break;
            case state.notcomplete:
                StartCoroutine(ShowDialog(sayNotComplete));         //未完成對話
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));            //完成對話
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                               //清空文字

        for (int i = 0; i < say.Length; i++)             //迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();           //累加每個文字
            aud.PlayOneShot(soundSay, 1.5f);
            yield return new WaitForSeconds(speed);      //等待
        }
    }

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        objCanvas.SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }

    void End()
    {
        END.SetActive(true);
    }
}


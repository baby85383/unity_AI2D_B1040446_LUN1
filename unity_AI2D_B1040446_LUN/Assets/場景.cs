using UnityEngine;
using UnityEngine.SceneManagement; // 引用 場景管理 API

public class 場景 : MonoBehaviour
{
    public void Replay()
    {
        //Application.LoadLevel("遊戲");     //舊版 API
        SceneManager.LoadScene("SampleScene");    //新版 API
    }

    public void Quit()
    {
        Application.Quit(); // 應用程式.離開遊戲
    }
}

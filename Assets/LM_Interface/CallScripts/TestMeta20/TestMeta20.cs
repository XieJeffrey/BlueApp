using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestMeta20 : MonoBehaviour
{
    public UIEventListener testButton1;

    public UIEventListener testButton2;

    public UIEventListener testButton3;

    public Text errorText;
    // Start is called before the first frame update
    void Start()
    {
        testButton1.onClick = Test1;

        testButton2.onClick = Test2;

        testButton3.onClick = Test3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Test1(GameObject go, PointerEventData eventdata)
    {
        StartCoroutine(TestWeatherHttp());
    }

    void Test2(GameObject go, PointerEventData eventdata)
    {
        StartCoroutine(TestOtherHttp());
    }

    void Test3(GameObject go, PointerEventData eventdata)
    {
        //StartCoroutine(TestHuangHttp());
        string url = "http://api.orangel.us/njascher/Admin/login";
        string data = "phone=" + "13813829757" +
            "&password=" + "123456";
        StartCoroutine(Post(url, data));
    }


    private IEnumerator TestWeatherHttp()
    {
        WWW www = new WWW("http://www.weather.com.cn/data/sk/101010100.html");
        yield return www;
        if (www.error != null)
            {
            // Debug.LogError(www.error);
            errorText.text = "报错报错报错:" + www.error;  
            }
        else if(www.isDone)
        {
            errorText.text = "回调信息 :" + www.text;  
        }
    }

    private IEnumerator TestOtherHttp()
    {
        WWW www = new WWW("https://list.ty200772.com/CopywritingList.aspx");
        yield return www;
        if (www.error != null)
        {
            // Debug.LogError(www.error);
            errorText.text ="报错报错报错:" + www.error;
        }
        else if (www.isDone)
        {
            errorText.text = "回调信息 :" + www.text;
        }
    }

    private IEnumerator TestHuangHttp()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
      
        headers["Content-Type"] = "application/x-www-form-urlencoded";
        headers["OSTOKEN"] = "mwpVJ5K0tjojPkaL";
                      
        WWW www = new WWW("http://api.orangel.us/njascher/Admin/login");
        yield return www;
        if (www.error != null)
        {
            // Debug.LogError(www.error);
            errorText.text = "报错报错报错:" + www.error;
        }
        else if (www.isDone)
        {
            errorText.text = "回调信息 :" + www.text;
        }
    }

    IEnumerator Post(string url, string data = null)
    {
        // Dictionary<string, string> headers = new Dictionary<string, string>();
        using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        {
            byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(data);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bs);
            www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");

            yield return www.Send();
            if (www.error != null)
            {
                errorText.text = "报错报错报错:" + www.error;
            }
            else if (www.responseCode == 200)
            {
                errorText.text = "回调信息 :" + www.downloadHandler.text;
            }
        }
    }
}

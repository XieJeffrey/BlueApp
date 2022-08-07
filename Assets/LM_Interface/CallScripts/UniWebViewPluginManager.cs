using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Schema;
using DG.Tweening;
using UnityEngine.UI;

namespace LM_Workspace {
    public class UniWebViewPluginManager : SingletonMono<UniWebViewPluginManager> {
        [HideInInspector]
        public UniWebView webView;


        protected override void Awake() {
            base.Awake();
            CreateWebView();
        }

        //	// Use this for initialization
        //	void Start () 
        //{

        //}



        void CreateWebView() {
            if (webView == null) {
                var webViewGameObject = new GameObject("UniWebView");
                webView = webViewGameObject.AddComponent<UniWebView>();
            }
            else {
                webView = GameObject.Find("UniWebView").GetComponent<UniWebView>();
            }



            webView.OnMessageReceived += (view, message) => {
                if (message.Path.Equals("GetAddress")) {
                    string value = message.Args["address"];
                    string address = value.Split('-')[0];
                    string street = value.Split('-')[1];
                    // CreateAppoimentManager.getInstance.mapRectBig.GetComponent<CanvasGroup>().DOFade(0f, 1f);
                    // LoadBaiDuMap(CreateAppoimentManager.getInstance.)
                    //  Debug.Log("address ::: " + value);
                    CreateAppoimentManager.getInstance.pNameText.text = address;
                    // CreateAppoimentManager.getInstance.addressInput.text = address;
                    LoadBaiDuMap(address, street, CreateAppoimentManager.getInstance.cityText.text);
                    // UniWebViewPluginManager.getInstance.st
                }

            };
            //webView.CleanCache ();
        }

        public void LoadBaiDuMapInit(string _city) {
            Debug.Log("_city:"+ _city);
            if (webView == null)
                CreateWebView();
            //UniWebView uniWeb = CreateWebView();
            webView.gameObject.SetActive(true);
            webView.ReferenceRectTransform = CreateAppoimentManager.getInstance.mapRect;
            // CreateAppoimentManager.getInstance.mapRectBig.GetComponent<Image>().raycastTarget = false;
            //string url = "http://www.midianvr.com:86/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            //string url = "http://192.168.0.107/BaiduMap/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            string url = "http://www.midianvr.com:86/BaiduMap/baidu_Map_City.html?city=" + _city;
            webView.Load(url, false);
            webView.Show();
        }

        public void LoadBaiDuMap(string _address, string _pName, string _city) {
            //UniWebView uniWeb = CreateWebView();
            CreateAppoimentManager.getInstance.mapSearch.GetComponent<CanvasGroup>().DOPause();
            CreateAppoimentManager.getInstance.mapSearch.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate {

                CreateAppoimentManager.getInstance.mapSearch.SetActive(false);
            });
            webView.gameObject.SetActive(true);
            webView.ReferenceRectTransform = CreateAppoimentManager.getInstance.mapRect;
            // CreateAppoimentManager.getInstance.mapRectBig.GetComponent<Image>().raycastTarget = false;
            //string url = "http://www.midianvr.com:86/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            //string url = "http://192.168.0.107/BaiduMap/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            string url = "http://www.midianvr.com:86/BaiduMap/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            webView.Load(url, false);
            webView.Show();
            //CreateAppoimentManager.getInstance.mapSearch.SetActive(false);
        }

        public void LoadSearchMap(string _city) {
            //UniWebView uniWeb = CreateWebView();
            webView.gameObject.SetActive(true);
            webView.ReferenceRectTransform = CreateAppoimentManager.getInstance.mapRectBig;
            //CreateAppoimentManager.getInstance.mapRectBig.GetComponent<Image>().raycastTarget = true;
            //string url = "http://www.midianvr.com:86/baidu_Map.html?address=" + _address + "&p_name=" + _pName + "&city=" + _city;
            //string url = "http://192.168.0.108/BaiduMap/baidu_Map_search.html?"+"city=" + _city;
            string url = "http://www.midianvr.com:86/BaiduMap/baidu_Map_search.html?" + "city=" + _city;
            webView.Load(url, false);
            webView.Show();
        }

        public void ReturnSearchMap(string _city) {

            CreateAppoimentManager.getInstance.mapSearch.GetComponent<CanvasGroup>().DOPause();
            CreateAppoimentManager.getInstance.mapSearch.GetComponent<CanvasGroup>().DOFade(0f, 1f).OnComplete(delegate {

                CreateAppoimentManager.getInstance.mapSearch.SetActive(false);
            });

            webView.gameObject.SetActive(true);
            webView.ReferenceRectTransform = CreateAppoimentManager.getInstance.mapRect;
            string url = "http://www.midianvr.com:86/BaiduMap/baidu_Map_City.html?city=" + _city;
            webView.Load(url, false);
            webView.Show();

        }

        public void HideBaiDuMap() {
            if (webView != null) {
                GameObject.Destroy(webView);
                //webView.gameObject.SetActive(false);
            }
        }

    }
}


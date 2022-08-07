using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using LM_Workspace;
using GD;
using UnityEngine.Networking;
using System.Text;

namespace XMWorkspace
{
    public class InterfaceManager : MonoBehaviour
    {
       public IEnumerator Get(string url, string data = null, System.Action<JsonData> callback = null) {
            if (!UtilManager.getInstance.CheckInternet())
                yield break;
            Dictionary<string, string> headers = new Dictionary<string, string>();

            Debug.Log(url + data);
            UnityWebRequest request = UnityWebRequest.Get(url + data);
            request.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            if(UserData.instance.token!="")
                request.SetRequestHeader("OSTOKEN", UserData.instance.token);

            yield return request.SendWebRequest();
            if (request.responseCode == 200) {
                string text = request.downloadHandler.text;
                Debug.Log(text);
                JsonData jobject = JsonMapper.ToObject(text);
                callback?.Invoke(jobject);              
            }
            else {
                Debug.LogError(request.error);
            }
        }

        public IEnumerator Post(string url, string data = null, System.Action<JsonData> callback = null)
        {
            if (!UtilManager.getInstance.CheckInternet())
                yield break;
            // Debug.Log("hahaha");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            WWW www;
            byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(data);

            if (UserData.instance.token != "")
            {
#if UNITY_EDITOR 
                print("加入了token");
                print(UserData.instance.token);
#endif
                headers["Content-Type"] = "application/x-www-form-urlencoded";
                headers["OSTOKEN"] = UserData.instance.token;
                www = new WWW(url, bs, headers);
            }
            else
            {
                www = new WWW(url, bs);
            }
#if UNITY_EDITOR 
            Debug.Log("url:" + url);
            Debug.Log("data:" + data);
#endif
            //UIManager.instance.ShowLoggerMsg("请求的url:" + url);
            // UIManager.instance.ShowLoggerMsg("请求的data:" + data);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError(www.error);
                // UIManager.instance.ShowLoggerMsg("www错误:" + www.error);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(www.text);
#endif
                if (callback != null)
                {
                    JsonData jobject = JsonMapper.ToObject(www.text);
                    callback(jobject);
                }
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
        }
        IEnumerator QrCodePost(string url, string data = null, System.Action<Texture2D> callback = null)
        {
            if (!UtilManager.getInstance.CheckInternet())
                yield break;
            // Debug.Log("hahaha");
            Dictionary<string, string> headers = new Dictionary<string, string>();
            WWW www;
            byte[] bs = System.Text.UnicodeEncoding.ASCII.GetBytes(data);

            if (UserData.instance.token != "")
            {
#if UNITY_EDITOR 
                print("加入了token");
#endif
                headers["Content-Type"] = "application/x-www-form-urlencoded";
                headers["OSTOKEN"] = UserData.instance.token;
                www = new WWW(url, bs, headers);
            }
            else
            {
                www = new WWW(url, bs);
            }
#if UNITY_EDITOR 
            Debug.Log("url:" + url);
            Debug.Log("data:" + data);
#endif
            //UIManager.instance.ShowLoggerMsg("请求的url:" + url);
            // UIManager.instance.ShowLoggerMsg("请求的data:" + data);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError(www.error);
                // UIManager.instance.ShowLoggerMsg("www错误:" + www.error);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(www.text);
#endif
                Texture2D texture = www.texture;
                if (callback != null)
                {
                   
                    callback(texture);
                }
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
        }

        //        IEnumerator Post(string url, string data = null, System.Action<JsonData> callback = null)
        //        {
        //            Debug.Log(data);
        //           // Dictionary<string, string> headers = new Dictionary<string, string>();
        //            using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        //            {
        //                byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(data);
        //                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //                if (UserData.instance.token != "")
        //                {
        //#if UNITY_EDITOR
        //                    print("加入了token");
        //#endif
        //                   // headers["Content-Type"] = "application/x-www-form-urlencoded";
        //                   // headers["OSTOKEN"] = UserData.instance.token;
        //                    //  www = new WWW(url, bs, headers);
        //                    www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bs);
        //                    www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        //                    www.SetRequestHeader("OSTOKEN", UserData.instance.token);

        //                }
        //                else
        //                {
        //                    www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bs);
        //                }
        //#if UNITY_EDITOR
        //               // Debug.Log("url:" + url);
        //               // Debug.Log("data:" + data);
        //#endif
        //               // UIManager.instance.ShowLoggerMsg("请求的url:" + url);
        //                // UIManager.instance.ShowLoggerMsg("请求的data:" + data);
        //                yield return www.Send();
        //                if (www.error != null)
        //                {
        //                    // Debug.LogError(www.error);
        //                    UIManager.instance.ShowLoggerMsg("www错误:" + www.error);
        //                }
        //                else if(www.responseCode == 200)
        //                {
        //#if UNITY_EDITOR
        //                    Debug.Log(www.downloadHandler.text);
        //#endif

        //            if (callback != null)
        //            {
        //                JsonData jobject = JsonMapper.ToObject(www.downloadHandler.text);
        //                callback(jobject);
        //            }
        //            Resources.UnloadUnusedAssets();
        //            System.GC.Collect();
        //        }
        //    }
        //}

        //        IEnumerator Post(string url, System.Action<JsonData> callback = null)
        //        {
        //            // Dictionary<string, string> headers = new Dictionary<string, string>();
        //            using (UnityWebRequest www = new UnityWebRequest(url, "POST"))
        //            {
        //               // byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(data);
        //                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        //                if (UserData.instance.token != "")
        //                {
        //#if UNITY_EDITOR
        //                    Debug.Log(UserData.instance.token);
        //                    print("加入了token");
        //#endif
        //                    // headers["Content-Type"] = "application/x-www-form-urlencoded";
        //                    // headers["OSTOKEN"] = UserData.instance.token;
        //                    //  www = new WWW(url, bs, headers);
        //                   // www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bs);
        //                    www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        //                    www.SetRequestHeader("OSTOKEN", UserData.instance.token);

        //                }
        //                else
        //                {
        //                   // www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bs);
        //                }
        //#if UNITY_EDITOR
        //                // Debug.Log("url:" + url);
        //                // Debug.Log("data:" + data);
        //#endif
        //                // UIManager.instance.ShowLoggerMsg("请求的url:" + url);
        //                // UIManager.instance.ShowLoggerMsg("请求的data:" + data);
        //                yield return www.Send();
        //                if (www.error != null)
        //                {
        //                    // Debug.LogError(www.error);
        //                    UIManager.instance.ShowLoggerMsg("www错误:" + www.error);
        //                }
        //                else if (www.responseCode == 200)
        //                {
        //#if UNITY_EDITOR
        //                    Debug.Log(www.downloadHandler.text);
        //#endif

        //            if (callback != null)
        //            {
        //                JsonData jobject = JsonMapper.ToObject(www.downloadHandler.text);
        //                callback(jobject);
        //            }
        //            Resources.UnloadUnusedAssets();
        //            System.GC.Collect();
        //        }
        //    }
        //}


      public  IEnumerator Post(string url, System.Action<JsonData> callback = null)
        {
            if (!UtilManager.getInstance.CheckInternet())
                yield break;
            // Debug.Log("hahaha");

            Dictionary<string, string> headers = new Dictionary<string, string>();
            WWW www;
            //byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes(data);

            if (UserData.instance.token != "")
            {
#if UNITY_EDITOR
                print("加入了token");
#endif
                headers["Content-Type"] = "application/x-www-form-urlencoded";
                headers["OSTOKEN"] = UserData.instance.token;
                www = new WWW(url, null, headers);
                //Debug.Log(UserData.instance.token);
            }
            else
            {
                www = new WWW(url);
            }
#if UNITY_EDITOR
            Debug.Log("url:" + url);
#endif
            yield return www;
            if (www.error != null)
            {
                //Debug.LogError(www.error);
            }
            else
            {
#if UNITY_EDITOR
                Debug.Log(www.text);
#endif
                if (callback != null)
                {
                    JsonData jobject = JsonMapper.ToObject(www.text);
                    callback(jobject);
                }
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }
        }
        private byte[] FileContent(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);

                return buffur;
            }
            catch (System.Exception ex)
            {
#if UNITY_EDITOR
                Debug.Log(ex.Message);
#endif
                return null;
            }
            finally
            {
                if (fs != null)
                {
                    //关闭资源  
                    fs.Close();
                }
            }
        }

        /// <summary>
        /// 上传图片到服务器
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>
		public IEnumerator PostImage(string filePath, string fileName, string typeName, string mimeType, System.Action<JsonData> callBack = null)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/UserUpload/upLoadImg";

            WWWForm form = new WWWForm();
            form.AddField("OSTOKEN", UserData.instance.token);
            //  form.AddBinaryData("tempImg", FileContent(filePath), "1.jpg", "image/jpg");
            form.AddBinaryData("tempImg", FileContent(filePath), fileName, mimeType);
            form.AddField("param", typeName);
            WWW _www = new WWW(url, form);
            yield return _www;

            if (_www.error != null)
            {
                // Debug.LogError(_www.error);
                yield return _www.error;
            }
            else
            {
                // print(_www.text);
                Resources.UnloadUnusedAssets();
                System.GC.Collect();
                if (callBack != null)
                {
                    JsonData jobject = JsonMapper.ToObject(_www.text);
                    callBack(jobject);
                }
            }
        }


        #region 注册登录模块

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="invite"></param>
        public void Register(string phone, string code, string password, string name, string invite = "")
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/register";


            string data = "phone=" + phone +
                "&code=" + code + "&password=" + password + "&name=" + name + "&invite_code=" + invite;

            StartCoroutine(Post(url, data, OnRegister));
        }

        /// <summary>
        /// 注册结果
        /// </summary>
        /// <param name="result"></param>
        private void OnRegister(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.Regist, false, status);
                Util.ShowErrorMessage(status);
                // Debug.LogError("OnRegister >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            UserData.instance.id = int.Parse(result["id"].ToString());
            UserData.instance.name = result["name"].ToString();
            UserData.instance.phone = result["phone"].ToString();
            UserData.instance.role = int.Parse(result["role"].ToString());
            UserData.instance.area = int.Parse(result["area"].ToString());
            UserData.instance.avatar = result["avatar"].ToString();
            UserData.instance.coin = int.Parse(result["coin"].ToString());
            UserData.instance.point = int.Parse(result["point"].ToString());
            UserData.instance.token = result["token"].ToString();

            GetAreaList(()=> {
                EventManager.instance.NotifyEvent(Event.Regist, true);
            });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="type"></param>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <param name="code"></param>
        public void Login(int type, string phone, string password = "", string code = "")
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/login";
            string data = "type=" + type + "&phone=" + phone + "&password=" + password + "&code=" + code;
            StartCoroutine(Post(url, data, OnLogin));
        }

        private void OnLogin(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //Debug.LogError("OnLogin >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.Login, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            UserData.instance.id = int.Parse(result["id"].ToString());
            UserData.instance.name = result["name"].ToString();
            UserData.instance.phone = result["phone"].ToString();
            UserData.instance.role = int.Parse(result["role"].ToString());
            UserData.instance.area = int.Parse(result["area"].ToString());
            UserData.instance.avatar = result["avatar"].ToString();
            UserData.instance.coin = int.Parse(result["coin"].ToString());
            UserData.instance.point = int.Parse(result["point"].ToString());
            UserData.instance.token = result["token"].ToString();
            UserData.instance.level = int.Parse(result["level"].ToString());
            UserData.instance.totalSign = int.Parse(result["totalSign"].ToString());

            GetAreaList(()=> {
                EventManager.instance.NotifyEvent(Event.Login, true);
            });
        }

        /// <summary>
        /// 选择身份
        /// </summary>
        /// <param name="roleId">1为“经销商”，2为“水工”，3为“试压员”</param>
        public void SetRole(int roleId)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/setRole";
            string data = "role_id=" + roleId;
            DataManager.instance.selectRoleID = roleId;
            StartCoroutine(Post(url, data, OnSetRole));
        }

        /// <summary>
        ///选择身份回调
        /// </summary>
        /// <param name="result"></param>
        private void OnSetRole(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.SetRole, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnSetRole >>>>error status:" + status);
                return;
            }

            UserData.instance.role = DataManager.instance.selectRoleID;
            EventManager.instance.NotifyEvent(Event.SetRole, true, UserData.instance.role);
        }

        /// <summary>
        /// 选择区域
        /// </summary>
        /// <param name="areaid"></param>
        public void SetArea(int areaid)
        {
            DataManager.instance.selectAreaID = areaid;
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/setArea";
            string data = "area_id=" + areaid;
            StartCoroutine(Post(url, data, OnSetArea));
        }

        /// <summary>
        /// 选择区域回调
        /// </summary>
        /// <param name="result"></param>
        private void OnSetArea(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnSetArea >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.SetArea, false);
                return;
            }
            UserData.instance.area = DataManager.instance.selectAreaID;
            UtilManager.getInstance.areaID = DataManager.instance.selectAreaID;
            EventManager.instance.NotifyEvent(Event.SetArea, true, UserData.instance.area);
        }

        string GetMD5(string _str)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(Encoding.UTF8.GetBytes(_str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="type">类型，1-注册，2-登录 ，3改密码</param>
        public void getSmsCode(string phone, int type)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getSmsCode";
            string token = GetMD5(GetMD5(phone) + phone).Substring(3, 8).ToUpper();
            string data = "phone=" + phone + "&type=" + type + "&token=" + token;
            StartCoroutine(Post(url, data, OnGetSmsCode));
        }

        /// <summary>
        /// 获取短信验证码结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetSmsCode(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnGetSmsCode >>>>error status:" + status);
                // Util.ShowErrorMessage(status);
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", result["msg"].ToString());
                EventManager.instance.NotifyEvent(Event.GetSmsCode, false, status);
                return;
            }
            Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "请注意查收验证码");
            EventManager.instance.NotifyEvent(Event.GetSmsCode, true, status);
        }

        #endregion

        #region 预约模块
        /// <summary>
        /// 发起预约
        /// </summary>
        /// <param name="areaId">区域ID</param>
        /// <param name="type">预约类型</param>
        /// <param name="community">小区</param>
        /// <param name="address">地址</param>
        /// <param name="name">业主姓名</param>
        /// <param name="phone">业主电话</param>
        /// <param name="time">预约上门时间</param>
        /// <param name="remark">备注</param>
        public void CreateAppoiment(int areaId, int type, string community, string address, string name, string phone, string date, int time_frame, string remark)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Appointment/create";
            string data = " area_id=" + areaId + "&type=" + type + "&community=" + community + "&address=" + address + "&name=" + name + "&phone=" + phone + "&date=" + date + "&time_frame=" + time_frame + "&remark=" + remark;
            StartCoroutine(Post(url, data, OnCreateAppointment));
        }

        /// <summary>
        /// 预约结果回調
        /// </summary>
        /// <param name="result"></param>
        public void OnCreateAppointment(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.CreateAppointment, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnCreateAppointment >>>>error status:" + status);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CreateAppointment, true);
        }

        /// <summary>
        /// 获取我的订单列表
        /// </summary>
        /// <param name="type">1为我预约的订单，2为我安装的订单 3为我试压的订单 </param>
        /// <param name="status">0为全部，3为合格订单，4为不合格</param>
        /// <param name="page">分页，页数</param>
        /// <param name="pageCount">分页，每页条数，最大50</param>   
        /// <param name="searchOrderId">搜索条件，订单号，最小4位，最大15位</param> --已删除
        /// <param name="searchAddress">地址模糊搜索</param>
        public void GetAppointmentOrderList(int type, int status, int page, int pageCount, string searchAddress)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Appointment/getOrderList";
            string lastTime = "";
            string data = "";
            //只有在首页的时候才会刷新上次请求的时间戳
            if (page == 1)
            {
                DataManager.instance.orderList_lastTime = Util.GetTimeStamp();
                data = "order_type=" + type + "&order_status=" + status + "& page=" + page + "& page_count=" + pageCount + "&search[communityOrAddress]=" + searchAddress;
            }
            else
            {
                lastTime = DataManager.instance.orderList_lastTime.ToString();
                data = "order_type=" + type + "&order_status=" + status + "& page=" + page + "& page_count=" + pageCount + "&last_time=" + lastTime + "&search[communityOrAddress]=" + searchAddress;
            }
            StartCoroutine(Post(url, data, OnGetAppointmentOrderList));
        }

        /// <summary>
        /// 获取我的订单回调
        /// </summary>
        /// <param name="result"></param>
        private void OnGetAppointmentOrderList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetAppointmentList, false);
                Util.ShowErrorMessage(status);
                // Debug.LogError("OnGetAppointmentOrderList >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());
            JsonData data = result["list"];
            List<Order> resultList = new List<Order>();
            for (int i = 0; i < data.Count; i++)
            {
                Order order = new Order();
                order.id = data[i]["id"].ToString();
                order.orderId = data[i]["order_id"].ToString();
                order.areaId = data[i]["area_id"].ToString();
                order.userId = data[i]["user_id"].ToString();
                //order.userName = data[i]["user_name"].ToString();
                //order.userPhone = data[i]["user_phone"].ToString();
                order.userName = "";
                order.userPhone = "";
                order.type = data[i]["a_type"].ToString();
                order.community = data[i]["a_community"].ToString();
                order.address = data[i]["a_address"].ToString();
                order.name = data[i]["a_name"].ToString();
                order.phone = data[i]["a_phone"].ToString();
                order.time = data[i]["a_time"].ToString();
                order.a_date = data[i]["a_date"].ToString();
                order.a_time_frame = data[i]["a_time_frame"].ToString();
                order.remark = data[i]["a_remark"].ToString();
                order.adminId = data[i]["b_admin_id"].ToString();
                order.adminName = data[i]["b_admin_name"].ToString();
                // order.testerId = data[i]["b_tester_id"].ToString();
                order.adminCreateTime = data[i]["b_create_time"].ToString();
                //Debug.Log(data[i]["c_pic_doorplate"].Count);
                order.menpaiList.Clear();
                order.stateList.Clear();
                order.chufangList.Clear();
                order.weishengjianList.Clear();
                order.ketingList.Clear();
                order.guodaoList.Clear();
                order.yangtaiList.Clear();
                order.otherList.Clear();
                order.pingzhengList.Clear();
                JsonData menpaiArray = data[i]["c_pic_doorplate"];

                for (int a = 0; a < menpaiArray.Count; a++)
                {
                    order.menpaiList.Add(menpaiArray[a].ToString());
                }
                JsonData stateArray = data[i]["c_pic_instrument"];
                for (int a = 0; a < stateArray.Count; a++)
                {
                    order.stateList.Add(stateArray[a].ToString());
                }
                JsonData chufangArray = data[i]["c_pic_kitchen"];
                for (int a = 0; a < chufangArray.Count; a++)
                {
                    order.chufangList.Add(chufangArray[a].ToString());
                }
                JsonData weishengjianArray = data[i]["c_pic_toilet"];
                for (int a = 0; a < weishengjianArray.Count; a++)
                {
                    order.weishengjianList.Add(weishengjianArray[a].ToString());
                }
                JsonData ketingArray = data[i]["c_pic_room"];
                for (int a = 0; a < ketingArray.Count; a++)
                {
                    order.ketingList.Add(ketingArray[a].ToString());
                }
                JsonData guodaoArray = data[i]["c_pic_corridor"];
                for (int a = 0; a < guodaoArray.Count; a++)
                {
                    order.guodaoList.Add(guodaoArray[a].ToString());
                }
                JsonData yangtaiArray = data[i]["c_pic_balcony"];
                for (int a = 0; a < yangtaiArray.Count; a++)
                {
                    order.yangtaiList.Add(yangtaiArray[a].ToString());
                }
                JsonData otherArray = data[i]["c_pic_other"];
                for (int a = 0; a < otherArray.Count; a++)
                {
                    order.otherList.Add(otherArray[a].ToString());
                }
                JsonData pingzhengArray = data[i]["c_pic_evidence"];
                for (int a = 0; a < pingzhengArray.Count; a++)
                {
                    order.pingzhengList.Add(pingzhengArray[a].ToString());
                }
                order.buildId = data[i]["c_build_id"].ToString();
                order.buildName = data[i]["c_build_name"].ToString();
                order.buildPhone = data[i]["c_build_phone"].ToString();
                order.testBuyPlace = data[i]["c_test_buy_place"].ToString();
                order.testHouseType = data[i]["c_test_house_type"].ToString();
                order.testKitchenType = data[i]["c_test_kitchen_type"].ToString();
                order.testToiletType = data[i]["c_test_toilet_type"].ToString();
                order.testDeveloper = data[i]["c_test_developer"].ToString();
                order.testDecoration = data[i]["c_test_decoration"].ToString();
                order.testHvac = data[i]["c_test_hvac"].ToString();
                order.testAir = data[i]["c_test_air"].ToString();
                order.testProductType = data[i]["c_test_product_type"].ToString();
                order.testLength = data[i]["c_test_length"].ToString();
                order.testLayingType = data[i]["c_test_laying_type"].ToString();
                order.testPipeline = data[i]["c_test_pipeline"].ToString();
                order.testRemark = data[i]["c_test_remark"].ToString();
                order.testAssess = data[i]["c_test_assess"].ToString();
                order.testCompress = data[i]["c_test_compress"].ToString();
                order.testWeld = data[i]["c_test_weld"].ToString();
                order.testWeldCheck = data[i]["c_test_weld_check"].ToString();
                order.testKeepStart = data[i]["c_test_keep_start"].ToString();
                order.testKeepEnd = data[i]["c_test_keep_end"].ToString();
                order.testOperatePressure = data[i]["c_test_operate_pressure"].ToString();
                order.testCheckPressure = data[i]["c_test_check_pressure"].ToString();
                order.testUserId = data[i]["c_test_user_id"].ToString();
                order.testUserName = data[i]["c_test_user_name"].ToString();
                order.testUserPhone = data[i]["c_test_user_phone"].ToString();
                order.testNotice = data[i]["c_test_notice"].ToString();
                order.testCreateTime = data[i]["c_create_time"].ToString();
                order.testSimpleTime = data[i]["c_test_simple_date"].ToString();
                order.orderStatus = data[i]["order_status"].ToString();
                order.createTime = data[i]["create_time"].ToString();

                resultList.Add(order);
            }

            EventManager.instance.NotifyEvent(Event.GetAppointmentList, true, resultList, total, page, pageCount);
        }

        /// <summary>
        /// 获取预约订单详情
        /// </summary>
        /// <param name="id">订单Id，不是订单号</param>
        public void GetAppointmentDetail(int id)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Appointment/getDetail";
            string data = " order_id=" + id;
            StartCoroutine(Post(url, data, OnGetAppointmentDetail));
        }

        /// <summary>
        /// 获取预约订单详情回调
        /// </summary>
        /// <param name="result"></param>
        public void OnGetAppointmentDetail(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetAppointmentGetList, false);
                Util.ShowErrorMessage(status);
                // Debug.LogError("OnGetAppointmentDetail >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            Order order = new Order();
            order.id = result["id"].ToString();
            order.orderId = result["order_id"].ToString();
            order.areaId = result["area_id"].ToString();
            order.userId = result["user_id"].ToString();
            order.userName = result["user_name"].ToString();
            order.userPhone = result["user_phone"].ToString();
            order.type = result["a_type"].ToString();
            order.community = result["a_community"].ToString();
            order.address = result["a_address"].ToString();
            order.name = result["a_name"].ToString();
            order.phone = result["a_phone"].ToString();
            order.time = result["a_time"].ToString();
            order.a_date = result["a_date"].ToString();
            order.a_time_frame = result["a_time_frame"].ToString();
            order.remark = result["a_remark"].ToString();
            order.adminId = result["b_admin_id"].ToString();
            //  order.testerId = result["b_tester_id"].ToString();
            order.adminCreateTime = result["b_create_time"].ToString();
            JsonData menpaiArray = result["c_pic_doorplate"];
            order.menpaiList.Clear();
            order.stateList.Clear();
            order.chufangList.Clear();
            order.weishengjianList.Clear();
            order.ketingList.Clear();
            order.guodaoList.Clear();
            order.yangtaiList.Clear();
            order.otherList.Clear();
            order.pingzhengList.Clear();
            order.buhegeList.Clear();
            
            for (int a = 0; a < menpaiArray.Count; a++)
            {
                order.menpaiList.Add(menpaiArray[a].ToString());
            }
            JsonData stateArray = result["c_pic_instrument"];
            for (int a = 0; a < stateArray.Count; a++)
            {
                order.stateList.Add(stateArray[a].ToString());
            }
            JsonData chufangArray = result["c_pic_kitchen"];
            for (int a = 0; a < chufangArray.Count; a++)
            {
                order.chufangList.Add(chufangArray[a].ToString());
            }
            JsonData weishengjianArray = result["c_pic_toilet"];
            for (int a = 0; a < weishengjianArray.Count; a++)
            {
                order.weishengjianList.Add(weishengjianArray[a].ToString());
            }
            JsonData ketingArray = result["c_pic_room"];
            for (int a = 0; a < ketingArray.Count; a++)
            {
                order.ketingList.Add(ketingArray[a].ToString());
            }
            JsonData guodaoArray = result["c_pic_corridor"];
            for (int a = 0; a < guodaoArray.Count; a++)
            {
                order.guodaoList.Add(guodaoArray[a].ToString());
            }
            JsonData yangtaiArray = result["c_pic_balcony"];
            for (int a = 0; a < yangtaiArray.Count; a++)
            {
                order.yangtaiList.Add(yangtaiArray[a].ToString());
            }
            JsonData otherArray = result["c_pic_other"];
            for (int a = 0; a < otherArray.Count; a++)
            {
                order.otherList.Add(otherArray[a].ToString());
            }
            JsonData pingzhengArray = result["c_pic_evidence"];
            for (int a = 0; a < pingzhengArray.Count; a++)
            {
                order.pingzhengList.Add(pingzhengArray[a].ToString());
            }
            order.buildId = result["c_build_id"].ToString();
            order.buildName = result["c_build_name"].ToString();
            order.buildPhone = result["c_build_phone"].ToString();
            order.testBuyPlace = result["c_test_buy_place"].ToString();
            order.testHouseType = result["c_test_house_type"].ToString();
            order.testKitchenType = result["c_test_kitchen_type"].ToString();
            order.testToiletType = result["c_test_toilet_type"].ToString();
            order.testDeveloper = result["c_test_developer"].ToString();
            order.testDecoration = result["c_test_decoration"].ToString();
            order.testHvac = result["c_test_hvac"].ToString();
            order.testAir = result["c_test_air"].ToString();
            order.testProductType = result["c_test_product_type"].ToString();
            order.testLength = result["c_test_length"].ToString();
            order.testLayingType = result["c_test_laying_type"].ToString();
            order.testPipeline = result["c_test_pipeline"].ToString();
            order.testRemark = result["c_test_remark"].ToString();
            order.testAssess = result["c_test_assess"].ToString();
            order.testCompress = result["c_test_compress"].ToString();
            order.testWeld = result["c_test_weld"].ToString();
            order.testWeldCheck = result["c_test_weld_check"].ToString();
            order.testKeepStart = result["c_test_keep_start"].ToString();
            order.testKeepEnd = result["c_test_keep_end"].ToString();
            order.testOperatePressure = result["c_test_operate_pressure"].ToString();
            order.testCheckPressure = result["c_test_check_pressure"].ToString();
            order.testUserId = result["c_test_user_id"].ToString();
            order.testUserName = result["c_test_user_name"].ToString();
            order.testUserPhone = result["c_test_user_phone"].ToString();
            order.testNotice = result["c_test_notice"].ToString();
            order.testCreateTime = result["c_create_time"].ToString();
            //  order.testSimpleTime = result["c_test_simple_date"].ToString();
            order.orderStatus = result["order_status"].ToString();
            order.createTime = result["create_time"].ToString();
            order.cardSwitch = result["card_switch"].ToString();
            order.cardType = result["card_type"].ToString();
            JsonData buhegeArray = result["failed_pic"];
            for (int a = 0; a < buhegeArray.Count; a++)
            {
                order.buhegeList.Add(buhegeArray[a].ToString());
            }
            order.buhegeLabel = result["failed_remark"].ToString();
            EventManager.instance.NotifyEvent(Event.GetAppointmentGetList, true, order);
        }


        #endregion

        #region 我的模块
        /// <summary>
        /// 获取系统开发区域列表
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="pageCount">每页的条目数</param>
        public void GetAreaList(int curPage, int pageCount)
        {
            return;
            string url = "http://" + UtilManager.getInstance.serverIP + "/Area/getList";

            string lastTime = "";
            string data = "";
            //只有在首页的时候才会刷新上次请求的时间戳
            if (curPage == 1)
            {
                DataManager.instance.areaList_lastTime = Util.GetTimeStamp();
                data = "page=" + curPage + "&page_count=" + pageCount;
            }
            else
            {
                lastTime = DataManager.instance.areaList_lastTime.ToString();
                data = "page=" + curPage + "&page_count=" + pageCount + "&last_time=" + lastTime;
            }


            StartCoroutine(Post(url, data, OnGetAreaList));
        }

        /// <summary>
        /// 获取系统开放区域列表结果
        /// </summary>
        private void OnGetAreaList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetAreaList, false);
                Util.ShowErrorMessage(status);
                // Debug.LogError("OnGetAreaList >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            JsonData data = result["list"];
            List<Area> dataList = new List<Area>();
            for (int i = 0; i < data.Count; i++)
            {
                Area area = new Area();
                area.id = int.Parse(data[i]["id"].ToString());
                area.title = data[i]["title"].ToString();
                if (area.id == UserData.instance.area)
                {
                    // Debug.Log(area.id);
                    LM_Workspace.UtilManager.getInstance.areaName = area.title;
                    LM_Workspace.UtilManager.getInstance.areaID = area.id;
                    Debug.Log(area.id);
                    GetAreaDetail(area.id);
                }
                area.createTime = data[i]["create_time"].ToString();
                dataList.Add(area);
            }
            EventManager.instance.NotifyEvent(Event.GetAreaList, true, dataList);

        }

        public void GetAreaList(System.Action call=null) {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Area/getList";         

            StartCoroutine(Get(url,"",(JsonData data)=> {
                UtilManager.InitRoleAreaData(data);
                if(UtilManager.getInstance.areaID!=0)
                    GetAreaDetail(UtilManager.getInstance.areaID);
                call?.Invoke();
            }));
        }

        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="curPage">当前页数</param>
        /// <param name="pageCount">每页的条目数</param>
        public void GetCollectionList(int curPage, int pageCount)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/getCollectList";
            string lastTime = "";
            string data = "";
            //只有在首页的时候才会刷新上次请求的时间戳
            if (curPage == 1)
            {
                DataManager.instance.collectionList_lastTime = Util.GetTimeStamp();
                data = "page=" + curPage + "&page_count=" + pageCount;
            }
            else
            {
                lastTime = DataManager.instance.collectionList_lastTime.ToString();
                data = "page=" + curPage + "&page_count=" + pageCount + "&last_time=" + lastTime;
            }
            StartCoroutine(Post(url, data, OnGetCollectionList));
        }

        /// <summary>
        /// 返回获取收藏列表
        /// </summary>
        /// <param name="result"></param>
        public void OnGetCollectionList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetCollectionList, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnGetCollectionList >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            JsonData data = result["list"];
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());

            List<Forum> nodeList = new List<Forum>();
            for (int i = 0; i < data.Count; i++)
            {
                Forum node = new Forum();
                node.id = int.Parse(data[i]["id"].ToString());
                node.title = data[i]["title"].ToString();
                node.catId = int.Parse(data[i]["cat_id"].ToString());
                node.userId = int.Parse(data[i]["user_id"].ToString());
                node.userName = data[i]["user_name"].ToString();
                node.userAvatar = data[i]["user_avatar"].ToString();
                node.view = int.Parse(data[i]["view"].ToString());
                node.comment = int.Parse(data[i]["comment"].ToString());
                node.create_time = data[i]["create_time"].ToString();
                nodeList.Add(node);
            }
            EventManager.instance.NotifyEvent(Event.GetCollectionList, true, nodeList, total, page, pageCount);

        }

        /// <summary>
        /// 【获取我的积分】针对经销商和水工
        /// </summary>
        public void GetUserPoint()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getPoint";
            StartCoroutine(Post(url, OnGetUserPoint));
        }

        /// <summary>
        /// 积分返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetUserPoint(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetUserPoint, false);
                Util.ShowErrorMessage(status);
                //Debug.LogError("OnGetUserPoint >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            UserData.instance.point = int.Parse(result.ToString());
            EventManager.instance.NotifyEvent(Event.GetUserPoint, true, UserData.instance.point);
        }

        /// <summary>
        /// 【获取我的邀请码】针对经销商和水工
        /// </summary>
        public void GetInviteCode()
        {
            //            if (UserData.instance.role == 1 || UserData.instance.role == 2)
            //                return;

            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getInviteCode";
            StartCoroutine(Post(url, OnGetInviteCode));
        }

        /// <summary>
        /// 邀请码返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetInviteCode(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetInviteCode, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnGetInviteCode >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            string code = result.ToString();
            EventManager.instance.NotifyEvent(Event.GetInviteCode, true, code);
        }

        /// <summary>
        /// 【获取我的等级】针对经销商和水工
        /// </summary>
        public void GetLevel()
        {
            //if (UserData.instance.role == 1 || UserData.instance.role == 2)
            //    return;

            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getLevel";
            StartCoroutine(Post(url, OnGetLevel));
        }

        /// <summary>
        /// 返回等级
        /// </summary>
        /// <param name="result"></param>
        private void OnGetLevel(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetLevel, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnGetLevel >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            UserData.instance.level = int.Parse(result["level"].ToString());
            int orderNum = int.Parse(result["order_num"].ToString());
            EventManager.instance.NotifyEvent(Event.GetLevel, true, UserData.instance.level, orderNum);
        }

        /// <summary>
        /// 获取我的蓝圈列表】 我的回帖，将显示“我回复的帖子”所属主贴，系统自动去重
        /// </summary>
        /// <param name="type"></param>
        /// <param name="curPage"></param>
        /// <param name="pageCount"></param>
        public void GetCircleList(int type, int curPage, int pageCount)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/getCircleList";
            string lastTime = "";
            string data = "";
            //只有在首页的时候才会刷新上次请求的时间戳
            if (curPage == 1)
            {
                DataManager.instance.circleList_lastTime = Util.GetTimeStamp();
                data = "type=" + type + "&page=" + curPage + "&page_count=" + pageCount;
            }
            else
            {
                lastTime = DataManager.instance.circleList_lastTime.ToString();
                data = "type=" + type + "&page=" + curPage + "&page_count=" + pageCount + "&last_time=" + lastTime;
            }
            StartCoroutine(Post(url, data, OnGetCircleList));
        }

        /// <summary>
        /// 获取篮圈列表回调
        /// </summary>
        /// <param name="result"></param>
        private void OnGetCircleList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetCircleList, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnGetCircleList >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int page_count = int.Parse(result["page_count"].ToString());

            JsonData data = result["list"];
            List<Forum> nodeList = new List<Forum>();
            for (int i = 0; i < data.Count; i++)
            {
                Forum node = new Forum();
                node.id = int.Parse(data[i]["id"].ToString());
                node.title = data[i]["title"].ToString();
                node.content = data[i]["content"].ToString();
                node.catId = int.Parse(data[i]["cat_id"].ToString());
                node.userId = int.Parse(data[i]["user_id"].ToString());
                node.userName = data[i]["user_name"].ToString();
                node.userAvatar = data[i]["user_avatar"].ToString();
                node.uploadImages = JsonMapper.ToObject<List<string>>(data[i]["upload_images"].ToString());

                node.view = int.Parse(data[i]["view"].ToString());
                node.comment = int.Parse(data[i]["comment"].ToString());
                node.create_time = data[i]["create_time"].ToString();
                nodeList.Add(node);
            }
            EventManager.instance.NotifyEvent(Event.GetCircleList, true, nodeList, total, page, page_count);

        }

        /// <summary>
        /// 【近7日签到情况】针对经销商和水工
        /// </summary>
        public void GetLastWeekSign()
        {
            if (UserData.instance.role == 3)
                return;

            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getLastWeekSign";
            StartCoroutine(Post(url, OnGetLastWeekSign));
        }

        /// <summary>
        /// 返回签到情况
        /// </summary>
        /// <param name="result"></param>
        private void OnGetLastWeekSign(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.GetLastWeekSign, false);
                Util.ShowErrorMessage(status);
                //  Debug.LogError("OnGetLastWeekSign >>>>error status:" + status);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            UserData.instance.lastWeekSign = new List<int>();
            for (int i = result.Count - 1; i >= 0; i--)
            {
                UserData.instance.lastWeekSign.Add(int.Parse(result[i].ToString()));
            }
            EventManager.instance.NotifyEvent(Event.GetLastWeekSign, true, UserData.instance.lastWeekSign);
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="url">新的头像地址</param>
        public void SetAvatar(string avatarUrl)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/setAvatar";
            string data = "avatar_url=" + avatarUrl;
            StartCoroutine(Post(url, data, OnSetAvatar));
        }

        /// <summary>
        /// 设置头像结果
        /// </summary>
        /// <param name="result"></param>
        public void OnSetAvatar(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                EventManager.instance.NotifyEvent(Event.SetAvatar, false);
                GD.UIManager.instance.ShowLoggerMsg(result["msg"].ToString());
                //Util.ShowErrorMessage(status);
                // Debug.LogError("OnSetAvatar >>>>error status:" + status);
                return;
            }
            EventManager.instance.NotifyEvent(Event.SetAvatar, true);

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="code">短信验证码</param>
        /// <param name="password">密码</param>
        public void ModifyPassword(string phone, string code, string password)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/modifyPwd";
            string data = "phone=" + phone + "&sms_code=" + code + "&new_pwd=" + password;
            StartCoroutine(Post(url, data, OnModifyPassword));
        }

        /// <summary>
        /// 修改密码返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnModifyPassword(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnModifyPassword >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.ModifyPassword, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.ModifyPassword, true);
        }

        /// <summary>
        /// 【获取我的基本信息】 用户已登录用户再次打开APP
        /// </summary>
        public void GetBaseInfo()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/getBaseInfo";
            StartCoroutine(Post(url, OnGetBaseInfo));
        }

        /// <summary>
        /// 获取用户基本信息结果
        /// </summary>
        /// <param name="result"></param>
        public void OnGetBaseInfo(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetBaseInfo >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetBaseInfo, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            UserData.instance.id = int.Parse(result["id"].ToString());
            UserData.instance.name = result["name"].ToString();
            UserData.instance.phone = result["phone"].ToString();
            UserData.instance.role = int.Parse(result["role"].ToString());
            UserData.instance.area = int.Parse(result["area"].ToString());
            UserData.instance.avatar = result["avatar"].ToString();
            UserData.instance.coin = int.Parse(result["coin"].ToString());
            UserData.instance.point = int.Parse(result["point"].ToString());
            UserData.instance.token = result["token"].ToString();
            UserData.instance.level = int.Parse(result["level"].ToString());
            UserData.instance.totalSign = int.Parse(result["totalSign"].ToString());

            GetAreaList(() => {
                EventManager.instance.NotifyEvent(Event.GetBaseInfo, true);
            });
        }
        #endregion

        #region 论坛模块
        /// <summary>
        /// 获取纯净小蓝官方发布列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageCount"></param>
        public void GetOfficialList(int curPage, int pageCount)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Official/getList";
            string lastTime = "";
            string data = "";
            if (curPage == 1)
            {
                DataManager.instance.officialList_lastTime = Util.GetTimeStamp();
                data = "page=" + curPage + "&page_count=" + pageCount;
            }
            else
            {
                lastTime = DataManager.instance.officialList_lastTime.ToString();
                data = "page=" + curPage + "&page_count=" + pageCount + "&last_time=" + lastTime;
            }
            StartCoroutine(Post(url, data, OnGetOfficialList));
        }

        /// <summary>
        /// 获取官方发布列表结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetOfficialList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetOfficialList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetOfficialList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());
            JsonData data = result["list"];
            List<OfficalForum> officialForumList = new List<OfficalForum>();
            for (int i = 0; i < data.Count; i++)
            {
                OfficalForum tmp = new OfficalForum();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.title = data[i]["title"].ToString();
                tmp.content = data[i]["content"].ToString();
                tmp.thumb = data[i]["thumb"].ToString();
                tmp.isTop = int.Parse(data[i]["is_top"].ToString()) == 1;
                tmp.comment = int.Parse(data[i]["comment"].ToString());
                tmp.view = int.Parse(data[i]["view"].ToString());
                officialForumList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetOfficialList, true, officialForumList, total, page, pageCount);
        }

        /// <summary>
        /// 发帖
        /// </summary>
        public void CreateForum(string catid, string title, string content, string[] imgArray)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/create";
            string data = "cat_id=" + catid + "&title=" + title + "&content=" + content;
            for (int i = 0; i < imgArray.Length; i++)
            {
                data += "&upload_images[]=" + imgArray[i];
            }
            StartCoroutine(Post(url, data, OnCreateForum));
        }

        /// <summary>
        /// 发帖回调
        /// </summary>
        /// <param name="result"></param>
        private void OnCreateForum(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //   Debug.LogError("OnCreateForum >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CreateForum, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CreateForum, true);
        }

        /// <summary>
        /// 回帖
        /// </summary>
        /// <param name="forumId">主贴ID</param>
        /// <param name="content">回复内容,最多140字</param>
        public void ReplyForum(int forumId, string content)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/reply";
            string data = "forum_id=" + forumId + "&content=" + content;
            StartCoroutine(Post(url, data, OnReplyForum));
        }

        /// <summary>
        /// 回帖结果
        /// </summary>
        /// <param name="result"></param>
        private void OnReplyForum(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnReplyForum >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.ReplyForum, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.ReplyForum, true);
        }
        /// <summary>
        /// 回帖
        /// </summary>
        /// <param name="forumId">主贴ID</param>
        /// <param name="content">回复内容,最多140字</param>
        public void ReplyOfficialForum(int forumId, string content)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Official/reply";
            string data = "official_id=" + forumId + "&content=" + content;
            StartCoroutine(Post(url, data, OnReplyOfficialForum));
        }

        /// <summary>
        /// 回帖结果
        /// </summary>
        /// <param name="result"></param>
        private void OnReplyOfficialForum(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnReplyForum >>>>error status:" + status);
                // Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.ReplyOfficialForum, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.ReplyOfficialForum, true);
        }
        /// <summary>
        /// 收藏帖子
        /// </summary>
        /// <param name="forumId"></param>
        public void CollectForum(int forumId)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/collect";
            string data = "user_id=" + UserData.instance.id + "&forum_id=" + forumId;
            StartCoroutine(Post(url, data, OnCollectForum));
        }

        /// <summary>
        /// 收藏帖子回调
        /// </summary>
        /// <param name="result"></param>
        public void OnCollectForum(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1 && status != 3)
            {
                //  Debug.LogError("OnCollectForum >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CollectForum, false);
                return;
            }

            EventManager.instance.NotifyEvent(Event.CollectForum, true, status);
        }

        /// <summary>
        /// 获取官方帖子详情
        /// </summary>
        /// <param name="id"></param>
        public void GetOfficialDetail(int id)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Official/getDetail";
            string data = "official_id=" + id;
#if UNITY_EDITOR
            Debug.Log(data);
#endif
            StartCoroutine(Post(url, data, OnGetOfficialDetail));
        }

        /// <summary>
        /// 获取官方帖子详情结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetOfficialDetail(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetOfficialDetail >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetOfficialForumDetail, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            JsonData mainOfficial = result["mainOfficial"];
            OfficalForum officalForum = new OfficalForum();
            officalForum.id = int.Parse(mainOfficial["id"].ToString());
            officalForum.title = mainOfficial["title"].ToString();
            officalForum.content = mainOfficial["content"].ToString();
            officalForum.thumb = mainOfficial["thumb"].ToString();
            officalForum.isTop = int.Parse(mainOfficial["is_top"].ToString()) == 1;
            officalForum.view = int.Parse(mainOfficial["view"].ToString());
            officalForum.comment = int.Parse(mainOfficial["comment"].ToString());
            officalForum.createTime = mainOfficial["create_time"].ToString();
            JsonData subForum = result["subOfficial"];
            for (var i = 0; i < subForum.Count; i++)
            {
                Comment tmp = new Comment();
                tmp.id = int.Parse(subForum[i]["id"].ToString());
                tmp.content = subForum[i]["content"].ToString();
                tmp.userId = int.Parse(subForum[i]["user_id"].ToString());
                tmp.userName = subForum[i]["user_name"].ToString();
                tmp.userAvatar = subForum[i]["user_avatar"].ToString();
                tmp.createTime = subForum[i]["create_time"].ToString();
                officalForum.commentList.Add(tmp);
            }

            EventManager.instance.NotifyEvent(Event.GetOfficialForumDetail, true, officalForum);
        }

        /// <summary>
        /// 获取banner列表
        /// </summary>
        /// <param name="curPage"></param>
        /// <param name="pageCount"></param>
        public void GetBannerList(int curPage, int pageCount)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Banner/getList";
            string lastTime = "";
            string data = "";
            if (curPage == 1)
            {
                DataManager.instance.bannerList_lastTime = Util.GetTimeStamp();
                data = "page=" + curPage + "&page_count=" + pageCount;
            }
            else
            {
                lastTime = DataManager.instance.bannerList_lastTime.ToString();
                data = "page=" + curPage + "&page_count=" + pageCount + "&last_time=" + lastTime;
            }
            StartCoroutine(Post(url, data, OnGetBannerList));
        }

        /// <summary>
        /// 获取banner回调
        /// </summary>
        /// <param name="result"></param>
        private void OnGetBannerList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnGetBannerList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetBannerList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());
            JsonData data = result["list"];
            List<Banner> bannerList = new List<Banner>();
            for (int i = 0; i < data.Count; i++)
            {
                Banner tmp = new Banner();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.title = data[i]["title"].ToString();
                tmp.thumb = data[i]["thumb"].ToString();
                tmp.createTime = data[i]["create_time"].ToString();
                bannerList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetBannerList, true, bannerList, total, page, pageCount);
        }

        /// <summary>
        /// 获取帖子详情及回帖，回帖按时间倒序
        /// </summary>
        public void GetDetail(int forunid)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/getDetail";
            string data = "forum_id=" + forunid;
            StartCoroutine(Post(url, data, OnGetDetail));
        }
        /// <summary>
        /// 获取帖子详情结果返回
        /// </summary>
        /// <param name="result"></param>
        private void OnGetDetail(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnGetDetail >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetDetail, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            Forum forum = new Forum();
            JsonData mainForum = result["mainForum"];
            forum.id = int.Parse(mainForum["id"].ToString());
            forum.catId = int.Parse(mainForum["cat_id"].ToString());
            forum.userId = int.Parse(mainForum["user_id"].ToString());
            forum.view = int.Parse(mainForum["view"].ToString());
            forum.comment = int.Parse(mainForum["comment"].ToString());
            forum.title = mainForum["title"].ToString();
            forum.content = mainForum["content"].ToString();
            forum.userAvatar = mainForum["user_avatar"].ToString();
            forum.uploadImages = JsonMapper.ToObject<List<string>>(mainForum["upload_images"].ToString());
            forum.create_time = mainForum["create_time"].ToString();
            forum.userName = mainForum["user_name"].ToString();

            forum.isCollect = mainForum["is_collect"].ToString() == "1";
            JsonData subForum = result["subForum"];
            for (var i = 0; i < subForum.Count; i++)
            {
                Comment tmp = new Comment();
                tmp.id = int.Parse(subForum[i]["id"].ToString());
                tmp.content = subForum[i]["content"].ToString();
                tmp.userId = int.Parse(subForum[i]["user_id"].ToString());
                tmp.userName = subForum[i]["user_name"].ToString();
                tmp.userAvatar = subForum[i]["user_avatar"].ToString();
                tmp.createTime = subForum[i]["create_time"].ToString();
                forum.commentList.Add(tmp);
            }

            EventManager.instance.NotifyEvent(Event.GetDetail, true, forum);
        }

        /// <summary>
        /// 【获取论坛帖子（主贴）列表】含搜索
        /// </summary>
        /// <param name="cat_id">获取的指定版块id，0为全部，1为动态分享，2为工艺展示，3为寻找工友，4为问题咨询</param>
        /// <param name="page">页数</param>
        /// <param name="pageCount">每页条数</param>
        /// <param name="title">搜索标题数组</param>
        public void GetForumList(int catId, int page, int pageCount, string[] title)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/getForumList";
            string data = "cat_id=" + catId + "&page=" + page + "&page_count=" + pageCount;
            for (var i = 0; i < title.Length; i++)
            {
                data += "&search[title]=" + title[i];
            }
            if (page == 1)
                DataManager.instance.forumList_lastTime = Util.GetTimeStamp();

            else
                data += "&last_time=" + DataManager.instance.forumList_lastTime.ToString();

            StartCoroutine(Post(url, data, OnGetForumList));
        }

        /// <summary>
        /// 获取论坛帖子结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetForumList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnGetForumList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetForumList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());

            JsonData data = result["list"];
            List<Forum> forumList = new List<Forum>();
            for (var i = 0; i < data.Count; i++)
            {
                Forum tmp = new Forum();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.userName = data[i]["user_name"].ToString();
                tmp.userAvatar = data[i]["user_avatar"].ToString();
                tmp.create_time = data[i]["create_time"].ToString();
                tmp.title = data[i]["title"].ToString();
                tmp.content = data[i]["content"].ToString();
                tmp.view = int.Parse(data[i]["view"].ToString());
                tmp.comment = int.Parse(data[i]["comment"].ToString());
                tmp.catId = int.Parse(data[i]["cat_id"].ToString());
                forumList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetForumList, true, forumList, total, page, pageCount);
        }
        #endregion

        #region 抽奖模块
        /// <summary>
        /// 【参与抽奖】奖金立即到帐，需PP可将中奖结果的id与界面中奖项匹配
        /// </summary>
        public void PlayWithLottery()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Lottery/playWithLottery";
            StartCoroutine(Post(url, OnPlayWithLottery));
        }

        /// <summary>
        /// 抽奖结果返回
        /// </summary>
        /// <param name="result"></param>
        private void OnPlayWithLottery(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnPlayWithLottery >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.PlayWithLottery, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            Award award = new Award();
            award.id = int.Parse(result["id"].ToString());
            award.title = result["title"].ToString();
            award.type = int.Parse(result["type"].ToString());
            award.price = int.Parse(result["price"].ToString());
            EventManager.instance.NotifyEvent(Event.PlayWithLottery, true, award);
        }

        /// <summary>
        /// 获取奖项列表
        /// </summary>
        public void GetAwardList()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/LotteryCategory/getList";
            StartCoroutine(Post(url, OnGetAwardList));
        }

        /// <summary>
        /// 获取奖项列表结果
        /// </summary>
        private void OnGetAwardList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //   Debug.LogError("OnGetAwardList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetAwardList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
#if UNITY_EDITOR
            Debug.Log(result.ToString());
#endif
            DataManager.instance.systemAwardList = new List<Award>();
            for (var i = 0; i < result.Count; i++)
            {
                Award tmp = new Award();
                tmp.id = int.Parse(result[i]["id"].ToString());
                tmp.title = result[i]["title"].ToString();
                DataManager.instance.systemAwardList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetAwardList, true, DataManager.instance.systemAwardList);
        }
        #endregion

        #region 工长排行模块
        /// <summary>
        /// 获取工长排行列表
        /// </summary>
        /// <param name="searchName">搜索名称</param>
        public void GetForemanList(string[] searchName)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Foreman/getList";
            if (searchName != null)
                for (var i = 0; i < searchName.Length; i++)
                {
                    string data = "";
                    data += "search[name]=" + searchName[i];
                    StartCoroutine(Post(url, data, OnGetForemanList));
                }
            else
                StartCoroutine(Post(url, OnGetForemanList));

        }
        /// <summary>
        /// 获取工长列表排行結果
        /// </summary>
        private void OnGetForemanList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetForemanList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetForemanList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            int total = int.Parse(result["total"].ToString());
            JsonData data = result["list"];
            List<Foreman> foremanList = new List<Foreman>();
            for (var i = 0; i < data.Count; i++)
            {
                Foreman tmp = new Foreman();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.name = data[i]["name"].ToString();
                tmp.avatar = data[i]["avatar"].ToString();
                tmp.role = int.Parse(data[i]["role"].ToString());
                tmp.area = int.Parse(data[i]["area"].ToString());
                tmp.star = int.Parse(data[i]["star"].ToString());
                tmp.qualifiedNum = int.Parse(data[i]["qualified_num"].ToString());
                tmp.starStatus = int.Parse(data[i]["star_status"].ToString()) == 1;
                tmp.rank = int.Parse(data[i]["rank"].ToString());
                tmp.level = int.Parse(data[i]["level"].ToString());
                foremanList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetForemanList, true, foremanList, total);
        }

        /// <summary>
        /// 【给工长点赞】1天内重复点赞视为取消点赞，系统自动判断
        /// </summary>
        /// <param name="foremanId"></param>
        public void SetStar(int foremanId)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Foreman/setStar";
            string data = "foreman_id=" + foremanId;
            StartCoroutine(Post(url, data, OnSetStar));
        }
        /// <summary>
        /// 点赞结果
        /// </summary>
        /// <param name="result"></param>
        private void OnSetStar(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status == 1)
            {
                JsonData data = result["data"];
                if (data == null)
                    return;
                int id = int.Parse(data["foreman_id"].ToString());
                EventManager.instance.NotifyEvent(Event.SetStar, true, id);
            }
            else if (status == 3)
            {
                JsonData data = result["data"];
                if (data == null)
                    return;
                int id = int.Parse(data["foreman_id"].ToString());
                EventManager.instance.NotifyEvent(Event.SetStar, "revoke", id);
            }
            else
            {
                // Debug.LogError("OnSetStar >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.SetStar, false);
                return;
            }

        }
        #endregion

        private void ResetArray(List<string> _list)
        {

        }


        #region 试压模块



        /// <summary>
        /// 试压员开始试压
        /// </summary>
        /// <param name="param"></param>
        public void TakeTestPressure(PostTest param)
        {

            string url = "http://" + UtilManager.getInstance.serverIP + "/Appointment/takeTest";
            string data = "appointment_id=" + param.appointment_id;
            for (int i = 0; i < param.menpaiList.ToArray().Length; i++)
            {
                data += "&pic_doorplate[]=" + param.menpaiList.ToArray()[i];
            }
            for (int i = 0; i < param.stateList.ToArray().Length; i++)
            {
                data += "&pic_instrument[]=" + param.stateList.ToArray()[i];
            }
            for (int i = 0; i < param.chufangList.ToArray().Length; i++)
            {
                data += "&pic_kitchen[]=" + param.chufangList.ToArray()[i];
            }
            for (int i = 0; i < param.weishengjianList.ToArray().Length; i++)
            {
                data += "&pic_toilet[]=" + param.weishengjianList.ToArray()[i];
            }
            for (int i = 0; i < param.ketingList.ToArray().Length; i++)
            {
                data += "&pic_room[]=" + param.ketingList.ToArray()[i];
            }
            for (int i = 0; i < param.guodaoList.ToArray().Length; i++)
            {
                data += "&pic_corridor[]=" + param.guodaoList.ToArray()[i];
            }
            for (int i = 0; i < param.yangtaiList.ToArray().Length; i++)
            {
                data += "&pic_balcony[]=" + param.yangtaiList.ToArray()[i];
            }
            for (int i = 0; i < param.otherList.ToArray().Length; i++)
            {
                data += "&pic_other[]=" + param.otherList.ToArray()[i];
            }
            for (int i = 0; i < param.pingzhengList.ToArray().Length; i++)
            {
                data += "&pic_evidence[]=" + param.pingzhengList.ToArray()[i];
            }
            data +=
                "&build_id=" + param.buildId +
                "&build_name=" + param.buildName +
                "&build_phone=" + param.buildPhone +
                "&test_buy_place=" + param.testBuyPlace +
                "&test_house_type=" + param.testHouseType +
                "&test_kitchen_type=" + param.testKitchenType +
                "&test_toilet_type=" + param.testToiletType +
                "&test_developer=" + param.testDeveloper +
                "&test_decoration=" + param.testDecoration +
                "&test_hvac=" + param.testHvac +
                "&test_air=" + param.testAir +
                "&test_product_type=" + param.testProducyType +
                "&test_length=" + param.testLength +
                "&test_laying_type=" + param.testLayingType +
                "&test_pipeline=" + param.testPipeline +
                "&test_remark=" + param.testRemark +
                "&test_assess=" + param.testAssess +
                "&test_compress=" + param.testCompress +
                "&test_weld=" + param.testWeld +
                "&test_weld_check=" + param.testWeldCheck +
                "&test_keep_start=" + param.testKeepStart +
                "&test_keep_end=" + param.testKeepEnd +
                "&test_operate_pressure=" + param.testOperatePressure +
                "&test_check_pressure=" + param.testCheckPressure +
                // "&test_user_id=" + param.testUserId +
                "&test_notice=" + param.testNotice +
                "&failed_remark=" + param.buhegeStr +
                "&order_status=" + param.orderStatus +
                "&card_type=" + param.cardType;
            for (int i = 0; i < param.buhegeList.ToArray().Length; i++)
            {
                data += "&failed_pic[]=" + param.buhegeList.ToArray()[i];
            }
            Debug.Log(param.buildId + " 最终id");
            Debug.Log(param.buildName + " 最终名字");
            Debug.Log(param.buildPhone + " 最终手机");
            StartCoroutine(Post(url, data, OnTakeTestPressure));
        }

        /// <summary>
        /// 试压员试压结果
        /// </summary>
        /// <param name="result"></param>
        private void OnTakeTestPressure(JsonData result)
        {
            // Debug.Log("一次");
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnTakeTestPressure >>>>error status:" + status);
                // Util.ShowErrorMessage(status);
                GD.UIManager.instance.ShowLoggerMsg(result["msg"].ToString());
                //  EventManager.instance.NotifyEvent(Event.TakeTest, false);
                return;
            }
            else
                EventManager.instance.NotifyEvent(Event.TakeTest);
        }
        /// <summary>
        /// 校验安装人是否为系统已有经销商或水工
        /// </summary>
        /// <param name="phone"></param>
        public void CheckBuilderByPhone(string phone)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/checkBuilderByPhone";
            string data = "phone=" + phone;
            StartCoroutine(Post(url, data, OnCheckBuilderByPhone));
        }

        /// <summary>
        /// 检查安装人员结果
        /// </summary>
        /// <param name="result"></param>
        private void OnCheckBuilderByPhone(JsonData result)
        {
            Builder builder = new Builder();
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnCheckBuilderByPhone >>>>error status:" + status);      
                GD.UIManager.instance.ShowLoggerMsg(result["msg"].ToString());
                builder.id = 0;
                builder.area = -1;
                Debug.Log(builder.id + " 25号id");
                // EventManager.instance.NotifyEvent(Event.CheckBuilder, false,status);
                //  return;
            }
            else
            {
                result = result["data"];
                if (result == null)
                    return;


                #region 之前的逻辑
                //if (status == 1)
                //{ 
                //    builder.id = int.Parse(result["id"].ToString());
                //}
                //else
                //{
                //    builder.name = (result["name"].ToString());
                //    builder.phone = (result["phone"].ToString());
                //}
                //   builder.id = 0;
                #endregion

                #region 2020年3月25日修改逻辑

                builder.id = int.Parse(result["id"].ToString());
                builder.area = ((int)result["area"]);
                //builder.name = (result["name"].ToString());
                // builder.phone = (result["phone"].ToString());

                Debug.Log(builder.id + " 25号id");

                builder.role = int.Parse(result["role"].ToString());

            }
            #endregion


            EventManager.instance.NotifyEvent(Event.CheckBuilder, true, builder);
            Debug.Log("检查完毕");
        }
        #endregion

        #region 任务模块
        /// <summary>
        /// 签到
        /// </summary>
        public void SetSign()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/setSign";
            StartCoroutine(Post(url, OnSetSign));
        }

        /// <summary>
        /// 签到结果
        /// </summary>
        /// <param name="result"></param>
        private void OnSetSign(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnSetSign >>>>error status:" + status);
                //  Util.ShowErrorMessage(status);
                GD.UIManager.instance.ShowLoggerMsg(result["msg"].ToString());
                EventManager.instance.NotifyEvent(Event.SetSign, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.SetSign, true);
        }

        /// <summary>
        /// 检查7天签到
        /// </summary>
        public void Check7Sign()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/takeTaskSevenSign";
            StartCoroutine(Post(url, OnCheck7Sign));
        }

        /// <summary>
        /// 检查7天签到结果
        /// </summary>
        /// <param name="result"></param>
        private void OnCheck7Sign(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //没有完成签到不提示错误消息
                //Debug.LogError("OnCheck7Sign >>>>error status:" + status);
                //Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CheckSevenSign, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CheckSevenSign, true);
        }

        /// <summary>
        /// 检查今日发帖任务是否完成
        /// </summary>
        public void CheckCreateForum()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/takeTaskCreateForum";
            StartCoroutine(Post(url, OnCheckCreateForum));
        }

        /// <summary>
        /// 任务完成结果返回
        /// </summary>
        /// <param name="result"></param>
        private void OnCheckCreateForum(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //没有完成任务不提示错误消息
                //Debug.LogError("OnCheckCreateForum >>>>error status:" + status);
                //Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CheckCreateForum, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CheckCreateForum, true);
        }

        /// <summary>
        /// 检查回帖3次任务是否完成
        /// </summary>
        public void CheckReplyForum()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Forum/takeTaskReplyForum";
            StartCoroutine(Post(url, OnCheckReplyMission));
        }

        /// <summary>
        /// 任务检查结果返回
        /// </summary>
        private void OnCheckReplyMission(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //没有完成任务不提示错误消息
                //Debug.LogError("OnCheckReplyMission >>>>error status:" + status);
                //Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CheckReplyForum, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CheckReplyForum, true);
        }

        /// <summary>
        /// 检查点赞任务是否完成
        /// </summary>
        public void CheckSetStar()
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/User/takeTaskStar";
            StartCoroutine(Post(url, OnCheckSetStar));
        }
        /// <summary>
        /// 任务检查结果返回
        /// </summary>
        /// <param name="result"></param>
        private void OnCheckSetStar(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //没有完成任务不提示错误消息
                //Debug.LogError("OnCheckSetStar >>>>error status:" + status);
                //Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.CheckSetStar, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.CheckSetStar, true);
        }
        #endregion

        #region 积分超市
        /// <summary>
        /// 兑换商品
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        /// <param name="name">订单姓名</param>
        /// <param name="address">订单地址</param>
        /// <param name="phone">联系方式</param>
        /// <param name="remark">备注</param>
        public void ExchangeGood(int goodsId, string name, string address, string phone, string remark)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Goods/exchange";
            string data = "goods_id=" + goodsId +
                "&order_name=" + name +
                "&order_adress=" + address +
                "&order_phone=" + phone +
                "&order_remark+" + remark;
            StartCoroutine(Post(url, data, OnExchangeGood));
        }
        /// <summary>
        /// 商品兑换结果
        /// </summary>
        /// <param name="result"></param>
        private void OnExchangeGood(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                // Debug.LogError("OnExchangeGood >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.ExchangeGood, false);
                return;
            }
            EventManager.instance.NotifyEvent(Event.ExchangeGood, true);
        }

        /// <summary>
        /// 获取我的兑换商品列表
        /// </summary>
        /// <param name="curPage">第几页</param>
        /// <param name="pageCount">每页多少条</param>
        public void GetMyExchangeList(int curPage, int pageCount)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Goods/getExchangeList";
            string data = "page=" + curPage +
                "& page_count=" + pageCount;

            if (curPage == 1)
                DataManager.instance.exchangeList_lastTime = Util.GetTimeStamp();
            else
                data += "&last_time" + DataManager.instance.exchangeList_lastTime.ToString();

            StartCoroutine(Post(url, data, OnGetMyExchangeList));
        }

        /// <summary>
        /// 商品兑换列表返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetMyExchangeList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //   Debug.LogError("OnGetExchangeList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetMyExchangeList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());
            JsonData data = result["list"];
            List<HistoryOrder> order = new List<HistoryOrder>();
            for (var i = 0; i < data.Count; i++)
            {
                HistoryOrder tmp = new HistoryOrder();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.goodsId = int.Parse(data[i]["goods_id"].ToString());
                tmp.goodsTitle = data[i]["goods_title"].ToString();
                tmp.thumb = data[i]["thumb"].ToString();
                tmp.catId = int.Parse(data[i]["cat_id"].ToString());
                tmp.price = int.Parse(data[i]["price"].ToString());
                tmp.name = data[i]["name"].ToString();
                tmp.address = data[i]["address"].ToString();
                tmp.phone = data[i]["phone"].ToString();
                tmp.createTime = data[i]["create_time"].ToString();
                order.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetMyExchangeList, true, order, total, page, pageCount);
        }

        /// <summary>
        ///获取商品列表
        /// </summary>
        /// <param name="catId">分类Id</param>
        /// <param name="curPage">页数</param>
        /// <param name="pageCount">每页条数</param>
        /// <param name="isPen">true:上架 false:下架</param>
        public void GetGoodsList(int catId, int curPage, int pageCount, bool isPen = true)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Goods/getList";
            string data = "cat_id=" + catId +
                "&page=" + curPage +
                "& page_count=" + pageCount;

            if (isPen)
                data += "&search[is_open]=1";
            else
                data += "&search[is_open]=0";

            if (curPage == 1)
                DataManager.instance.goodList_lastTime = Util.GetTimeStamp();
            else
                data += "&last_time=" + DataManager.instance.goodList_lastTime.ToString();

            StartCoroutine(Post(url, data, OnGetGoodsList));
        }

        /// <summary>
        /// 商品兑换列表返回结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetGoodsList(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetGoodsList >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetGoodsList, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            int total = int.Parse(result["total"].ToString());
            int page = int.Parse(result["page"].ToString());
            int pageCount = int.Parse(result["page_count"].ToString());
            JsonData data = result["list"];
            List<Good> goodList = new List<Good>();
            for (var i = 0; i < data.Count; i++)
            {
                Good tmp = new Good();
                tmp.id = int.Parse(data[i]["id"].ToString());
                tmp.title = data[i]["title"].ToString();
                tmp.price = int.Parse(data[i]["price"].ToString());
                tmp.referencePrice = int.Parse(data[i]["reference_price"].ToString());
                tmp.stock = int.Parse(data[i]["stock"].ToString());
                tmp.thumb = data[i]["thumb"].ToString();
                tmp.details = data[i]["details"].ToString();
                tmp.catId = int.Parse(data[i]["cat_id"].ToString());

                goodList.Add(tmp);
            }
            EventManager.instance.NotifyEvent(Event.GetGoodsList, true, goodList, total, page, pageCount);
        }

        /// <summary>
        /// 获取商品详情
        /// </summary>
        /// <param name="goodsId">商品Id</param>
        public void GetGoodsDetail(int goodsId)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Goods/getDetail";
            string data = "goods_id=" + goodsId;
            StartCoroutine(Post(url, data, OnGetGoodsDetail));
        }

        /// <summary>
        /// 商品详情获取结果
        /// </summary>
        /// <param name="result"></param>
        private void OnGetGoodsDetail(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetGoodsDetail >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetGoodsDetail, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;

            Good good = new Good();
            good.id = int.Parse(result["id"].ToString());
            good.title = result["title"].ToString();
            good.price = int.Parse(result["price"].ToString());
            good.referencePrice = int.Parse(result["reference_price"].ToString());
            good.stock = int.Parse(result["stock"].ToString());
            good.thumb = result["thumb"].ToString();
            good.details = result["details"].ToString();
            good.catId = int.Parse(result["cat_id"].ToString());
            good.isOpen = int.Parse(result["is_open"].ToString()) == 1;

            EventManager.instance.NotifyEvent(Event.GetGoodsDetail, true, good);

        }
        #endregion
        #region 工匠卡模块
        /// <summary>
        /// APP用户获取可用工匠卡总数
        /// </summary>
        /// <param name="card_no"></param>
        public void GetListByUser(int page,int page_count,int export)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/CarftsmanCard/getListByUser";
            string data = "page=" + page + "&page_count=" + page_count + "&export=" + export;
            StartCoroutine(Post(url,data, OnGetCarftsmanCard));
        }
        private void OnGetCarftsmanCard(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetGoodsDetail >>>>error status:" + status);
                Util.ShowErrorMessage(status);
                EventManager.instance.NotifyEvent(Event.GetListByUser, false);
                return;
            }
            result = result["data"];
            if (result == null)
                return;
            EventManager.instance.NotifyEvent(Event.GetListByUser, true,result);
        }
        /// <summary>
        /// APP端获取工匠卡二维码
        /// </summary>
        /// <param name="card_no"></param>
        public void GetQrCode(string card_no)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/CarftsmanCard/getQrCode";
            string data = "card_no=" + card_no;
            StartCoroutine(QrCodePost(url, data,OnGetQrCode));
        }
        private void OnGetQrCode(Texture2D result)
        {
            //Debug.Log(result);
            if (result != null)
            {
                EventManager.instance.NotifyEvent(Event.GetQrCode, true,result);
            }
            else
            {
                Util.ShowErrorMessage(0);
                EventManager.instance.NotifyEvent(Event.GetQrCode, false);
            }
        }

        /// <summary>
        /// 试压员修改订单信息
        /// </summary>
        public void ChangeOrderMessage(int _id,string _name,string _phone,string _community, string _address)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Appointment/modifyAppointmentByTester";
            string data = "order_id=" + _id + "&name=" + _name + "&phone=" + _phone + "&community=" + _community + "&address=" + _address;
            StartCoroutine(Post(url, data, OnChangeOrderMessage));
        }

        /// <summary>
        /// 试压员修改订单信息结果
        /// </summary>
        /// <param name="result"></param>
        public void OnChangeOrderMessage(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1 && status != -1)
            {
                //  Debug.LogError("OnGetBaseInfo >>>>error status:" + status);
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "修改失败，请检查修改内容!");
                return;
            }
            else
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "修改成功!");
                EventManager.instance.NotifyEvent(Event.ChangeOrderMessage, true);
            }

        }

        /// <summary>
        /// 获取区域信息(后台接口)
        /// </summary>
        public void GetAreaDetail(int _id,System.Action<string> call=null)
        {
            string url = "http://" + UtilManager.getInstance.serverIP + "/Area/getDetail";
            string data = "id=" + _id;
            if (call == null)
                StartCoroutine(Post(url, data, OnGetAreaDetail));
            else
                StartCoroutine(Post(url, data, (JsonData result) => {
                    int status = int.Parse(result["status"].ToString());
                    if (status != 1)
                        call("未知区域");
                    else
                        call(((string)result["data"]["title"]+"区"));
                }));
        }

        /// <summary>
        /// 获取区域信息结果
        /// </summary>
        /// <param name="result"></param>
        public void OnGetAreaDetail(JsonData result)
        {
            int status = int.Parse(result["status"].ToString());
            if (status != 1)
            {
                //  Debug.LogError("OnGetBaseInfo >>>>error status:" + status);
                Util.ShowAlert(UtilManager.getInstance.messageAlert, "提示", "查询失败!");
                return;
            }
            else
            {
                result = result["data"];
                if (result == null)
                    return;
                UtilManager.getInstance.cardRemark = result["card_remark"].ToString();
            }

        }
        #endregion

        //private void Start()
        //{
        //    string token = GetMD5(GetMD5("13888888888") + "13888888888").Substring(3,8).ToUpper();
        //    Debug.Log(token);
        //}
    }
}

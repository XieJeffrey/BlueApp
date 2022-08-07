using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace XMWorkspace
{
    public class Util
    { 
        /// <summary>
        /// 日期转成时间戳，精确到秒
        /// </summary>
        /// <param name="year">年</param>
        /// <param name="month">月</param>
        /// <param name="day">日</param>
        /// <param name="hour">时</param>
        /// <param name="mintue">分</param>
        /// <param name="second">秒</param>
        /// <returns></returns>
        public static int DateTimeToTimeStamp(int year, int month, int day, int hour = 0, int mintue = 0, int second = 0)
        {
            System.DateTime now = new System.DateTime(year, month, day, hour, mintue, second);
            System.TimeSpan ts = now.ToUniversalTime() - new System.DateTime(1970, 1, 1);
            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 日期转成时间戳，精确到秒
        /// </summary>
        /// <param name="timeString">格式为yyyy-mm-dd hh:mm:ss</param>
        /// <returns></returns>
        public static int DateTimeToTimeStamp(string timeString)
        {
            int year = int.Parse(timeString.Split(' ')[0].Split('-')[0]);
            int month = int.Parse(timeString.Split(' ')[0].Split('-')[1]);
            int day = int.Parse(timeString.Split(' ')[0].Split('-')[2]);
            int hour = int.Parse(timeString.Split(' ')[1].Split('-')[0]);
            int min = int.Parse(timeString.Split(' ')[1].Split('-')[1]);
            int sec = int.Parse(timeString.Split(' ')[1].Split('-')[2]);


            return DateTimeToTimeStamp(year, month, day, hour, min, sec);
        }
        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime StampToDateTime(string timeStamp)
        {
           // Debug.Log(timeStamp);
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long mTime = long.Parse(timeStamp);
            //TimeSpan toNow = new TimeSpan(mTime);
         //   Debug.Log("\n 当前时间为：" + startTime.AddSeconds(mTime).ToString("yyyy/MM/dd HH:mm:ss:ffff"));
            return startTime.AddSeconds(mTime);
        }

        /// <summary>
        /// 时间戳转换为时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static string StampToTimeString(string timeStamp)
        {
            DateTime time = StampToDateTime(timeStamp);

            //return time.ToString("yyyy/MM/dd HH:mm:ss");
            return time.ToString("yyyy/MM/dd HH:mm");
        }
        /// <summary>
        /// 获取当前时间的时间戳，精确到秒
        /// </summary>
        /// <returns></returns>
        public static int GetTimeStamp()
        {
            System.TimeSpan ts = System.DateTime.Now.ToUniversalTime() - new System.DateTime(1970, 1, 1);
            return (int)ts.TotalSeconds; ;
        }

        /// <summary>
        /// 错误消息提示
        /// </summary>
        /// <param name="messageId"></param>
        public static void ShowErrorMessage(int messageId)       
        {
            string msg = "";
            switch (messageId)
            {
                case -1:msg = "失败";break;
                case -2: msg = "参数错误"; break;
                case -3: msg = "验证码错误"; break;
                case -4: msg = "TOKEN校验失败"; break;
                //case 1: msg = "成功"; break;
                case 2: msg = "内容为空"; break;                
                default:msg = "未知错误";break;
            }
            EventManager.instance.NotifyEvent(Event.ShowMessage, msg);
        }

        public static GameObject FindChildByName(GameObject go, string path)
        {
            string[] pathArray = path.Split('/');            
            GameObject tmp = go;
            for (int i = 0; i < pathArray.Length; i++)
            {
                        tmp = tmp.transform.Find(pathArray[i]).gameObject;
            }
            return tmp;
        } 

		/// <summary>
		/// 显示提示界面
		/// </summary>
		/// <param name="_messageObj">提示界面组件</param>
		/// <param name="_title">标题</param>
		/// <param name="_message">消息</param>
		public static void ShowAlert(GameObject _messageObj,string _title,string _message,UnityEngine.Events.UnityAction callBack = null)
		{
           
			Text titleObj = FindChildByName (_messageObj, "Panel/Content/titlePanel/title").GetComponent<Text> ();
			Text messageObj = FindChildByName (_messageObj, "Panel/Content/message").GetComponent<Text> ();
			Button commitBtn = FindChildByName (_messageObj, "Panel/Content/commitBtn").GetComponent<Button> ();
            commitBtn.onClick.RemoveAllListeners();
			titleObj.text = _title;
			messageObj.text = _message;
			if (callBack == null)
				commitBtn.onClick.AddListener (delegate {
					_messageObj.SetActive (false);
				});
			else
				commitBtn.onClick.AddListener (callBack);
			_messageObj.SetActive(true);
		}

        public static void ShowCommit(GameObject _messageObj, string _title, string _message, UnityEngine.Events.UnityAction callBack = null)
        {
            Text titleObj = FindChildByName(_messageObj, "Panel/Content/titlePanel/title").GetComponent<Text>();
            Text messageObj = FindChildByName(_messageObj, "Panel/Content/message").GetComponent<Text>();
            Button commitBtn = FindChildByName(_messageObj, "Panel/Content/commitBtn").GetComponent<Button>();
            Button backBtn = FindChildByName(_messageObj, "Panel/Content/backBtn").GetComponent<Button>();

            commitBtn.onClick.RemoveAllListeners();
            backBtn.onClick.RemoveAllListeners();

            titleObj.text = _title;
            messageObj.text = _message;
            if (callBack == null)
            {
                
                commitBtn.onClick.AddListener(delegate {
                    _messageObj.SetActive(false);
                });
                backBtn.onClick.AddListener(delegate {
                    _messageObj.SetActive(false);
                });
            }
            else
            {
                
                commitBtn.onClick.AddListener(callBack);
                backBtn.onClick.AddListener(delegate {
                    _messageObj.SetActive(false);
                });
            }
            _messageObj.SetActive(true);
        }


    }
}

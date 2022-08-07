using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 开始界面
    /// </summary>
    public class StartPanel : MonoBehaviour
    {
        /// <summary>
        /// 开始
        /// </summary>
        IEnumerator  Start()
        {
            // PlayerPrefs.DeleteKey("TOKEN");
           // AndroidMethodManager.CallOtherApp();
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetBaseInfo, OnGetBaseInfo);
            yield return new WaitForSeconds(3);
            if (PlayerPrefs.HasKey("TOKEN"))
            {
                UserData.instance.token = PlayerPrefs.GetString("TOKEN");
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetBaseInfo();
            }else
            {
                UIManager.instance.GoToSelectPanel(gameObject);
            }
            
           // UserData.instance.token = "mwpVJ5K0tjojPkaL";
            //UIManager.instance.GoToSelectPanel(gameObject);
           
           // GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAreaList(1, 50);
        }

        private void OnGetBaseInfo(object[] obj)
        {
            if (!gameObject.activeSelf) return;
            gameObject.SetActive(false);

            if ((bool)obj[0])
            {
				
                UIManager.instance.OpenMainPanel(gameObject);
               // UIManager.instance.GoToForumMain();
            }
            else
            {
                UIManager.instance.GoToSelectPanel(gameObject);
            }
        }
    }
}

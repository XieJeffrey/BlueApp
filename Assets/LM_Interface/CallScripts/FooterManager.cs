using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using SVGImporter;
using GD;

namespace LM_Workspace
{
	public class FooterManager :MonoBehaviour
	{
		
		/// <summary>
		/// 当前打开的模块
		/// </summary>
		private GameObject openedGo;

		public EnumManager.footerCloseType footerType;
		// Use this for initialization
		void Start () 
		{
            RegisterBtn();
        }

		void OnEnable()
		{
          
            if (UserData.instance.role == 3)
                Util.FindChildByName(this.gameObject, "btn_editor/label").GetComponent<Text>().text = "订单";
            else
                Util.FindChildByName(this.gameObject, "btn_editor/label").GetComponent<Text>().text = "预约";
        }

		private void RegisterBtn()
		{
			UIEventListener forumBtn = Util.FindChildByName (this.gameObject, "btnlayout/btn_lanquan").GetComponent<UIEventListener>();
			UIEventListener foremanRankBtn = Util.FindChildByName (this.gameObject, "btnlayout/btn_paihang").GetComponent<UIEventListener>();
			UIEventListener createAppoimentBtn = Util.FindChildByName (this.gameObject, "btn_editor").GetComponent<UIEventListener>();
			UIEventListener marketBtn = Util.FindChildByName (this.gameObject, "btnlayout/btn_chaoshi").GetComponent<UIEventListener>();
			UIEventListener myselfBtn = Util.FindChildByName (this.gameObject, "btnlayout/btn_wode").GetComponent<UIEventListener>();

			forumBtn.onClick = ForumBtnClick;
			foremanRankBtn.onClick = ForemanRankBtnClick;
			createAppoimentBtn.onClick = CreateAppoimentBtnClick;
           

            marketBtn.onClick = MarketBtnClick;
			myselfBtn.onClick = MySelfBtnClick;
//			forumIcon = forumBtn.transform.Find ("icon").GetComponent<SVGImage> ();



		}

		void ForumBtnClick(GameObject go, PointerEventData eventdata)
		{
			GD.UIManager.instance.OpenMainPanel();
			UniWebViewPluginManager.getInstance.HideBaiDuMap ();
			SelectFooterType (delegate {
				UIManager.instance.GoToForumMain();
			});
		}

		void ForemanRankBtnClick(GameObject go, PointerEventData eventdata)
		{
			//Debug.Log ("点击了排行");
			UniWebViewPluginManager.getInstance.HideBaiDuMap ();
			UtilManager.getInstance.foremanRankGo.SetActive (true);
			SelectFooterType ();
		}

		void CreateAppoimentBtnClick(GameObject go, PointerEventData eventdata)
		{
			if (UserData.instance.role != 3) 
			{
                UtilManager.getInstance.createAppoimentGo.SetActive(true);
                //Util.ShowAlert (UtilManager.getInstance.messageAlert, "提示", "试压员无法预约!");
                //return;
            }
			
			SelectFooterType(delegate {
                if (UserData.instance.role == 3)
                    UIManager.instance.GoToOrderPanel();
            } );
           
        }

		void MarketBtnClick(GameObject go, PointerEventData eventdata)
		{
			UniWebViewPluginManager.getInstance.HideBaiDuMap ();
			UtilManager.getInstance.marketGo.SetActive (true);
			SelectFooterType ();
		}

		void MySelfBtnClick(GameObject go, PointerEventData eventdata)
		{
			UniWebViewPluginManager.getInstance.HideBaiDuMap ();
			UtilManager.getInstance.myselfGo.SetActive (true);
			SelectFooterType (delegate {
				UIManager.instance.OpenMainPanel();
			});
		}

		void SelectFooterType(System.Action callBack = null)
		{
			Transform _trans = null;
			switch (footerType)
			{

			case EnumManager.footerCloseType.forum:
				_trans = UtilManager.getInstance.forumGo.transform;
				break;
			case EnumManager.footerCloseType.foremanRank:
				_trans = UtilManager.getInstance.foremanRankGo.transform;
				//_trans.GetComponent<ForemanRankManager> ().StopAllCoroutines ();
				break;
			case EnumManager.footerCloseType.createAppoiment:
				_trans = UtilManager.getInstance.createAppoimentGo.transform;
				break;
			case EnumManager.footerCloseType.market:
				_trans = UtilManager.getInstance.marketGo.transform;
				//_trans.GetComponent<MarketManager> ().StopAllCoroutines ();
				break;
			case EnumManager.footerCloseType.myself:
				_trans = UtilManager.getInstance.myselfGo.transform;
				break;
			}
			for (int i = 0; i < _trans.childCount; i++)
			{
				_trans.GetChild (i).gameObject.SetActive (false);
			}
			_trans.gameObject.SetActive (false);


			if (callBack != null) 
			{
				callBack ();
			}
		}
	}
}

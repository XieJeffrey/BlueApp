using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using SVGImporter;

namespace LM_Workspace
{
	public class ForemanRankManager : SingletonMono<ForemanRankManager> 
	{
//		/// <summary>
//		/// 排行按钮
//		/// </summary>
//		private UIEventListener foremanRankBtn;

		/// <summary>
		/// 代表每个人信息的预制物
		/// </summary>
		private GameObject foremanListPrefab;

		/// <summary>
		/// 预制物的父物体
		/// </summary>
		private Transform foremanItemParent;

		/// <summary>
		/// 左边拖拽点赞按钮
		/// </summary>
		private UIEventListener dragClickedBtn;
		/// <summary>
		/// 左边拖拽点赞按钮Rect
		/// </summary>
		private RectTransform dragClickedRect;

		/// <summary>
		/// 右边点赞按钮
		/// </summary>
		private UIEventListener rightClickedBtn;
		/// <summary>
		/// 右边点赞按钮Rect
		/// </summary>
		private RectTransform rightClickedRect;

		/// <summary>
		/// 排行label
		/// </summary>
		private Text rankLabel;

		/// <summary>
		/// 姓名label
		/// </summary>
		private Text userNameLabel;

		/// <summary>
		/// 所获赞数label
		/// </summary>
		private Text starNumLabel;

		/// <summary>
		/// 等级label
		/// </summary>
		private Text levelLabel;

		/// <summary>
		/// 点赞数label
		/// </summary>
		private Text rightClickedLabel;

		/// <summary>
		/// 工长个人信息list
		/// </summary>
		private List<Foreman> foremanList;

		/// <summary>
		/// 打开搜索按钮
		/// </summary>
		private UIEventListener openSearchBtn;

		/// <summary>
		/// 搜索组件
		/// </summary>
		private GameObject SearchPanel;

		/// <summary>
		/// 搜索界面input
		/// </summary>
		private InputField searchInput;

		/// <summary>
		/// 搜索按钮
		/// </summary>
		private UIEventListener searchBtn;

        /// <summary>
        /// 说明信息按钮
        /// </summary>
        private UIEventListener infoOpenBtn;

        /// <summary>
        /// 说明信息关闭按钮
        /// </summary>
        private UIEventListener infoCloseBtn;

        /// <summary>
        /// 说明信息界面
        /// </summary>
        private GameObject infoObj;

		/// <summary>
		/// 用工长id做标识的字典
		/// </summary>
		private Dictionary<int,GameObject> foremanDic;

		/// <summary>
		///  头像
		/// </summary>
		private RawImage headImage;

		/// <summary>
		/// 显示总共有多少工友
		/// </summary>
		private Text totalLabel;

		private GameObject rankChildPanel;

		void OnEnable()
		{
			

			if(!rankChildPanel.activeInHierarchy)
				rankChildPanel.SetActive (true);
			SearchPanel.SetActive (false);
			foremanDic = new Dictionary<int, GameObject> ();



			GameObject.Find ("Camera").GetComponent<InterfaceManager> ().GetForemanList (null);
		}

		// Use this for initialization
		protected override void Awake ()
		{
			base.Awake ();

			rankChildPanel = Util.FindChildByName (gameObject, "p_main");
			rankChildPanel.SetActive (true);
			//foremanRankBtn = GameObject.Find ("btn_paihang_foremanRank").GetComponent<UIEventListener> ();
			foremanListPrefab = Resources.Load ("foreman_item") as GameObject;

			if(GameObject.Find("foremanItemParent") != null)
				foremanItemParent = GameObject.Find ("foremanItemParent").transform;

			if(GameObject.Find("searchBtn_foremanRank") != null)
				openSearchBtn = GameObject.Find ("searchBtn_foremanRank").GetComponent<UIEventListener> ();

			if(GameObject.Find("searchPanel_ForemanRank") != null)
				SearchPanel = GameObject.Find ("searchPanel_ForemanRank");

			searchInput = Util.FindChildByName (SearchPanel, "searchbar/input_search").GetComponent<InputField>();
			searchBtn = Util.FindChildByName (SearchPanel, "searchbar/btn_search").GetComponent<UIEventListener> ();

            infoObj = Util.FindChildByName(gameObject, "p_main/infoBG");
            infoOpenBtn = Util.FindChildByName(gameObject, "p_main/header/infoOpenBtn").GetComponent<UIEventListener>();
            infoCloseBtn = Util.FindChildByName(infoObj, "infoImage/Btn").GetComponent<UIEventListener>();

			if(GameObject.Find("totalLabel_ForemanRank") != null)
				totalLabel = GameObject.Find ("totalLabel_ForemanRank").GetComponent<Text> ();

			openSearchBtn.onClick = OpenSearchPanelClick;
			searchBtn.onClick = SearchBtnClick;
            infoOpenBtn.onClick = InfoOpenBtnClick;
            infoCloseBtn.onClick = InfoCloseBtnClick;


			EventManager.instance.RegisterEvent(XMWorkspace.Event.GetForemanList,OnForemanRank);
			EventManager.instance.RegisterEvent(XMWorkspace.Event.SetStar,OnSetStar);
		}


		void OnDisable()
		{
			StopAllCoroutines ();
			CleanData ();
		}

//		void ForemanRankClick(GameObject go, PointerEventData eventdata)
//		{
//			//UserData.instance.token = "mwpVJ5K0tjojPkaL";
//			GameObject.Find ("Camera").GetComponent<InterfaceManager> ().GetForemanList (null);
//		}



		void OnForemanRank(object[] data)
		{
			//Debug.Log ("hahaha");
			StopAllCoroutines ();
			CleanData ();
			if(gameObject.activeInHierarchy)
				StartCoroutine(ForeamListYield (data));


		}

		IEnumerator ForeamListYield(object[] data)
		{
			
			//Debug.Log (data [0].ToString ());
			if (data [0].ToString () == "True") {
				//Debug.Log (data [2].ToString() + "total");
				totalLabel.text = "共有 " + data [2].ToString () + " 名工友";
				foremanList = data [1] as List<Foreman>;
				for (int i = 0; i < foremanList.Count; i++) {
					Transform foremanTrans = Instantiate (foremanListPrefab).transform;

					foremanTrans.SetParent (foremanItemParent);
					RectTransform rect = foremanTrans.GetComponent<RectTransform> ();
					rect.anchoredPosition3D = new Vector3 (rect.anchoredPosition3D.x, rect.anchoredPosition3D.y, 0);
					rect.localScale = Vector3.one;
					yield return StartCoroutine(SetForemanInfo (foremanTrans.gameObject, foremanList [i], i));
				}
			} else 
			{
				Util.ShowAlert (UtilManager.getInstance.messageAlert, "查询", UtilManager.getInstance.ApplicationError);
			}
		}

		void ChangeRectColor(GameObject _foremanGo,Color _color,int starNum)
		{
			
			dragClickedBtn = Util.FindChildByName (_foremanGo, "drager/dragClicked_foremanRank").GetComponent<UIEventListener> ();

			dragClickedRect = dragClickedBtn.GetComponent<RectTransform> ();

			rightClickedBtn = Util.FindChildByName (_foremanGo, "drager/doc/rightClicked_foremanRank").GetComponent<UIEventListener> ();

			rightClickedRect = rightClickedBtn.GetComponent<RectTransform> ();

			starNumLabel = Util.FindChildByName (_foremanGo, "drager/doc/center/numberLabel").GetComponent<Text> ();

			rightClickedLabel = Util.FindChildByName (_foremanGo, "drager/doc/rightClicked_foremanRank/label").GetComponent<Text> ();

			dragClickedRect.GetComponent<Image> ().color = _color;
			rightClickedRect.GetComponent<SVGImage>().color = _color;

			starNumLabel.text = (int.Parse (starNumLabel.text) + starNum).ToString ();
			if(int.Parse(rightClickedLabel.text) < 999)
				rightClickedLabel.text = (int.Parse (rightClickedLabel.text) + starNum).ToString ();
		}

		IEnumerator SetForemanInfo(GameObject _foremanGo,Foreman _value,int _listID)
		{
			
			//Debug.Log (_foremanGo.name);
			dragClickedBtn = Util.FindChildByName (_foremanGo, "drager/dragClicked_foremanRank").GetComponent<UIEventListener> ();

			dragClickedRect = dragClickedBtn.GetComponent<RectTransform> ();

			rightClickedBtn = Util.FindChildByName (_foremanGo, "drager/doc/rightClicked_foremanRank").GetComponent<UIEventListener> ();

			rightClickedRect = rightClickedBtn.GetComponent<RectTransform> ();

			dragClickedBtn.onClick = SetStarBtnClick;
			rightClickedBtn.onClick = SetStarBtnClick;

			rankLabel = Util.FindChildByName (_foremanGo, "drager/doc/left/rankLabel").GetComponent<Text> ();
			userNameLabel = Util.FindChildByName (_foremanGo, "drager/doc/center/userName").GetComponent<Text> ();
			starNumLabel = Util.FindChildByName (_foremanGo, "drager/doc/center/numberLabel").GetComponent<Text> ();
			levelLabel = Util.FindChildByName (_foremanGo, "drager/doc/center/levelLabel").GetComponent<Text> ();
			rightClickedLabel = Util.FindChildByName (_foremanGo, "drager/doc/rightClicked_foremanRank/label").GetComponent<Text> ();
			headImage = Util.FindChildByName (_foremanGo, "drager/doc/center/userHead").GetComponent<RawImage> ();
			headImage.GetComponent<SelfImageHelper> ().imageUrl = _value.avatar;
			if (!foremanDic.ContainsKey (_value.id))
				foremanDic.Add (_value.id, _foremanGo);
			//未点赞
			if (!_value.starStatus) 
			{
				dragClickedRect.GetComponent<Image> ().color = UtilManager.getInstance.grayColor;
				rightClickedRect.GetComponent<SVGImage>().color = UtilManager.getInstance.grayColor;

			}
			else
			{
				dragClickedRect.GetComponent<Image> ().color = UtilManager.getInstance.greenColor;
				rightClickedRect.GetComponent<SVGImage>().color = UtilManager.getInstance.greenColor;

			}
			rankLabel.text = _value.rank.ToString ();
			userNameLabel.text = _value.name;
			starNumLabel.text = _value.qualifiedNum.ToString ();
			if (_value.star <= 999)
				rightClickedLabel.text = _value.star.ToString ();
			else
				rightClickedLabel.text = "999+";
			levelLabel.text = CheckLevel (_value.level);
			dragClickedBtn.GetComponent<GetListPos> ().listId = _value.id;
			rightClickedBtn.GetComponent<GetListPos> ().listId = _value.id;
			yield return new WaitForEndOfFrame ();
			//yield return WaitForEndOfFrame ();
//			if (_value.avatar != "") 
//			{
//				yield return PostImageManager.getInstance.LoadImageFromUrl(_value.avatar, UtilManager.getInstance.headImage_foreamRank_SizeX, UtilManager.getInstance.headImage_foreamRank_SizeY, delegate {
//					return headImage;
//				});
//			}
		}

		void SetStarBtnClick(GameObject go, PointerEventData eventdata)
		{
			SetStar (go.GetComponent<GetListPos> ().listId);
		}

		/// <summary>
		/// 点赞
		/// </summary>
		void SetStar(int _id)
		{
			GameObject.Find ("Camera").GetComponent<InterfaceManager> ().SetStar (_id);
		}

		void OnSetStar(object[] data)
		{
			Debug.Log (data [0] + "pppppppppppp");
			if (data [0].ToString () == "False") {
				Util.ShowAlert (UtilManager.getInstance.messageAlert, "点赞", UtilManager.getInstance.ApplicationError);
			} else if (data [0].ToString () == "True") 
			{
				GameObject foremanGo = foremanDic [int.Parse (data [1].ToString ())];

				ChangeRectColor (foremanGo, UtilManager.getInstance.greenColor,1);

			} else 
			{
				GameObject foremanGo = foremanDic [int.Parse (data [1].ToString ())];

				ChangeRectColor (foremanGo, UtilManager.getInstance.grayColor,-1);
			}
		}

		string CheckLevel(int _level)
		{
			string levelStar = "";
			switch (_level) 
			{
			case 0:
				levelStar = "新手";
				break;
			case 1:
				levelStar = "工匠";
				break;
			case 2:
				levelStar = "巧匠";
				break;
			case 3:
				levelStar = "鲁班";
				break;
			case 4:
				levelStar = "大师";
				break;
			case 5:
				levelStar = "中财权威认证";
				break;
			}
			return levelStar;
		}

		void OpenSearchPanelClick(GameObject go, PointerEventData eventdata)
		{
			SearchPanel.SetActive (true);
		}

		void SearchBtnClick(GameObject go, PointerEventData eventdata)
		{
			//UserData.instance.token = "mwpVJ5K0tjojPkaL";
			SearchPanel.SetActive (false);
			if (searchInput.text == "")
				return;
			string[] nameStr = new string[1]{searchInput.text};
			GameObject.Find ("Camera").GetComponent<InterfaceManager> ().GetForemanList (nameStr);

		}

        void InfoOpenBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(true);

        }
        void InfoCloseBtnClick(GameObject go, PointerEventData eventdata)
        {
            infoObj.SetActive(false);

        }

		void CleanData()
		{
			if (foremanList!=null)
			{
				foremanList.Clear ();
				for (int i = 0; i < foremanItemParent.childCount; i++) 
				{

					Destroy(foremanItemParent.GetChild (i).gameObject);
				}
			}

			if (foremanDic != null)
				foremanDic.Clear ();
		}
	}
}

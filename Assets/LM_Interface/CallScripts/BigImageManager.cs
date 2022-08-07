using System.Collections;
using System.Collections.Generic;
using GD;
using UnityEngine;
using UnityEngine.EventSystems;
using XMWorkspace;

namespace LM_Workspace
{
    public class BigImageManager : SingletonMono<BigImageManager>
    {
        /// <summary>
        /// 返回按钮
        /// </summary>
        private UIEventListener backBtn;

        /// <summary>
        /// 删除图片按钮
        /// </summary>
        private UIEventListener destroyBtn;

        [System.NonSerialized]
        /// <summary>
        /// 当前被放大图片类型
        /// </summary>
        public string currentTypeStr;

        [System.NonSerialized]
        /// <summary>
        /// 当前被放大图片名字
        /// </summary>
        public string currentItemName;


        // Start is called before the first frame update
        void Start()
        {
            backBtn = Util.FindChildByName(gameObject, "header/btn_back").GetComponent<UIEventListener>();
            destroyBtn = Util.FindChildByName(gameObject, "DestroyBtn").GetComponent<UIEventListener>();
            
            backBtn.onClick = BackBtnClick;
            destroyBtn.onClick = DestroyBtnClick;

        }

		private void OnEnable()
		{
            
            if(destroyBtn == null)
                destroyBtn = Util.FindChildByName(gameObject, "DestroyBtn").GetComponent<UIEventListener>();

            if (UtilManager.getInstance.isBigImageShowDestroyBtn)
                destroyBtn.gameObject.SetActive(true);
            else
                destroyBtn.gameObject.SetActive(false);
		}

		void BackBtnClick(GameObject go, PointerEventData eventdata)
        {
            
            UtilManager.getInstance.bigRawImage.texture = null;
            gameObject.SetActive(false);

        }

        void DestroyBtnClick(GameObject go, PointerEventData eventdata)
        {
            UIManager.instance.ShowCommitMsg("是否确认删除此照片?", "提示", delegate {

                StartCoroutine(DestroyImage());
            });

        }

        void ClearData(Transform _parent,Dictionary<string,string> _dic)
        {
            for (int i = 0; i < _parent.childCount; i++)
            {
                if (_parent.GetChild(i).name == currentItemName)
                {
                    DestroyImmediate(_parent.GetChild(i).gameObject);
                    break;
                }
            }
            _dic.Remove(currentItemName);
            UtilManager.getInstance.photoKey[currentTypeStr] = _dic;


        }

        IEnumerator DestroyImage()
        {
            switch (currentTypeStr)
            {
                case "menpai":
                    ClearData(UtilManager.getInstance.menpaiParent, UtilManager.getInstance.menpaiDic);
                    break;
                case "state":
                    ClearData(UtilManager.getInstance.stateParent, UtilManager.getInstance.stateDic);
                    break;
                case "chufang":
                    ClearData(UtilManager.getInstance.chufangParent, UtilManager.getInstance.chufangDic);
                    break;
                case "weishengjian":
                    ClearData(UtilManager.getInstance.weishengjianParent, UtilManager.getInstance.weishengjianDic);
                    break;
                case "keting":
                    ClearData(UtilManager.getInstance.ketingParent, UtilManager.getInstance.ketingDic);
                    break;
                case "guodao":
                    ClearData(UtilManager.getInstance.guodaoParent, UtilManager.getInstance.guodaoDic);
                    break;
                case "yangtai":
                    ClearData(UtilManager.getInstance.yangtaiParent, UtilManager.getInstance.yangtaiDic);
                    break;
                case "other":
                    ClearData(UtilManager.getInstance.otherParent, UtilManager.getInstance.otherDic);
                    break;
                case "pingzheng":
                    ClearData(UtilManager.getInstance.pingzhengParent, UtilManager.getInstance.pingzhengDic);
                    break;
                case "buhege":
                    ClearData(UtilManager.getInstance.buhegeParent, UtilManager.getInstance.buhegeDic);
                    break;
            }
            yield return new WaitForEndOfFrame();
            UtilManager.getInstance.bigRawImage.texture = null;
            gameObject.SetActive(false);
        }
    }
}

using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;

namespace GD
{

    public class UIManager : SingleMono<UIManager>
    {

      
        private void Start()
        {
           // UserData.instance.token = "mwpVJ5K0tjojPkaL";
            ///登录系统组件
            loginPanel = transform.Find("- login & signin -").gameObject;
            loginShouYe= loginPanel.transform.Find("p_splash").gameObject;

            StartCoroutine(LoadImageRunning());
          


           
          
          
           
         
           

        }


        #region 登录系统
        /// <summary>
        /// 登录系统
        /// </summary>
        private GameObject loginPanel;
        /// <summary>
        /// 登录系统首界面
        /// </summary>
        private GameObject loginShouYe;
        /// <summary>
        /// 登录系统验证码登录界面
        /// </summary>
        private GameObject loginYanZhongMa;
        /// <summary>
        /// 登录系统普通登录界面
        /// </summary>
        private GameObject loginMiMa;
        /// <summary>
        /// 登录系统注册界面
        /// </summary>
        private GameObject loginZhuCe;
        /// <summary>
        /// 登录系统登录类型选择界面
        /// </summary>
        private GameObject loginDengLuFangShi;
        /// <summary>
        /// 登录系统角色类型选择界面
        /// </summary>
        private GameObject loginRoleTypeSelect;
        /// <summary>
        /// 登录系统区域选择界面
        /// </summary>
        private GameObject loginAreaSelect;
        /// <summary>
        /// 关闭登录 注册 系统界面
        /// </summary>
        public void CloseLoginPanel()
        {
            loginPanel.SetActive(false);
        }
        /// <summary>
        /// 跳转至登录选择界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToSelectPanel(GameObject current)
        {
            loginPanel.SetActive(true);
            current.SetActive(false);
            if(loginDengLuFangShi==null)
            loginDengLuFangShi = loginPanel.transform.Find("p_select").gameObject;
            loginDengLuFangShi.SetActive(true);

        }

        /// <summary>
        /// 跳转至密码登录界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToLoginPanel(GameObject current)
        {
            current.SetActive(false);
            if(loginMiMa==null)
            loginMiMa = loginPanel.transform.Find("p_login").gameObject;
            loginMiMa.SetActive(true);

        }
        /// <summary>
        /// 跳转至验证码登录界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToYanZhengLoginPanel(GameObject current)
        {
            current.SetActive(false);
            if(loginYanZhongMa==null)
            loginYanZhongMa = loginPanel.transform.Find("p_yanzheng").gameObject;
            loginYanZhongMa.SetActive(true);

        }
        /// <summary>
        /// 跳转至注册界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToZhuCePanel(GameObject current)
        {
            current.SetActive(false);
            if(loginZhuCe==null)
            loginZhuCe = loginPanel.transform.Find("p_signin").gameObject;
            loginZhuCe.SetActive(true);

        }
        /// <summary>
        /// 跳转身份选择界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToRoleSelectPanel(GameObject current)
        {
            current.SetActive(false);
            if(loginRoleTypeSelect==null)
            loginRoleTypeSelect = loginPanel.transform.Find("p_userTypeSelect").gameObject;
            loginRoleTypeSelect.SetActive(true);

        }

        /// <summary>
        /// 跳转区域选择界面
        /// </summary>
        /// <param name="current"></param>
        public void GoToAreaSelectPanel(GameObject current)
        {
          
            current.SetActive(false);
            if (loginAreaSelect == null)
                loginAreaSelect = loginPanel.transform.Find("p_areaSelect").gameObject;
            loginAreaSelect.GetComponent<UserAreaSelectPanel>().Show();

        }
        /// <summary>
        /// 返回上一个界面
        /// </summary>
        public void BackToLastPanel()
        {
            IRefresh lastPanel = panelCache.Pop();
            if (lastPanel!=null)
            {
                lastPanel.Reload();
            }

        }
        #endregion

        #region 我的模块

        /// <summary>
        /// 操作主界面
        /// </summary>
        private MainPanel wode;
        /// <summary>
        /// 蓝圈界面
        /// </summary>
        private Panel lanQuan;
        /// <summary>
        /// 收藏界面
        /// </summary>
        private Panel shouCang;
        /// <summary>
        /// 订单界面 试压员
        /// </summary>
        private Panel dingDan_shiya;
        /// <summary>
        /// 订单界面 水工
        /// </summary>
        private DingDanPanel dingDan_shuiGong;
        /// <summary>
        /// 邀请界面
        /// </summary>
        private Panel yaoQing;
        /// <summary>
        /// 积分界面
        /// </summary>
        private Panel jiFen;
        /// <summary>
        /// 兑换界面
        /// </summary>
        private Panel duiHuan;
        /// <summary>
        /// 设置界面
        /// </summary>
        private Panel sheZhi;
        /// <summary>
        /// 修改密码界面
        /// </summary>
        private Panel xiuGaiMima;

        /// <summary>
        /// 修改信息界面
        /// </summary>
        private Panel xiuGaiXinXi;
       

        /// <summary>
        /// 我的等级界面
        /// </summary>
        private Panel level;
        /// <summary>
        /// 我的工匠卡
        /// </summary>
        private Panel craftsman;
        /// <summary>
        /// 试压订单信息界面
        /// </summary>
        private DingDanInfo_ShiYaPanel dingDanInfo_shiya;
        /// <summary>
        /// 安装 预约 订单信息界面
        /// </summary>
        private DingDanInfoPanel dingDanInfo_shuigong;
        /// <summary>
        /// 试压订单信息详情界面
        /// </summary>
        private DingDanDetail_ShiYaPanel dingDanDetailInfo_shiya;
        /// <summary>
        /// 安装 预约 订单信息详情界面
        /// </summary>
        private DingDanDetailPanel dingDanDetailInfo_shuigong;

        /// <summary>
        /// 试压完成结果界面
        /// </summary>
        DingDanStatePanel shiYaState;
        /// <summary>
        /// 打开对应身份的主操作界面
        /// </summary>
        public void OpenMainPanel( GameObject obj)
        {
           // CloseLoginPanel();
            obj.SetActive(false);
            if (wode==null)
            wode = transform.Find("- wode -").GetComponent<MainPanel>();
            wode.gameObject.SetActive(true);
            for(int i = 0; i < wode.transform.childCount; i++)
            {
                wode.transform.GetChild(i).gameObject.SetActive(false);

            }
            panelCache.Clear();
            wode.OpenMainPanel();
        }
        public void OpenMainPanel()
        {
            // CloseLoginPanel();
            if (wode == null)
                wode = transform.Find("- wode -").GetComponent<MainPanel>();
            wode.gameObject.SetActive(true);
            for (int i = 0; i < wode.transform.childCount; i++)
            {
                wode.transform.GetChild(i).gameObject.SetActive(false);

            }
            panelCache.Clear();
            wode.OpenMainPanel();
        }
        /// <summary>
        /// 跳转到收藏界面
        /// </summary>
        public void GoToShouCang(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (shouCang==null)
                shouCang = wode.transform.Find("p_shoucang").GetComponent<Panel>();
            shouCang.Show();


        }

        /// <summary>
        /// 跳转到蓝圈界面
        /// </summary>
        public void GoToLanQuan(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (lanQuan == null)
                lanQuan = wode.transform.Find("p_lanquan").GetComponent<Panel>();
            lanQuan.Show();
        }
         
      
      

        ///// <summary>
        ///// 跳转到试压员订单界面
        ///// </summary>
        //public void GoToOrder_ShiYa(IRefresh lastPanel)
        //{


        //    lastPanel.ClearCache();
        //    panelCache.Push(lastPanel);
        //    if (dingDan_shiya == null)
        //        dingDan_shiya = wode.transform.Find("p_dingdanshiya").GetComponent<Panel>();
        //    dingDan_shiya.Show();
        //}
        /// <summary>
        /// 跳转到试压员订单界面
        /// </summary>
        public void GoToOrderPanel()
        {
            Debug.Log(1111111);
            if (wode == null)
                wode = transform.Find("- wode -").GetComponent<MainPanel>();
            wode.gameObject.SetActive(true);
            for (int i = 0; i < wode.transform.childCount; i++)
            {
                wode.transform.GetChild(i).gameObject.SetActive(false);

            }
            if (dingDan_shiya == null)
                dingDan_shiya = wode.transform.Find("p_dingdanshiya").GetComponent<Panel>();
            dingDan_shiya.Show();
        }
        /// <summary>
        /// 跳转到水工订单界面
        /// 
        /// 1 预约
        /// 2 安装 
        /// 3 试压
        /// </summary>
        public void GoToOrder_ShuiGong(int index)
        {
            if (dingDan_shuiGong == null)
                dingDan_shuiGong = wode.transform.Find("p_dingdan").GetComponent<DingDanPanel>();
            dingDan_shuiGong.Show(index);
        }
        /// <summary>
        /// 显示水工订单详情
        /// </summary>
        /// <param name="data"></param>
        public void ShowOrderInfo_ShuiGong(Order data)
        {
            if (dingDanInfo_shuigong == null)
                dingDanInfo_shuigong = wode.transform.Find("p_dingdan_info").GetComponent<DingDanInfoPanel>();
            dingDanInfo_shuigong.Show(data);
        }
        /// <summary>
        /// 显示试压员订单详情
        /// </summary>
        /// <param name="data"></param>
        public void ShowOrderInfo_ShiYa(Order data)
        {
            if (dingDanInfo_shiya == null)
                dingDanInfo_shiya = wode.transform.Find("p_dingdanshiya_info").GetComponent<DingDanInfo_ShiYaPanel>();
            dingDanInfo_shiya.Show(data);
        }
        /// <summary>
        /// 显示试压订单更多信息
        /// </summary>
        /// <param name="data"></param>
        public void ShowOrderDetailInfo_ShiYa(Order data)
        {
            if (dingDanDetailInfo_shiya == null)
                dingDanDetailInfo_shiya = wode.transform.Find("p_dingdanshiya_detail").GetComponent<DingDanDetail_ShiYaPanel>();
            dingDanDetailInfo_shiya.Show(data);
        }
        /// <summary>
        /// 查看试压结果信息
        /// </summary>
        /// <param name="data"></param>
        public void ShowOrderDetailInfo_ShuiGong(Order data)
        {
            if (dingDanDetailInfo_shuigong == null)
                dingDanDetailInfo_shuigong = wode.transform.Find("p_dingdan_detail").GetComponent<DingDanDetailPanel>();
            dingDanDetailInfo_shuigong.Show(data);
        }


        /// <summary>
        /// 显示试压结果界面
        /// </summary>
        /// <param name="value"></param>
        public void GoToShiYaJieGuo(bool value)
        {
            if(shiYaState==null)
                shiYaState = wode.transform.Find("p_dingdanshiya_state").GetComponent<DingDanStatePanel>();
            shiYaState.Show(value);

        }
        /// <summary>
        /// 跳转到邀请界面
        /// </summary>
        public void GoToYaoQing(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (yaoQing == null)
                yaoQing = wode.transform.Find("p_yaoqing").GetComponent<Panel>();
            yaoQing.Show();

        }

        /// <summary>
        /// 跳转至积分界面
        /// </summary>
        public void GoToJiFen(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (jiFen == null)
                jiFen = wode.transform.Find("p_jifen").GetComponent<Panel>();
            jiFen.Show();
        }

        /// <summary>
        /// 跳转至兑换界面
        /// </summary>
        public void GoToDuiHuan(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (duiHuan == null)
                duiHuan = wode.transform.Find("p_duihuan").GetComponent<Panel>();

            duiHuan.Show();
        }
        /// <summary>
        /// 跳转到设置界面
        /// </summary>
        public void GoToSheZhi(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);

            if (sheZhi==null)
                sheZhi = wode.transform.Find("p_个人设置").GetComponent<Panel>();
            sheZhi.Show();

        }
        /// <summary>
        /// 跳转到修改密码界面
        /// </summary>
        public void GoToXiuGaiMiMa(IRefresh lastPanel)
        {

            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (xiuGaiMima == null)
                xiuGaiMima = wode.transform.Find("p_修改密码").GetComponent<Panel>();
            xiuGaiMima.Show();
        }

        public void GoToXiuGaiXinXi(IRefresh lastPanel) {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);

            if (xiuGaiXinXi == null)
                xiuGaiXinXi = wode.transform.Find("p_修改信息").GetComponent<Panel>();
            xiuGaiXinXi.Show();
        }

        /// <summary>
        /// 跳转到等级界面
        /// </summary>
        public void GoToLevelPanel(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (level == null)
                level = wode.transform.Find("p_我的等级").GetComponent<Panel>();
            level.Show();

        }
        /// <summary>
        /// 跳转到我的工匠卡界面
        /// </summary>
        public void GoToCraftsmanPanel(IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (craftsman == null)
                craftsman = wode.transform.Find("p_CraftsmanCard").GetComponent<Panel>();
            craftsman.Show();

        }
        #endregion















        #region 论坛模块
        /// <summary>
        /// 论坛动态界面
        /// </summary>
        private ForumDongTaiPanel dongTai;
        /// <summary>
        /// 搜索结果
        /// </summary>
        private ForumSearchPanel souSuoJieGuo;
        private ForumDetailPanel tieZiXiangQing;
        private ForumEditorPanel tieZiBianJi;
        private ForumMainPanel lunTanMain;

        GameObject forum;
        public void GoToForumMain()
        {

            panelCache.Clear();
            if (forum == null)
                forum = transform.Find("- forum -").gameObject;
            forum.SetActive(true);
            if (lunTanMain == null)
            {
                lunTanMain= transform.Find("- forum -").Find("p_forum").GetComponent<ForumMainPanel>();

            }
            Transform parent = lunTanMain.transform.parent;
            for(int i = 0; i < parent.childCount; i++)
            {
                parent.GetChild(i).gameObject.SetActive(false);
            }
            lunTanMain.Show();
        }

        /// <summary>
        /// 跳转到动态界面
        /// </summary>
        /// <param name="index"></param>
        public void GoToDongTai(int index,IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (dongTai == null)
            {
                dongTai = transform.Find("- forum -").Find("p_dongtai").GetComponent<ForumDongTaiPanel>();
            }
            dongTai.Show(index);
        }
        /// <summary>
        /// 跳转到帖子搜索界面
        /// </summary>
        /// <param name="index"></param>
        /// <param name="condition"></param>
        public void GoToForumJieGuo(int index, string[] condition ,IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (souSuoJieGuo == null)
            {
                souSuoJieGuo = transform.Find("- forum -").Find("p_search").GetComponent<ForumSearchPanel>();

            }
            souSuoJieGuo.Show(index, condition);

        }
        /// <summary>
        /// 跳转到帖子详情
        /// </summary>
        /// <param name="index"></param>
        /// <param name="condition"></param>
        public void GoToForumDetail(Forum forum)
        {
            if (tieZiXiangQing == null)
            {
                tieZiXiangQing = transform.Find("- forum -").Find("p_lanquan").GetComponent<ForumDetailPanel>();
            }
            transform.Find("- forum -").gameObject.SetActive(true);
            tieZiXiangQing.Show(forum);
        }
        /// <summary>
        /// 跳转官方到帖子详情
        /// </summary>
        /// <param name="index"></param>
        /// <param name="condition"></param>
        public void GoToForumDetail(OfficalForum forum)
        {
            if (tieZiXiangQing == null)
            {
                tieZiXiangQing = transform.Find("- forum -").Find("p_lanquan").GetComponent<ForumDetailPanel>();
            }

            tieZiXiangQing.Show(forum);
        }
        /// <summary>
        /// 跳转到帖子编辑界面
        /// </summary>
        /// <param name="index"></param>
        /// <param name="condition"></param>
        public void GoToForumEditor( IRefresh lastPanel)
        {
            lastPanel.ClearCache();
            panelCache.Push(lastPanel);
            if (tieZiBianJi == null)
            {
                tieZiBianJi = transform.Find("- forum -").Find("p_editor").GetComponent<ForumEditorPanel>();
            }

            tieZiBianJi.Show();
        }
        #endregion

   
        


        /// <summary>
        /// 提示面板
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        /// <param name="callBack"></param>
        public void ShowLoggerMsg(string msg, string title="提示",System.Action callBack=null)
        {
            if(callBack==null)
           Util.ShowAlert(UtilManager.getInstance.messageAlert, title, msg);
            else
            {
                Util.ShowAlert(UtilManager.getInstance.messageAlert, title, msg,delegate {

                    callBack();
                    UtilManager.getInstance.messageAlert.SetActive(false);
                });
            }

        }

        public void ShowCommitMsg(string msg, string title = "提示", System.Action callBack = null)
        {
            if (callBack == null)
                Util.ShowCommit(UtilManager.getInstance.messageCommit, title, msg);
            else
            {
                Util.ShowCommit(UtilManager.getInstance.messageCommit, title, msg, delegate {

                    callBack();
                    UtilManager.getInstance.messageCommit.SetActive(false);
                });
            }

        }


        public void LoadImageFromUrl(string url,int width,int height,out bool check,Func<RawImage>  callBack=null)
        {
            if (CheckImageType(url))
            {
                check = true;
                //  Debug.Log("Url:"+url);
                //实际情况要用param作为图片类型的标识符进行区分情况加载图片
                // StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(url, width, height, callBack));
                //单个加载
                // StartCoroutine(LoadImage(url, width, height, callBack));

                //序列加载
                PushLoadTask(url, width, height, callBack);
            }
            else
            {
                check = false;
            }
        }
        public void LoadImageFromUrl(string url, int width, int height, Func<RawImage> callBack = null,Func<LayoutElement> element = null)
        {
            if (CheckImageType(url))
            {

                //  Debug.Log("Url:"+url);
                //实际情况要用param作为图片类型的标识符进行区分情况加载图片
                // StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(url, width, height, callBack));
                //单个加载
                // StartCoroutine(LoadImage(url, width, height, callBack));

                //序列加载
               // Debug.Log(sizeOffect + "------");
                PushLoadTask(url, width, height, callBack,element);
            }
            
        }

        private IEnumerator LoadImage(string _url, int _imageWidth, int _imageHeight, Func<RawImage> callBack = null,Func<LayoutElement> _element = null)
        {
            
            //if (imageCache.ContainsKey(_url + "_" + _imageWidth + "_" + _imageHeight))
            //{
            //    //Debug.Log(Application.persistentDataPath);
            //    callBack().texture = imageCache[_url + "_" + _imageWidth + "_" + _imageHeight];
            //}
            //else
            {
               // Debug.Log(_sizeOffect);
                if(_element == null)
                {
               using (WWW www = new WWW(_url+ "?imageView2/2/w/"+ _imageWidth + "/h/"+ _imageHeight))
                //using (WWW www = new WWW(_url + "?imageslim"))
                {
                    yield return www;
                    if (www.isDone)
                    {
                      //  Debug.Log(www.texture.height);
                        Texture2D tex2d = new Texture2D(_imageWidth, _imageHeight, TextureFormat.Alpha8, false);

                        //if (!imageCache.ContainsKey(_url + "_" + _imageWidth + "_" + _imageHeight))
                        //{
                        // imageCache.Add(_url + "_" + _imageWidth + "_" + _imageHeight, tex2d);
                        //}
                        www.LoadImageIntoTexture(tex2d);
                        //  Debug.Log(callBack().name);
                        callBack().texture = tex2d;
                            tex2d = null;
                        UtilManager.getInstance.loadingPanel.SetActive(false);
                        Resources.UnloadUnusedAssets();
                        GC.Collect();
                    }

                }
                }
                else
                {
                    using (WWW www = new WWW(_url + "?imageView2/2/w/" + _imageWidth))
                    //using (WWW www = new WWW(_url + "?imageslim"))
                    {
                        yield return www;
                        if (www.isDone)
                        {
                            //Debug.Log(www.texture.height + "height");
                            //Debug.Log(www.texture.width + "width");
                            float currentHeight = _imageWidth * ((float)www.texture.height /(float)www.texture.width);
                            //Debug.Log(currentHeight + "currentHeight");
                            Texture2D tex2d = new Texture2D(_imageWidth, (int)currentHeight, TextureFormat.Alpha8, false);

                            //if (!imageCache.ContainsKey(_url + "_" + _imageWidth + "_" + _imageHeight))
                            //{
                            // imageCache.Add(_url + "_" + _imageWidth + "_" + _imageHeight, tex2d);
                            //}
                            www.LoadImageIntoTexture(tex2d);
                            //  Debug.Log(callBack().name);
                            callBack().texture = tex2d;
                            tex2d = null;
                            _element().preferredHeight = (int)currentHeight;
                            if(_element().GetComponentInChildren<ToBigImageHelper>() != null)
                            {
                                ToBigImageHelper toBig = _element().GetComponentInChildren<ToBigImageHelper>();

                                toBig.layoutHeight = _element().preferredHeight;
                            }
                            //Debug.Log(tex2d.height);
                            UtilManager.getInstance.loadingPanel.SetActive(false);
                            Resources.UnloadUnusedAssets();
                            GC.Collect();
                        }

                    }
                }
            }
          
        }



        IEnumerator LoadImageRunning()
        {
            while (true)
            {
                yield return new WaitUntil(delegate { return urls.Count>0; });
                if(sizeElement.Count > 0)
                    yield return LoadImage(urls.Dequeue(), width.Dequeue(), height.Dequeue(), funcs.Dequeue(),sizeElement.Dequeue());
                else
                    yield return LoadImage(urls.Dequeue(), width.Dequeue(), height.Dequeue(), funcs.Dequeue());

            }
        }
        /// <summary>
        /// 清空图片缓存
        /// </summary>
        public void RefrehImageCache()
        {
            StopAllCoroutines();
            urls.Clear();
            width.Clear();
            height.Clear();
            sizeElement.Clear();
            funcs.Clear();
            imageCache.Clear();
            if(gameObject.activeInHierarchy)
            StartCoroutine(LoadImageRunning());

            Resources.UnloadUnusedAssets();
            GC.Collect();

        }
        /// <summary>
        /// 记录打开过的界面
        /// </summary>
        private Stack<IRefresh> panelCache = new Stack<IRefresh>();


        Dictionary<string, Texture> imageCache = new Dictionary<string, Texture>();
        private Queue<Func< RawImage>> funcs = new Queue<Func<RawImage>>();
        private Queue<string> urls = new Queue<string>();
        private Queue<int> width = new Queue<int>();
        private Queue<int> height = new Queue<int>();
        private Queue<Func<LayoutElement>> sizeElement = new Queue<Func<LayoutElement>>();
        /// <summary>
        /// 加入待加载的任务
        /// </summary>
        /// <param name="url"></param>
        /// <param name="backImage"></param>
        public void PushLoadTask(string url,int _width,int _height,Func<RawImage> backImage,Func<LayoutElement> _element = null)
        {
            urls.Enqueue(url);
            funcs.Enqueue(backImage);
            width.Enqueue(_width);
            height.Enqueue(_height);
           // Debug.Log(_sizeOffect);
            if(_element != null)
                sizeElement.Enqueue(_element);
        }

      


       public bool CheckImageType(string url)
        {
            for(int i=0;i<types.Count;i++)
            {
                if (url.EndsWith(types[i]))
                {
                    return true;
                }

            }
            return false;
        }

        /// <summary>
        /// 图片过滤类型
        /// </summary>
        List<string> types = new List<string>() { ".png", ".jpg" };

    }

}
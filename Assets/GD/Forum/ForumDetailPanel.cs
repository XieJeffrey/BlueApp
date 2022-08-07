using LM_Workspace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;

namespace GD
{
    /// <summary>
    /// 帖子详情信息
    /// </summary>
    public class ForumDetailPanel : Panel {


        private UIEventListener back;
        private UIEventListener send;
        private InputField sendMsg;
        private Toggle shoucang;
        private PingLunItem prefab;
        private Text userName;
        private Text context;
        private Text creatTime;
        private RawImage header;
        private GameObject officalHeader;
        private RawImage[] uploadImages = new RawImage[6];
        private List<PingLunItem> currentItem=new List<PingLunItem>();

        private ScrollItem st;

        int page = 1;
        int Page
        {
            get
            {
                return page;
            }
            set
            {
                if (value < 1)
                {
                    page = 1;
                }else
                {
                     page = value;
                }
            }
        }
        int pageCount = 5;


        void Start()
        {
            back = transform.Find("topbar").Find("btn_back").GetComponent<UIEventListener>();
            back.onClick = delegate { Hide(); };
            send = transform.Find("bottom").Find("btn_send").GetComponent<UIEventListener>();
            send.onClick = delegate { Send(); };

            st = transform.Find("center").Find("dragPanel").GetComponent<ScrollItem>();
            st.onEndDrag = OnEndDrag;
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetOfficialForumDetail, OnGetOfficialDetail);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.GetDetail, OnGetDetail);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.CollectForum, OnCollectForum);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.ReplyForum, OnReplyForum);
            EventManager.instance.RegisterEvent(XMWorkspace.Event.ReplyOfficialForum, OnReplyOfficialForum);
            
        }

        




        /// <summary>
        /// 刷新按钮
        /// </summary>
        /// <param name="obj"></param>
        private void OnEndDrag(Vector2 obj)
        {
            //刷新
            if (obj.y < -100)
            {
                page = 1;
                if (isOffical)
                {
                    GetDetail(officalDetail.id);
                }
                else
                {
                    GetDetail(detail.id);
                }
            }
            else if(obj.y > 100)
            {
                page++;
                CreatCommentList();
            }
        }

        /// <summary>
        /// 回帖结果
        /// </summary>
        /// <param name="obj"></param>
        private void OnReplyForum(object[] obj)
        {
            if ((bool)obj[0])
            {
                page = 1;
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetDetail(detail.id);

            }
            else
            {
                //回复失败
                UIManager.instance.ShowLoggerMsg("回帖失败");


            }
        }
        private void OnReplyOfficialForum(object[] obj)
        {
            if ((bool)obj[0])
            {
                page = 1;
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetOfficialDetail(officalDetail.id);

            }
            else
            {
                //回复失败

                UIManager.instance.ShowLoggerMsg("回帖失败");

            }
        }
        /// <summary>
        /// 收藏取消收藏回调
        /// </summary>
        /// <param name="obj"></param>
        private void OnCollectForum(object[] obj)
        {
            if ((bool)obj[0])
            {
                    detail.isCollect = (int)obj[1] == 1;
                if (detail.isCollect)
                {
                    UIManager.instance.ShowLoggerMsg("收藏成功");
                }else
                {

                    UIManager.instance.ShowLoggerMsg("取消收藏成功");
                }

            }else
            {
                //收藏失败
                shoucang.isOn = detail.isCollect;
                if (detail.isCollect)
                {
                    UIManager.instance.ShowLoggerMsg("取消收藏失败");
                }
                else
                {

                    UIManager.instance.ShowLoggerMsg("收藏失败");
                }

            }
            
        }

        /// <summary>
        /// 评论
        /// </summary>
        private void Send()
        {
            if (isOffical)
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().ReplyOfficialForum(officalDetail.id, sendMsg.text);

            }
            else
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().ReplyForum(detail.id, sendMsg.text);

            }
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            for(int i=0;i< uploadImages.Length; i++)
            {
                uploadImages[i].texture = null;
            }
            for (int i = 0; i < currentItem.Count; i++)
            {
              Destroy(  currentItem[i].gameObject);
            }
            UIManager.instance.RefrehImageCache();
            currentShowCount = 0;
            currentItem.Clear();
        }

        public override void Show()
        {



            
        }
        /// <summary>
        /// 帖子的详情信息
        /// </summary>
        Forum detail;
        public  void  Show(Forum data)
        {
            gameObject.SetActive(true);
            isOffical = false;
            GetCompent();
            header.gameObject.SetActive(true);
            officalHeader.gameObject.SetActive(false);
               page = 1;
            GetDetail(data.id);
        }
        /// <summary>
        /// 帖子的详情信息
        /// </summary>
        OfficalForum officalDetail;
        public void Show(OfficalForum data)
        {
            UtilManager.getInstance.forumGo.SetActive(true);
           // Debug.Log(data.id);
            gameObject.SetActive(true);
          //  UIManager.instance.Clear();
            isOffical = true;
            GetCompent();
            header.gameObject.SetActive(false);
            officalHeader.gameObject.SetActive(true);
            page = 1;
            GetDetail(data.id);
        }

        void GetDetail(int forumId)
        {
            if (isOffical)
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetOfficialDetail(forumId);
            }
            else
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().GetDetail(forumId);
            }
           
        }
        private bool isOffical;
        /// <summary>
        /// 获取帖子详情回调
        /// </summary>
        /// <param name="obj"></param>
        private void OnGetDetail(object[] obj)
        {
            if ((bool)obj[0])
            {
                if (page == 1)
                {
                    detail = (Forum)obj[1];
                    HideAllItem();
                    userName.text = detail.userName;
                    context.text = detail.content;
                    creatTime.text = detail.create_time;
                     shoucang.gameObject.SetActive(true);
                    shoucang.isOn = detail.isCollect;
                    sendMsg.text = "";

                    List<string> tiezitupian = detail.uploadImages;
                    for (int i = 0; i < tiezitupian.Count; i++)
                    {
                        string url = tiezitupian[i];
                        if (UIManager.instance.CheckImageType(url))
                        {
                            // Debug.Log("哈哈哈哈哈哈");
                            // float showFloat = 1f;

                            RawImage show = uploadImages[i];
                            show.gameObject.SetActive(true);

                            UIManager.instance.LoadImageFromUrl(url, 700, 0, delegate {
                                return show;
                            }, delegate {
                                return show.GetComponent<LayoutElement>();

                            });

                        }
                        ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
                        //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(url, 700, 400, delegate {
                        //    return show;
                        //}));
                    }
                    bool checkType;

                    UIManager.instance.LoadImageFromUrl(detail.userAvatar, 94, 94,out checkType, delegate {
                        header.gameObject.SetActive(true);
                        return header;
                    });
                    if (!checkType)
                    {
                      
                        header.gameObject.SetActive(false);
                    }
                }
                CreatCommentList();

                ////实际情况要用param作为图片类型的标识符进行区分情况加载图片
                //StartCoroutine(PostImageManager.getInstance.LoadImageFromUrl(detail.userAvatar, 300, 500, delegate {
                //    return header;
                //}));


            }
        }

        private void OnGetOfficialDetail(object[] obj)
        {
            if ((bool)obj[0])
            {
                if (page == 1)
                {
                    officalDetail = (OfficalForum)obj[1];
                    HideAllItem();
                    userName.text = "纯净蓝官方发布";
                    context.text = officalDetail.content;
                    creatTime.text = officalDetail.createTime;
                    // shoucang.isOn = shoucang;
                    shoucang.gameObject.SetActive(false);
                    sendMsg.text = "";

                    string url = officalDetail.thumb;
                    if (UIManager.instance.CheckImageType(url))
                    {
                       
                        RawImage show = uploadImages[0];
                       
                        show.gameObject.SetActive(true);
                        //UIManager.instance.LoadImageFromUrl(url, 700, 0, delegate {
                        //    return show;
                        //});
                        UIManager.instance.LoadImageFromUrl(url, 700, 0, delegate {
                            return show;
                        }, delegate {
                            return show.GetComponent<LayoutElement>();

                        });
                       
                    }
                        
                }
                CreatCommentList();

            }
        }

        int currentShowCount=0;
        /// <summary>
        /// 创建评论列表
        /// </summary>
        void CreatCommentList()
        {
            List<Comment> commentList;
            if (isOffical)
            {
                commentList = officalDetail.commentList;
            }
            else
            {
                commentList = detail.commentList;

            }
            ///当前已经存在的个数
            int currentCount = currentItem.Count;
            //回复的个数
            int total = commentList.Count;
            //当前需要显示的个数
            int showCount = page * pageCount;
            showCount = Mathf.Min(showCount, total);
            for (int i = currentShowCount; i < showCount; i++)
            {
                PingLunItem temp;
                if (i < currentCount)
                {
                    temp = currentItem[i];
                }
                else
                {
                    temp = Instantiate<PingLunItem>(prefab, prefab.transform.parent);
                    currentItem.Add(temp);

                }

                  temp.SetItemData(commentList[i]);
                 // temp.gameObject.SetActive(true);
                  currentShowCount++;
            }
          //  UIManager.instance.StartLoad();


        }
        void GetCompent()
        {
            if (shoucang == null)
            {
                shoucang = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("topItem").Find("iconStar").GetComponent<Toggle>();
                shoucang.onValueChanged.AddListener(OnShouChangChange);
            }

            if (prefab==null)
            {
                prefab = transform.Find("center").Find("dragPanel").Find("content").Find("forumItem").GetComponent<PingLunItem>();
            }
            if (userName == null)
            {
                userName = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("topItem").Find("userName").GetComponent<Text>();

            }
            if (context == null)
            {
                context = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("label").GetComponent<Text>();

            }
            if (creatTime == null)
            {
                creatTime = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("topItem").Find("dateTimeLabel").GetComponent<Text>();

            }
            if (header == null)
            {
                header = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("topItem").Find("mask").Find("iconHead").GetComponent<RawImage>();
            }
            if (officalHeader == null)
            {
                officalHeader = transform.Find("center").Find("dragPanel").Find("content").Find("topArea").Find("topItem").Find("officalIconHead").gameObject;

            }
            if (sendMsg == null)
            {
                sendMsg = transform.Find("bottom").Find("InputField").GetComponent<InputField>();
            }
            sendMsg.text = "";
            if (uploadImages[0] == null)
            {
                uploadImages[0]= transform.Find("center").Find("dragPanel").Find("content").Find("imageItem1").GetComponent<RawImage>();

            }
            if (uploadImages[1] == null)
            {
                uploadImages[1] = transform.Find("center").Find("dragPanel").Find("content").Find("imageItem2").GetComponent<RawImage>();
            }
            if (uploadImages[2] == null)
            {
                uploadImages[2] = transform.Find("center").Find("dragPanel").Find("content").Find("imageItem3").GetComponent<RawImage>();
            }
            if (uploadImages[3] == null)
            {
                uploadImages[3] = transform.Find("center").Find("dragPanel").Find("content").Find("imageItem4").GetComponent<RawImage>();
            }
            if (uploadImages[4] == null)
            {
                uploadImages[4] = transform.Find("center").Find("dragPanel").Find("content").Find("imageItem5").GetComponent<RawImage>();
            }
            if (uploadImages[5] == null)
            {
                uploadImages[5] = transform.Find("center").Find("dragPanel").Find("content").Find("imageItem6").GetComponent<RawImage>();
            }
            for(int i=0;i< uploadImages.Length; i++)
            {
                uploadImages[i].gameObject.SetActive(false);
            }

        }
        /// <summary>
        /// 当收藏状态变化的时候
        /// </summary>
        /// <param name="arg0"></param>
        private void OnShouChangChange(bool arg0)
        {

          //  Debug.Log(arg0);
            if (arg0 != detail.isCollect)
            {
                GameObject.Find("Camera").GetComponent<InterfaceManager>().CollectForum(detail.id);

            }
        }

        /// <summary>
        /// 关闭当前显示的回帖
        /// </summary>
        void HideAllItem()
        {
            for(int i = 0; i < currentShowCount; i++)
            {
                currentItem[i].gameObject.SetActive(false);
            }
            currentShowCount = 0;
        }
    }


}
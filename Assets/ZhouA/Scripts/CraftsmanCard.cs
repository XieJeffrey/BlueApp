using GD;
using LitJson;
using LM_Workspace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XMWorkspace;

public class CraftsmanCardInfo {
    public int itemId;
    public string id;
    public string card_no;//工匠卡编号
    public string user_id;//用户id；
    public string name;//用户姓名
    public string phone;//用户电话
    public string order_id;//来源订单编号
    public string write_off_remark;//核销备注
    public string status;//是否兑换
    public string area_title;//区域
    public string store_title;//营销点
    public string store_phone;//营销点电话
    public string create_time;//获取时间
    public string write_off_time;//使用时间
    public long deadline_time;//过期时间
}
public class CraftsmanCard : Panel {
    private InterfaceManager interM;
    public List<CraftsmanCardInfo> craftsmanCardInfoList;

    private GameObject conversion;
    private UIEventListener backBtn_conversion;
    private RectTransform itemLayout;
    private GameObject itemPrefab;
    private UIEventListener allBtn;
    private UIEventListener notConversionBtn;
    private UIEventListener conversionBtn;

    private GameObject myCraftsmanCard;
    private UIEventListener backBtn_MyCraftsmanCard;
    private UIEventListener btn_more;
    public int itemId;
    public int tempItemId;

    private GameObject qRCode;
    private UIEventListener backBtn_QRCode;

    private GameObject infoCard;


    private bool isFirst = true;
    // Start is called before the first frame update
    void Awake() {
        itemId = 0;

        EventManager.instance.RegisterEvent(XMWorkspace.Event.GetListByUser, OnGetListByUser);
        //UserData.instance.token = "49n2v5rEtEdgxbe8";

        conversion = transform.Find("Conversion").gameObject;
        backBtn_conversion = conversion.transform.Find("header/btn_back").GetComponent<UIEventListener>();
        itemLayout = conversion.transform.Find("Scroll/Layout").GetComponent<RectTransform>();
        allBtn = conversion.transform.Find("MenuBar/AllCardBtn").GetComponent<UIEventListener>();
        notConversionBtn = conversion.transform.Find("MenuBar/NotConversionBtn").GetComponent<UIEventListener>();
        conversionBtn = conversion.transform.Find("MenuBar/ConversionBtn").GetComponent<UIEventListener>();

        myCraftsmanCard = transform.Find("MyCraftsmanCard").gameObject;
        backBtn_MyCraftsmanCard = myCraftsmanCard.transform.Find("content/header/btn_back").GetComponent<UIEventListener>();
        btn_more = myCraftsmanCard.transform.Find("content/btn_more").GetComponent<UIEventListener>();

        qRCode = transform.Find("QRCode").gameObject;
        backBtn_QRCode = qRCode.transform.Find("content/header/btn_back").GetComponent<UIEventListener>();

        itemPrefab = transform.Find("Prefab/CardItem").gameObject;

        infoCard = myCraftsmanCard.transform.Find("content/doc/Info_Card").gameObject;

        backBtn_conversion.onClick = delegate {
            DestroyItem(() => {

                allBtn.GetComponent<Image>().raycastTarget = true;
                Hide();
            });
        };

        backBtn_MyCraftsmanCard.onClick = delegate {
            myCraftsmanCard.SetActive(false);
            conversion.SetActive(true);
        };
        btn_more.onClick = delegate {
            myCraftsmanCard.SetActive(false);
            qRCode.SetActive(true);
            OnBtnMoreClick();
        };


        backBtn_QRCode.onClick = delegate {
            myCraftsmanCard.SetActive(true);
            qRCode.SetActive(false);
        };

        allBtn.onClick = delegate { OnAllBtnClick(); };
        notConversionBtn.onClick = delegate { OnNotConversionBtnClick(); };
        conversionBtn.onClick = delegate { OnConversionBtnClick(); };
        //OnAllBtnClick();
    }
    private void OnEnable() {
        interM = GameObject.Find("Camera").GetComponent<InterfaceManager>();
        interM.GetListByUser(1, 50, 1);
        allBtn.transform.Find("icon").gameObject.SetActive(true);
        notConversionBtn.transform.Find("icon").gameObject.SetActive(false);
        conversionBtn.transform.Find("icon").gameObject.SetActive(false);
        //if (!isFirst)
        //{
        //   // OnAllBtnClick();
        //    allBtn.GetComponent<Image>().raycastTarget = true;
        //}
        //else
        //{
        //    isFirst = false;
        //}
    }

    private void OnGetListByUser(object[] obj) {
        if ((bool)obj[0]) {
            JsonData result = (JsonData)obj[1];
            //Debug.Log(result["total"].ToString());
            result = result["list"];
            craftsmanCardInfoList = new List<CraftsmanCardInfo>();
            for (int i = 0; i < result.Count; i++) {
                CraftsmanCardInfo info = new CraftsmanCardInfo();
                info.itemId = itemId;
                itemId++;
                info.id = result[i]["id"].ToString();
                info.card_no = result[i]["card_no"].ToString();
                info.user_id = result[i]["user_id"].ToString();
                info.name = result[i]["name"].ToString();
                info.phone = result[i]["phone"].ToString();
                info.order_id = result[i]["order_id"].ToString();
                info.write_off_remark = result[i]["write_off_remark"].ToString();
                info.status = result[i]["status"].ToString();
                info.area_title = result[i]["area_title"].ToString();
                info.store_title = result[i]["store_title"].ToString();
                info.store_phone = result[i]["store_phone"].ToString();
                info.create_time = result[i]["create_time"].ToString();
                info.write_off_time = result[i]["write_off_time"].ToString();
                info.deadline_time = long.Parse(result[i]["deadline_time"].ToString());
                //info.card_remark = result[i]["card_remark"].ToString();
                craftsmanCardInfoList.Add(info);
                //if (i == result.Count - 1) 
            }
            Debug.Log("hahha");
            Init();
        }
    }
    private void Init() {
        //Debug.Log(craftsmanCardInfoList.Count);
        for (int i = 0; i < craftsmanCardInfoList.Count; i++) {
            GameObject item = Instantiate(itemPrefab, itemLayout);
            item.transform.Find("Order/Label").GetComponent<Text>().text = (i + 1).ToString();
            item.transform.Find("LabelLyaout/OrderNum/OrderId").GetComponent<Text>().text = craftsmanCardInfoList[i].order_id;
            item.transform.Find("LabelLyaout/CardNum/CardNum").GetComponent<Text>().text = craftsmanCardInfoList[i].card_no;//工匠卡编
            item.transform.Find("LabelLyaout/CreateTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].create_time;//获取
            item.transform.Find("LabelLyaout/WriteOffTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].write_off_time;//
            //过期时间
            System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            System.DateTime deadlineTime = startTime.AddSeconds(craftsmanCardInfoList[i].deadline_time);
            item.transform.Find("LabelLyaout/DeadlineTime/Time").GetComponent<Text>().text = deadlineTime.ToString("yyyy-MM-dd HH:mm:ss");

            string status = "";
            if (craftsmanCardInfoList[i].status == "0") {
                item.transform.Find("Circle/TrueIcon").gameObject.SetActive(false);
                item.transform.Find("Circle/ShortLine").gameObject.SetActive(true);
                item.transform.Find("Circle/Label").GetComponent<Text>().text = "未兑换";
                status = "0";
            }
            else {
                item.transform.Find("Circle/TrueIcon").gameObject.SetActive(true);
                item.transform.Find("Circle/ShortLine").gameObject.SetActive(false);
                item.transform.Find("Circle/Label").GetComponent<Text>().text = "已兑换";
            }
            //Debug.Log(craftsmanCardInfoList[i].itemId);
            item.GetComponent<CardItem>().itemId = craftsmanCardInfoList[i].itemId;
            item.GetComponent<UIEventListener>().onClick = delegate { OnItemClick(item.GetComponent<CardItem>().itemId, status); };
        }
    }
    private void OnAllBtnClick() {
        //isFirst = true;
        if (allBtn.GetComponent<Image>().raycastTarget) {
            DestroyItem();
        }
        allBtn.GetComponent<Image>().raycastTarget = false;
        notConversionBtn.GetComponent<Image>().raycastTarget = true;
        conversionBtn.GetComponent<Image>().raycastTarget = true;
        allBtn.transform.Find("icon").gameObject.SetActive(true);
        notConversionBtn.transform.Find("icon").gameObject.SetActive(false);
        conversionBtn.transform.Find("icon").gameObject.SetActive(false);
        Debug.Log("OnAllBtnClick");
        Init();
    }
    private void OnNotConversionBtnClick() {
        //isFirst = true;
        if (notConversionBtn.GetComponent<Image>().raycastTarget) {
            DestroyItem();
        }
        allBtn.GetComponent<Image>().raycastTarget = true;
        notConversionBtn.GetComponent<Image>().raycastTarget = false;
        conversionBtn.GetComponent<Image>().raycastTarget = true;
        allBtn.transform.Find("icon").gameObject.SetActive(false);
        notConversionBtn.transform.Find("icon").gameObject.SetActive(true);
        conversionBtn.transform.Find("icon").gameObject.SetActive(false);

        int idx = 0;
        for (int i = 0; i < craftsmanCardInfoList.Count; i++) {
            if (craftsmanCardInfoList[i].status == "0") {
                GameObject item = Instantiate(itemPrefab, itemLayout, false);
                item.transform.Find("Order/Label").GetComponent<Text>().text = (++idx).ToString();
                item.transform.Find("LabelLyaout/OrderNum/OrderId").GetComponent<Text>().text = craftsmanCardInfoList[i].order_id;
                item.transform.Find("LabelLyaout/CardNum/CardNum").GetComponent<Text>().text = craftsmanCardInfoList[i].card_no;//工匠卡编
                item.transform.Find("LabelLyaout/CreateTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].create_time;//获取
                item.transform.Find("LabelLyaout/WriteOffTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].write_off_time;//

                //过期时间
                System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                System.DateTime deadlineTime = startTime.AddSeconds(craftsmanCardInfoList[i].deadline_time);
                item.transform.Find("LabelLyaout/DeadlineTime/Time").GetComponent<Text>().text = deadlineTime.ToString("yyyy-MM-dd HH:mm:ss");

                item.transform.Find("Circle/TrueIcon").gameObject.SetActive(false);
                item.transform.Find("Circle/ShortLine").gameObject.SetActive(true);
                item.transform.Find("Circle/Label").GetComponent<Text>().text = "未兑换";
                item.GetComponent<CardItem>().itemId = craftsmanCardInfoList[i].itemId;
                item.GetComponent<UIEventListener>().onClick = delegate { OnItemClick(item.GetComponent<CardItem>().itemId, "0"); };
            }
        }
    }
    private void OnConversionBtnClick() {
        //isFirst = true;
        if (conversionBtn.GetComponent<Image>().raycastTarget) {
            DestroyItem();
        }
        allBtn.GetComponent<Image>().raycastTarget = true;
        notConversionBtn.GetComponent<Image>().raycastTarget = true;
        conversionBtn.GetComponent<Image>().raycastTarget = false;
        allBtn.transform.Find("icon").gameObject.SetActive(false);
        notConversionBtn.transform.Find("icon").gameObject.SetActive(false);
        conversionBtn.transform.Find("icon").gameObject.SetActive(true);

        int idx = 0;
        for (int i = 0; i < craftsmanCardInfoList.Count; i++) {
            if (craftsmanCardInfoList[i].status != "0") {
                GameObject item = Instantiate(itemPrefab, itemLayout, false);
                item.transform.Find("Order/Label").GetComponent<Text>().text = (++idx).ToString();
                item.transform.Find("LabelLyaout/OrderNum/OrderId").GetComponent<Text>().text = craftsmanCardInfoList[i].order_id;
                item.transform.Find("LabelLyaout/CardNum/CardNum").GetComponent<Text>().text = craftsmanCardInfoList[i].card_no;//工匠卡编
                item.transform.Find("LabelLyaout/CreateTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].create_time;//获取
                item.transform.Find("LabelLyaout/WriteOffTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[i].write_off_time;//

                //过期时间
                System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                System.DateTime deadlineTime = startTime.AddSeconds(craftsmanCardInfoList[i].deadline_time);
                item.transform.Find("LabelLyaout/DeadlineTime/Time").GetComponent<Text>().text = deadlineTime.ToString("yyyy-MM-dd HH:mm:ss");

                item.transform.Find("Circle/TrueIcon").gameObject.SetActive(true);
                item.transform.Find("Circle/ShortLine").gameObject.SetActive(false);
                item.transform.Find("Circle/Label").GetComponent<Text>().text = "已兑换";

                item.GetComponent<CardItem>().itemId = craftsmanCardInfoList[i].itemId;
                item.GetComponent<UIEventListener>().onClick = delegate { OnItemClick(item.GetComponent<CardItem>().itemId, "1"); };
            }
        }
    }
    private void DestroyItem(System.Action _callBack = null) {
        for (int i = itemLayout.childCount - 1; i >= 0; i--)
            DestroyImmediate(itemLayout.GetChild(i).gameObject);
        itemId = 0;
        if (_callBack != null)
            _callBack();
    }

    private void OnItemClick(int id, string status) {
        //isFirst = true;
        if (status == "0") {
            btn_more.gameObject.SetActive(true);
            infoCard.SetActive(false);
        }
        else {
            btn_more.gameObject.SetActive(false);
            infoCard.SetActive(true);
        }
        //Debug.Log(craftsmanCardInfoList[id].write_off_remark);
        tempItemId = id;
        myCraftsmanCard.SetActive(true);
        conversion.SetActive(false);
        Transform cardItem = myCraftsmanCard.transform.Find("content/CardItem");
        System.DateTime startTime = System.TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
        System.DateTime deadlineTime = startTime.AddSeconds(craftsmanCardInfoList[tempItemId].deadline_time);

        cardItem.Find("LabelLyaout/CreateTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].create_time;
        cardItem.Find("LabelLyaout/WriteOffTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].write_off_time;
        cardItem.Find("LabelLyaout/CardNum/CardNum").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].card_no;
        cardItem.Find("LabelLyaout/OrderNum/OrderId").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].order_id;

        Transform docLayout = myCraftsmanCard.transform.Find("content/doc/layout");
        docLayout.Find("item/Label").GetComponent<Text>().text = "用户姓名：" + craftsmanCardInfoList[id].name;
        docLayout.Find("item_1/Label").GetComponent<Text>().text = "用户手机号：" + craftsmanCardInfoList[id].phone;
        docLayout.Find("item_2/leftlabel").GetComponent<Text>().text = "营销点名称：" + craftsmanCardInfoList[id].store_title;
        docLayout.Find("item_2/rightLabel").GetComponent<Text>().text = "营销点手机号：" + craftsmanCardInfoList[id].store_phone;
        docLayout.Find("item_3/leftlabel").GetComponent<Text>().text = "获取时间：" + craftsmanCardInfoList[id].create_time;    
        docLayout.Find("item_3/rightLabel").GetComponent<Text>().text = "过期时间：" + deadlineTime.ToString("yyyy-MM-dd HH:mm:ss");
        docLayout.Find("item_4/leftlabel").GetComponent<Text>().text = "所在区域：" + craftsmanCardInfoList[id].area_title;      
        docLayout.Find("item_4/rightLabel").GetComponent<Text>().text = "使用时间：" + craftsmanCardInfoList[id].write_off_time;

        docLayout.Find("item_5/Label").GetComponent<Text>().text = "核销备注：" + craftsmanCardInfoList[id].write_off_remark;

        infoCard.transform.Find("background_beizhu/item/Label").GetComponent<Text>().text = UtilManager.getInstance.cardRemark;

    }
    private void OnBtnMoreClick() {
        Transform cardItem = qRCode.transform.Find("content/CardItem");
        cardItem.Find("LabelLyaout/CreateTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].create_time;
        cardItem.Find("LabelLyaout/WriteOffTime/Time").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].write_off_time;
        cardItem.Find("LabelLyaout/CardNum/CardNum").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].card_no;
        cardItem.Find("LabelLyaout/OrderNum/OrderId").GetComponent<Text>().text = craftsmanCardInfoList[tempItemId].order_id;

        Transform docLayout = qRCode.transform.Find("content/doc/layout");

        EventManager.instance.RegisterEvent(XMWorkspace.Event.GetQrCode, OnGetQrCode);
        interM.GetQrCode(craftsmanCardInfoList[tempItemId].card_no);
    }
    private void OnGetQrCode(object[] obj) {
        Debug.Log(111);
        if ((bool)obj[0]) {
            qRCode.transform.Find("content/doc/RawImage").GetComponent<RawImage>().texture = (Texture2D)obj[1];
            qRCode.transform.Find("content/Info_QR/background_beizhu/item/Label").GetComponent<Text>().text = UtilManager.getInstance.cardRemark;
        }
    }
    public override void Hide() {
        gameObject.SetActive(false);
        UIManager.instance.BackToLastPanel();
    }
    public override void Show() {
        gameObject.SetActive(true);

        //GameObject.Find("Camera").GetComponent<InterfaceManager>().GetAppointmentOrderList(1, 0, 1, 50, "");

    }
}

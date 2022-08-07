using System.Collections;
using System.Collections.Generic;


public class PostTest  {
    /// <summary>
    /// 预约订单id
    /// </summary>
    public string appointment_id;
    /// <summary>
    /// 门牌照片
    /// </summary>
    public List<string> menpaiList = new List<string>();
    /// <summary>
    /// 智能试压仪状态图照片
    /// </summary>
    public List<string> stateList = new List<string>();
    /// <summary>
    /// 厨房照片
    /// </summary>
    public List<string> chufangList = new List<string>();
    /// <summary>
    /// 卫生间照片
    /// </summary>
    public List<string> weishengjianList = new List<string>();
    /// <summary>
    /// 客厅照片
    /// </summary>
    public List<string> ketingList = new List<string>();
    /// <summary>
    /// 过道照片
    /// </summary>
    public List<string> guodaoList = new List<string>();
    /// <summary>
    /// 阳台照片
    /// </summary>
    public List<string> yangtaiList = new List<string>();
    /// <summary>
    /// 其他照片
    /// </summary>
    public List<string> otherList = new List<string>();
    /// <summary>
    /// 凭证照片
    /// </summary>
    public List<string> pingzhengList = new List<string>();
    /// <summary>
    /// 不合格照片
    /// </summary>
    public List<string> buhegeList = new List<string>();
    /// <summary>
    /// 不合格描述
    /// </summary>
    public string buhegeStr;
    /// <summary>
    /// 安装者Id
    /// </summary>
    public string buildId;
    /// <summary>
    /// 安装者姓名
    /// </summary>
    public string buildName;
    /// <summary>
    /// 安装者手机
    /// </summary>
    public string buildPhone;
    /// <summary>
    /// 购买地点
    /// </summary>
    public string testBuyPlace;
    /// <summary>
    /// 房屋类型，1别墅，2商品房，3安置房，4车库
    /// </summary>
    public string testHouseType;
    /// <summary>
    /// 厨房户型
    /// </summary>
    public string testKitchenType;
    /// <summary>
    /// 卫生间户型
    /// </summary>
    public string testToiletType;
    /// <summary>
    /// 开发商
    /// </summary>
    public string testDeveloper;
    /// <summary>
    /// 家装公司
    /// </summary>
    public string testDecoration;
    /// <summary>
    /// 暖通公司
    /// </summary>
    public string testHvac;
    /// <summary>
    /// 空调公司
    /// </summary>
    public string testAir;
    /// <summary>
    /// 产品类型，1纯净蓝，2 3米白色精品家装管，3普通咖喱色PPR，4 Z-HOME PPR，5无忧抗冻管，6SW康居管，7其他
    /// </summary>
    public string testProducyType;
    /// <summary>
    /// 安装长度
    /// </summary>
    public string testLength;
    /// <summary>
    /// 管道铺设方案 1：屋顶；2：地面，3：墙壁
    /// </summary>
    public string testLayingType;
    /// <summary>
    /// 现场管道保护：000，每一位分别表示：现场干净、管道固定、管道无压埋
    /// </summary>
    public string testPipeline;
    /// <summary>
    /// 备注
    /// </summary>
    public string testRemark;
    /// <summary>
    /// 安装规范评定：000，每一位分别表示：水表后安装户内总阀、管道无混装、管材表面无划伤
    /// </summary>
    public string testAssess;
    /// <summary>
    /// 加压1.0兆帕接口渗漏情况：00000，每一位分别表示：热水器、厨房水龙头、淋浴、面盆、坐便器
    /// </summary>
    public string testCompress;
    /// <summary>
    /// 焊接接头点敲击检查渗漏情况：00000，每一位分别表示：内丝弯、直接、90度弯、三通、过桥弯
    /// </summary>
    public string testWeld;
    /// <summary>
    /// 焊接接头点敲击检查渗漏情况：00000，每一位分别表示：内丝弯、直接、90度弯、三通、过桥弯
    /// </summary>
    public string testWeldCheck;
    /// <summary>
    /// 保压开始时间，时间戳，精确到秒
    /// </summary>
    public int testKeepStart;
    /// <summary>
    /// 保压结束时间，时间戳，精确到秒
    /// </summary>
    public int testKeepEnd;
    /// <summary>
    /// 管路运行压力：1，0.3兆帕，2，0.4兆帕，3，0.5兆帕
    /// </summary>
    public int testOperatePressure;
    /// <summary>
    /// 管路检测压力：1，0.8兆帕，2，1.0兆帕，3，1.2兆帕
    /// </summary>
    public int testCheckPressure;
    /// <summary>
    /// 试压员id
    /// </summary>
    public int testUserId;
    /// <summary>
    /// 管道使用注意事项告知：0000，每一位分别表示：防水与油漆、热源连接、保温与防冻、管道保护
    /// </summary>
    public string testNotice;
    /// <summary>
    /// 3：试压合格；4：试压不合格；
    /// </summary>
    public int orderStatus;
    /// <summary>
    /// 工匠卡类型
    /// </summary>
    public string cardType;


}

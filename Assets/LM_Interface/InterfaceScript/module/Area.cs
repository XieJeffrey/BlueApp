using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType { 
    Province=0,
    City=1,
    Area=2,
}

public class Area  {

    /// <summary>
    /// 区域id
    /// </summary>
    public int id;

    /// <summary>
    /// 标题
    /// </summary>
    public string title;

    /// <summary>
    /// 创建时间
    /// </summary>
    public string createTime;

    //父节点
    public int parent = -1;

    //区域所属类型
    public AreaType areaType = AreaType.Area;
}

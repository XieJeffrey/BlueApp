using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LM_Workspace;

public class DateTimeType : MonoBehaviour
{

    /// <summary>
    /// 增加的天数
    /// </summary>
    [System.NonSerialized]
    public int addDay;

    public int dayID;

	private void OnEnable()
	{
        this.GetComponent<UIEventListener>().onClick = delegate {
        
            CreateAppoimentManager.getInstance.SelectDay(dayID,addDay);
            CreateAppoimentManager.getInstance.SelectTimeDate(0);
        };

       // CreateAppoimentManager.getInstance.SelectTimeDate(0);
	}

	//private void Start()
	//{
 //       if(dayID == 0)
 //       {
 //           CreateAppoimentManager.getInstance.SelectDay(dayID, addDay);

 //       }
	//}

}

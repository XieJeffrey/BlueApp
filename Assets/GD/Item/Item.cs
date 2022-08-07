using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GD
{
    /// <summary>
    /// 显示单元的基类
    /// </summary>
    public abstract class Item : MonoBehaviour
    {

        public abstract void SetItemData(object data);
       
    }


}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GD
{
    public interface IRefresh
    {
        /// <summary>
        ///再次加载当前的界面
        /// </summary>
        void Reload();
        /// <summary>
        /// 清理数据
        /// </summary>
        void ClearCache();


    }


}
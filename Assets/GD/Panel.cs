using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GD
{
    /// <summary>
    /// 界面基类
    /// </summary>
    public abstract class Panel : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Hide();
    }
}

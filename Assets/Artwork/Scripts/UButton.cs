using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using SVGImporter;

namespace UFrame
{
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    /* 按钮类型的定义，以此来定义按钮的触发动画状态 */
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    public enum ButtonType
    {
        None,   /* 直接点击，没有任何效果 */
        Normal  /* 基本状态，边背景色 */
    }

    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    /* 按钮样式的数据类型定义 */
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    [System.Serializable]
    public class StyleDataStruck
    {
        // Transition target
        public MaskableGraphic targetGraphic;

        // Transition Type
        public StyleTransitionType transitionType = StyleTransitionType.ColorTint;

        // Color tint
        public Color normalColor;
        public Color stateColor;

        // Swap SVG
        public SVGAsset normalSVG;
        public SVGAsset stateSVG;

        // Swap Sprite
        public Sprite normalSprite;
        public Sprite stateSprite;
    }

    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    /* 按钮样式过渡类型的定义 */
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    public enum StyleTransitionType
    {
        ColorTint,  // 变换颜色
        SwapSVG,    // 变换SVG图形
        SwapSprite  // 变换Sprite图形
    }

    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    /* 点击事件的定义 */
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    [System.Serializable]
    public class ButtonClickEvent : UnityEngine.Events.UnityEvent { }

    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    /* 具体按钮类 */
    /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
    [ExecuteInEditMode]
    public class UButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /* 所有变量 */
        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        // 点击事件的定义
        [SerializeField] public ButtonClickEvent OnClick = new ButtonClickEvent();

        // 按钮类型
        [SerializeField] ButtonType m_buttonType = ButtonType.None;

        // 当处于 Custom 状态时，自定义列表的长度，实现多对象控制
        [SerializeField] StyleDataStruck[] m_styleStructs;

        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /* 初始化 */
        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        private void Awake()
        {
            InitStyleValue();
        }

        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /* 鼠标事件方法 */
        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /// <summary>
        /// 触摸开始事件
        /// </summary>
        /// <param name="ed">Ed.</param>
        public void OnPointerDown(PointerEventData ed)
        {
            if (m_buttonType == ButtonType.Normal)
            {
                StyleControll(0);
            }
        }
        /// <summary>
        /// 触摸结束事件
        /// </summary>
        /// <param name="ed">Ed.</param>
        public void OnPointerUp(PointerEventData ed)
        {
            if (m_buttonType == ButtonType.Normal)
            {
                StyleControll(1);
            }

            /* 应用点击事件 */
            if (OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /* 样式状态控制方法 */
        /* ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ */
        /// <summary>
        /// 初始化按钮样式组件的初始值
        /// </summary>
        public void InitStyleValue()
        {
            if (m_buttonType == ButtonType.Normal)
            {
                foreach (StyleDataStruck item in m_styleStructs)
                {
                    if (item.targetGraphic != null)
                    {
                        switch (item.transitionType)
                        {
                            case StyleTransitionType.ColorTint:
                                item.normalColor = item.targetGraphic.color;
                                break;

                            case StyleTransitionType.SwapSVG:
                                SVGImage svgComponent = item.targetGraphic.gameObject.GetComponent<SVGImage>();
                                if (svgComponent == null)
                                {
                                    Debug.LogError("当前的 TargetGraphic ( " + item.targetGraphic.gameObject.name + " ) 对象上没有找到SVGImage组件，请核实！");
                                }
                                else
                                {
                                    item.normalSVG = svgComponent.vectorGraphics;
                                }
                                break;

                            case StyleTransitionType.SwapSprite:
                                Image imgComponent = item.targetGraphic.gameObject.GetComponent<Image>();
                                if (imgComponent == null)
                                {
                                    Debug.LogError("当前的 TargetGraphic ( " + item.targetGraphic.gameObject.name + " ) 对象上没有找到Image组件，请核实！");
                                }
                                else
                                {
                                    item.normalSprite = imgComponent.sprite;
                                }
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 自定义样式的控制，根据传入的状态id进行识别。
        /// </summary>
        /// <param name="stateId">触摸类型的ID，0：按下，1：抬起.</param>
        public void StyleControll(int stateId)
        {
            // 点击开始：列表中所有按钮变色
            foreach (StyleDataStruck item in m_styleStructs)
            {
                if (item.targetGraphic != null)
                {
                    MaskableGraphic grap = item.targetGraphic;
                    switch (item.transitionType)
                    {
                        case StyleTransitionType.ColorTint:
                            if (stateId == 0)
                            {
                                grap.color = item.stateColor;
                            }
                            else
                            {
                                grap.color = item.normalColor;
                            }
                            break;

                        case StyleTransitionType.SwapSprite:
                            Image imgComponent = grap.gameObject.GetComponent<Image>();
                            if (stateId == 0)
                            {
                                imgComponent.sprite = item.stateSprite;
                            }
                            else
                            {
                                imgComponent.sprite = item.normalSprite;
                            }
                            imgComponent.SetNativeSize();
                            break;

                        case StyleTransitionType.SwapSVG:
                            SVGImage svgComponent = grap.gameObject.GetComponent<SVGImage>();
                            if (stateId == 0)
                            {
                                svgComponent.vectorGraphics = item.stateSVG;
                            }
                            else
                            {
                                svgComponent.vectorGraphics = item.normalSVG;
                            }
                            svgComponent.SetNativeSize();
                            break;
                    }
                }
            }
        }
    }
}

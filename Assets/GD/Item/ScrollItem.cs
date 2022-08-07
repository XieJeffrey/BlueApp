using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GD
{

    public class ScrollItem : MonoBehaviour, IEndDragHandler
    {
         ScrollRect sr;

        RectTransform srt;
             

        Vector2 srValue;

        public Action<Vector2> onEndDrag;

        public void OnEndDrag(PointerEventData eventData)
        {
            if (onEndDrag != null)
            {
                
                Vector2 offset = Vector2.zero;
               
                 
                Vector2 srtDelta = srt.offsetMin;
                // srtDelta = GetDelta(transform);
                srtDelta = GetDelta2(transform);
                //  Debug.Log(srtDelta);
                //  Debug.Log(srt.offsetMin);
                //  Debug.Log(srt.offsetMax);
#if UNITY_EDITOR
                Debug.Log(srtDelta);
#endif
                //srt.anchorMin = min;
                // srt.anchorMax = max;
                // Debug.Log(sr.content.sizeDelta);
                srtDelta.y = Mathf.Min(srtDelta.y, sr.content.sizeDelta.y);
                srtDelta.x = Mathf.Min(srtDelta.x, sr.content.sizeDelta.x);
                //Debug.Log(srtDelta);
                //竖直方向
                if (sr.vertical)
                {
                    float y = sr.content.anchoredPosition.y;
                    if (y < 0)
                    {
                        offset.y = y;

                    }else if(y> sr.content.sizeDelta.y- srtDelta.y)
                    {
                        
                        offset.y = y - sr.content.sizeDelta.y + srtDelta.y;
                    }
                }
                //水平方向
                if(sr.horizontal)
                {
                    float x = sr.content.anchoredPosition.x;
                    if (x < 0)
                    {
                        offset.x = x;
                    }
                    else if (x > sr.content.sizeDelta.x - srtDelta.x)
                    {
                        offset.x= x - sr.content.sizeDelta.x +srtDelta.x;
                    }
                }
                onEndDrag(offset);
            }
        }


        Vector2 GetDelta(Transform rect)
        {
            if (rect.GetComponent<Canvas>())
            {

                return rect.GetComponent<RectTransform>().sizeDelta;

            }
            return GetDelta(rect.parent) + rect.GetComponent<RectTransform>().sizeDelta;

        }
        /// <summary>
        /// 获取当前ui组件的尺寸
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        Vector2 GetDelta2(Transform rect)
        {
            if (rect.GetComponent<Canvas>())
            {
                return rect.GetComponent<RectTransform>().sizeDelta;
            }

            RectTransform _rt = rect.GetComponent<RectTransform>();
            Vector2 anchors = _rt.anchorMax - _rt.anchorMin;
            Vector2 offset= _rt.offsetMax- _rt.offsetMin; ;
            //Debug.Log(anchors);
            if (anchors == Vector2.zero)
            {
              
                return offset;

            }
            Vector2 parentData = GetDelta2(rect.parent);
            parentData = new Vector2(parentData.x * anchors.x, parentData.y * anchors.y);
            return parentData+ offset;

        }

        private void Start()
        {

            sr = transform.GetComponent<ScrollRect>();
            // sr.onValueChanged.AddListener(OnValueChange);
            srt = sr.GetComponent<RectTransform>();
        }
        //private void OnValueChange(Vector2 arg0)
        //{
        //   // sr.content.sizeDelta;
        //   Debug.Log(arg0);
        //    srValue = arg0;
        //}
    }
}

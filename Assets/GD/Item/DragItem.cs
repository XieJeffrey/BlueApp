using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace GD
{

    public class DragItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public System.Action OnEnter;
        public System.Action OnExit;
        public void OnPointerEnter(PointerEventData eventData)
        {

            if (OnEnter != null)
            {
                OnEnter();
            }
         
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (OnExit != null)
            {
                OnExit();
            }

        }
    }
}

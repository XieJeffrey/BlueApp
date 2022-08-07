using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XMWorkspace;
using DG.Tweening;

namespace LM_Workspace
{
	public class foremanItemControl : MonoBehaviour,IDragHandler
	{
		private RectTransform selfRect;

		private float tweenSpeed;

		#region IDragHandler implementation

		public void OnDrag (PointerEventData eventData)
		{
			//throw new System.NotImplementedException ();
			//Debug.Log(eventData.delta.x);

			Vector2 leftEndValue = new Vector2(0,-130);
			Vector2 rightEndValue = new Vector2 (128, 0);

			Vector2 leftBeginValue = new Vector2(-128,-130);
			Vector2 rightBeginValue = new Vector2 (0, 0);
			//Debug.Log(selfRect.offsetMin);
			//Debug.Log (selfRect.offsetMax);
			//向右滑动
			if (eventData.delta.x >= 10)
			{
				selfRect.DOPause ();
				DOTween.To (() => selfRect.offsetMin, r => selfRect.offsetMin = r, leftEndValue, tweenSpeed);
				DOTween.To (() => selfRect.offsetMax, r => selfRect.offsetMax = r, rightEndValue, tweenSpeed);
			} else if (eventData.delta.x <= -10) 
			{
				selfRect.DOPause ();
				DOTween.To (() => selfRect.offsetMin, r => selfRect.offsetMin = r, leftBeginValue, tweenSpeed);
				DOTween.To (() => selfRect.offsetMax, r => selfRect.offsetMax = r, rightBeginValue, tweenSpeed);
			}
		}

		#endregion


		// Use this for initialization
		void Start () 
		{
			selfRect = this.GetComponent<RectTransform> ();

			tweenSpeed = 0.5f;
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LM_Workspace
{
    public class ToBigImageHelper : SingleMono<ToBigImageHelper>
    {
        [System.NonSerialized]
        public string typeStr;

        [System.NonSerialized]
        public float layoutHeight;
        protected void Start()
		{
            transform.GetComponent<UIEventListener>().onClick = delegate {
                UtilManager.getInstance.bigImagePanel.SetActive(true);
                UtilManager.getInstance.bigRawImage.texture = transform.GetComponent<RawImage>().mainTexture;
                UtilManager.getInstance.bigRawImage.GetComponent<LayoutElement>().preferredHeight = layoutHeight;
                BigImageManager.getInstance.currentTypeStr = typeStr;
                BigImageManager.getInstance.currentItemName = transform.parent.name;
            };
		}
	}
}

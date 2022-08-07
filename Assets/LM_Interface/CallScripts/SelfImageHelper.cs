using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LM_Workspace;
using XMWorkspace;

public class SelfImageHelper : MonoBehaviour 
{
	private Transform pTran;

	private RectTransform cTran;

	private RawImage cImage;

	public string imageUrl;

	private bool isDownLoading;

	private bool isClear;

	private bool isAddUrl;

	public EnumManager.imageSelfType imageType;

	void Start()
	{
		cTran = this.GetComponent<RectTransform> ();

		switch (imageType)
		{
		case EnumManager.imageSelfType.market:
			pTran = Util.FindChildByName (UtilManager.getInstance.marketGo, "p_chaoshi/docPanel/scrolloffset").transform;
			break;
		case EnumManager.imageSelfType.foremanRank:
			pTran = Util.FindChildByName (UtilManager.getInstance.foremanRankGo, "p_main/scroll/offset").transform;
			break;
		}

		cImage = transform.GetComponent<RawImage> ();

		isDownLoading = false;
		isClear = false;

		isAddUrl = false;
	}

	void Update()
	{
		//Debug.Log (transform.position.y + transform.name);
		//Debug.Log (pTran.transform.position.y);
		if (cImage.texture != null && cImage.texture.name != "icon_userHeadImage") {
			if (transform.position.y - pTran.position.y < -20 || transform.position.y - pTran.position.y > 10) {
				if (!isClear) {
					isClear = true;
					isDownLoading = false;
					//StopAllCoroutines ();
					cImage.texture = null;
				}
			}
		} else if (cImage.texture == null || cImage.texture.name == "icon_userHeadImage") {
			if (imageUrl != "")
			{
				if (transform.position.y - pTran.position.y >= -20 && transform.position.y - pTran.position.y <= 10) {
					if (!isDownLoading) {
						isDownLoading = true;
						if (!isAddUrl)
						{
							imageUrl += "?imageView2/2/w/" + cTran.sizeDelta.x.ToString () + "/h/" + cTran.sizeDelta.y.ToString ();
							isAddUrl = true;
						}
						StartCoroutine (PostImageManager.getInstance.LoadImageFromUrl (imageUrl, (int)cTran.sizeDelta.x, (int)cTran.sizeDelta.y, delegate {
							isClear = false;
							return cImage;

						}));
					}
				}
			}
		}
	}

	void OnDisable()
	{
		StopAllCoroutines ();
	}

}

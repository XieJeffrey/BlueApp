using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SVGImporter;
namespace GD
{

    public class MyTriggle : MonoBehaviour
    {

        public RectTransform slider;
        
        public SVGImage bg;
        public System.Action<bool> onValueChange;
        private bool isOn;

        public bool IsOn
        {
            get { return isOn; }
            set {
               
                isOn = value;
                
                if (isOn)
                {
                    slider.anchoredPosition = Vector3.left * 4;
                    bg.color = new Color32(38, 149,159, 225);

                }else
                {
                    slider.anchoredPosition = Vector3.left * 64;
                    bg.color = new Color32(181, 181, 181, 225);
                }
            }
        }

        private void Start()
        {
            if (bg.GetComponent<UIEventListener>())
            {
                bg.GetComponent<UIEventListener>().onClick = delegate {

                    IsOn = !IsOn;
                    if (onValueChange != null)
                    {
                        onValueChange(IsOn);
                    }
                };
            }
            
        }



    }


}

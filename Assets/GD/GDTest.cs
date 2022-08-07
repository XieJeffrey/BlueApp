using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GD
{

    public class GDTest : MonoBehaviour
    {

        public ScrollItem st;

        private int index = 0;
        public Text text;
        private void Start()
        {

            st.onEndDrag = OnEndDrag;
            text.text = "page:" + index;



        }

        private void OnEndDrag(Vector2 obj)
        {
#if UNITY_EDITOR
            Debug.Log(obj);
#endif
            if (obj.y <= -0.2)
            {
                index++;

            }
            else if(obj.y >= 1.2)
            {
                index--;
            }

            text.text = "page:" + index;
        }

       
    }

    
}

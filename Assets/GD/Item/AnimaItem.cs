using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimaItem : MonoBehaviour {

    Animator anim;
    private void OnEnable()
    {
        if (!anim)
            anim=GetComponent<Animator>();
       
        anim.SetInteger("state",UnityEngine.Random.Range(0,3));
    }

}

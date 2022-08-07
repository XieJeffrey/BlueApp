using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMono<T> :MonoBehaviour {

    public static T instance;

    private void Awake()
    {
        instance = GetComponent<T>();

    }

}

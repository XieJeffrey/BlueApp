using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingHelper : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeInHierarchy)
        {
            transform.Rotate(Vector3.forward * Time.deltaTime * -100);
        }
    }
}

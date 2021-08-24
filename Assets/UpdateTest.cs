using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTest : MonoBehaviour
{
    bool isT;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) && !isT)
        {
            isT = true;
            Debug.Log("いえーい！");
        }
    }


    void FixedUpdate()
    {
        if (isT)
        {
            Debug.Log("FIぇｄで呼ばれました＠確定");
            isT = !isT;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("FIぇｄで呼ばれました＠不確定");
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        NormalStoneInfomation temp = collision.gameObject.GetComponent<NormalStoneInfomation>();
        if(temp != null)
        {
            temp.DeleteMyself();
        }
    }
}

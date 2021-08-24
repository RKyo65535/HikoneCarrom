using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IStoneDestryable temp = collision.gameObject.GetComponent<IStoneDestryable>();
        if(temp != null)
        {
            temp.DestoryMyself();
        }
    }
}

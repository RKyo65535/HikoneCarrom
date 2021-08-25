using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IStoneDestryable destroyableObject = collision.gameObject.GetComponent<IStoneDestryable>();
        if(destroyableObject != null)
        {
            destroyableObject.DestoryMyself();
        }
    }
}

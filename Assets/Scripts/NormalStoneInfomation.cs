using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NormalStoneInfomation : MonoBehaviour, IStoneDestryable
{

    //マネージャーから受け取った関数
    //自身が破棄されたとき、マネージャー君に設定された奴を実行する
    Action<StoneRole> destroyEvent;





    StoneRole stoneAttribute;

    [SerializeField] Material redMaterial;
    [SerializeField] Material blueMaterial;

    public void SetMyAttribute(StoneRole attribute, Action<StoneRole> destoryEvent)
    {
        this.destroyEvent = destoryEvent;
        stoneAttribute = attribute;
        switch (stoneAttribute)
        {
            case StoneRole.RED:               
                GetComponent<MeshRenderer>().material = redMaterial;
                break;
            case StoneRole.BLUE:
                GetComponent<MeshRenderer>().material = blueMaterial;
                break;
            default:
                break;
        }

    }

    public void DestoryMyself()
    {
        destroyEvent(stoneAttribute);
        Destroy(gameObject);
    }


}

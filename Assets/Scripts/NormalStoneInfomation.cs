using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStoneInfomation : MonoBehaviour, IStoneDestryable
{
    /// <summary>
    /// 石の属性
    /// </summary>
    public enum StoneAttribute
    {
        RED,
        BLUE
    }

    StoneAttribute stoneAttribute;

    [SerializeField] Material redMaterial;
    [SerializeField] Material blueMaterial;

    public void SetMyAttribute(StoneAttribute attribute)
    {
        stoneAttribute = attribute;
        switch (stoneAttribute)
        {
            case StoneAttribute.RED:
                GetComponent<MeshRenderer>().material = redMaterial;
                break;
            case StoneAttribute.BLUE:
                GetComponent<MeshRenderer>().material = blueMaterial;
                break;
            default:
                break;
        }
    }

    public void DestoryMyself()
    {
        Destroy(gameObject);
    }


}

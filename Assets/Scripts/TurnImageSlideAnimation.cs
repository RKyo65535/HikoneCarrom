using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnImageSlideAnimation : MonoBehaviour
{
    [SerializeField] CanvasScaler canvasScaler;
    [SerializeField] RectTransform myRectTransform;

    Vector2 startPoint;
    Vector2 endPoint;

    private void Start()
    {
        Debug.Log("offsetMinは" + myRectTransform.offsetMin);
        Debug.Log("offsetMaxは" + myRectTransform.offsetMax);

        //myRectTransform.offsetMin = new Vector2(-100,-100);
        //myRectTransform.offsetMax = new Vector2(100, 100);

        Debug.Log("offsetMinは" + myRectTransform.offsetMin);
        Debug.Log("offsetMaxは" + myRectTransform.offsetMax);

        //float startx = canvasScaler.referenceResolution.x + myRectTransform.



    }

}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// おおもとのスクリプトから共通部分を抜き出した方が賢いやり方かもしれない
/// </summary>
public class WinImageMoveAnimation : MonoBehaviour,ITeamUIAnimationable
{


    [SerializeField] CanvasScaler canvasScaler;
    RectTransform myRectTransform;
    Image myImageComponent;

    [SerializeField] Sprite redTeamSprite;
    [SerializeField] Sprite blueTeamSprite;


    Vector2 startPoint;
    Vector2 endPoint;
    Vector2 adoptedImageDeltaSize;



    public void Initialize()
    {

        myRectTransform = GetComponent<RectTransform>();
        myImageComponent = GetComponent<Image>();

        //Debug.Log("offsetMinは" + myRectTransform.offsetMin);
        //Debug.Log("offsetMaxは" + myRectTransform.offsetMax);


        adoptedImageDeltaSize = new Vector2(
            myRectTransform.sizeDelta.x * myRectTransform.localScale.x,
            myRectTransform.sizeDelta.y * myRectTransform.localScale.y
            );




        //ぎりぎりのスタート座標設定になっているはずなんですが……
        float startx = 0;
        float starty = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / 2f;
        float endx = 0;
        float endy = 0;

        startPoint = new Vector2(startx, starty);
        endPoint = new Vector2(endx, endy);

        Debug.Log(startPoint + "から" + endPoint);


    }



    /// <summary>
    /// 実際に画像を変更し、動かす感じのやつ
    /// </summary>
    /// <param name="stone"></param>
    public void StartAnimation(StoneRole stone)
    {
        switch (stone)
        {
            case StoneRole.RED:
                myImageComponent.sprite = redTeamSprite;
                break;
            case StoneRole.BLUE:
                myImageComponent.sprite = blueTeamSprite;
                break;
            default:
                break;
        }

        _ = SlideImageByTask(startPoint, endPoint, 100);
    }



    /// <summary>
    /// タスクを用いた非同期処理にて徐々に座標を動かす作戦
    /// </summary>
    async Task SlideImageByTask(Vector2 startPoint, Vector2 endPoint, int loopTimes)
    {
        for (int i = 0; i < loopTimes; i++)
        {
            await Task.Run(() => Task.Delay(10));
            Vector2 newPoint = ((startPoint * (loopTimes - i - 1)) + endPoint * i) / (loopTimes - 1);
            myRectTransform.anchoredPosition = newPoint;
        }

    }


}

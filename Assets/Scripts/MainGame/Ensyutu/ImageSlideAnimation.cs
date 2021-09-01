using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 正直Taskを用いてアニメーションさせる習作のスクリプトのため、実際はもっと別な手段を取った方がUnityでは賢い
/// 本来であれば重たい処理を別スレッドに行わせるためにTaskを作るのだが、今回はTask.Delayの為だけにTask使ってる感がある。
/// </summary>
public class ImageSlideAnimation : MonoBehaviour,ITeamUIAnimationable
{
    enum Directions
    {
        Top,
        UpperRight,
        Right,
        LowerRight,
        Bottom,
        LowerLeft,
        Left,
        UpperLeft,
        Center
    }


    //キャンバススカラー君から参照の大きさを取得するのはとても重要と思います。
    [SerializeField] CanvasScaler canvasScaler;

    //自分自身の参照シリーズ
    RectTransform myRectTransform;
    Image myImageComponent;

    //今回は画像が2種類必要なので、二つ参照
    [SerializeField] Sprite redTeamSprite;
    [SerializeField] Sprite blueTeamSprite;

    //どの方向からやってきて、どこに消えるか
    [SerializeField] Directions startDirection;
    [SerializeField] Directions endDirection;

    //アニメーション用の変数
    [SerializeField] int waitingMiliseconds=16;
    [SerializeField] int loopTimes=100;

    //内部変数
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

              

        startPoint = SetPosition(startDirection);
        endPoint = SetPosition(endDirection);

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

        _ = SlideImageByTask(startPoint, endPoint, loopTimes);
    }

    /// <summary>
    /// タスクを用いた非同期処理にて徐々に座標を動かす作戦
    /// </summary>
    async Task SlideImageByTask(Vector2 startPoint, Vector2 endPoint, int loopTimes)
    {
        for (int i = 0; i < loopTimes; i++)
        {
            await Task.Run(() => Task.Delay(waitingMiliseconds));
            Vector2 newPoint = ((startPoint * (loopTimes - i - 1)) + endPoint * i) / (loopTimes - 1);
            myRectTransform.anchoredPosition = newPoint;
        }

    }

    /// <summary>
    /// 位置をいい感じに計算してくれる関数
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    Vector2 SetPosition(Directions direction)
    {

        float posx = 0 ;
        float posy = 0;
        switch (direction)
        {
            case Directions.Top:
                posx = 0;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / 2f;
                break;
            case Directions.UpperRight:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / 2f;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / 2f;
                break;
            case Directions.Right:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / 2f;
                posy = 0;
                break;
            case Directions.LowerRight:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / 2f;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / -2f;
                break;
            case Directions.Bottom:
                posx = 0;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / -2f;
                break;
            case Directions.LowerLeft:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / -2f;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / -2f;
                break;
            case Directions.Left:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / -2f;
                posy = 0;
                break;
            case Directions.UpperLeft:
                posx = (canvasScaler.referenceResolution.x + adoptedImageDeltaSize.x) / -2f;
                posy = (canvasScaler.referenceResolution.y + adoptedImageDeltaSize.y) / 2f;
                break;
            case Directions.Center:
                posx = 0;
                posy = 0;
                break;
            default:
                break;
        }
        return new Vector2(posx, posy);
    }



}

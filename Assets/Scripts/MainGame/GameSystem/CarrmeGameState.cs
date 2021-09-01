using UnityEngine;
using UnityEngine.UI;

public class CarrmeGameState : MonoBehaviour
{

    //マスターから管理すべきオブジェクトたち
    [SerializeField] StonePlacementer stonePlacementer;
    [SerializeField] PlayerStoneProjector stoneProjector;
    [SerializeField] InputMouseReseaver inputMouseReseaver;
    [SerializeField] TurnImageSlideAnimation turnImageSlideAnimation;


    [SerializeField] Text redTeamRemainStoneCountText;
    [SerializeField] Text blueTeamRemainStoneCountText;
    [SerializeField] Text whoseTurnText;

    [Tooltip("1チームあたりが落とさないといけない石の数 > 2")]
    [SerializeField] int numOfStonesOfOneTeam;
    [SerializeField] int juckPenaltyStoneCount;

    [SerializeField] Vector3 redTeamInitialStonePoint;
    [SerializeField] Vector3 blueTeamInitialStonePoint;

    StoneCounter stoneCounter;

    bool canContinueTurn = false;//連続で石を打つことができるフラグ


    enum WhoseTurn
    {
        PLAYER1,
        PLAYER2,
    }

    enum GameState
    {
        WAIT_FOR_SHOOT,//カロムがはじかれるのを待っている状態
        SIMURATING,//実際に弾が動いている状態
    }

    WhoseTurn whoseTurn;
    GameState gameState;



    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトたちの初期化
        turnImageSlideAnimation.Initialize();
        stonePlacementer.Initialize(numOfStonesOfOneTeam, StoneDestroyEvent);

        //指示された数だけ石を置く
        stoneCounter = new StoneCounter(numOfStonesOfOneTeam);
        RefleshCountText();

        //石を置き、石を弾けるようにする
        PlasePlayerStone();
        inputMouseReseaver.action = ProjectPlayerStone;
    }

    void ProjectPlayerStone(Vector3 targetPoint)
    {
        if (gameState == GameState.WAIT_FOR_SHOOT)
        {
            stoneProjector.ProjectStone(targetPoint);
            gameState = GameState.SIMURATING;
        }
        else
        {

        }
    }


    void ResetPlayerStone()
    {
        //連続攻撃の可否を判断
        if (canContinueTurn)
        {
            Debug.Log("連続攻撃");
            canContinueTurn = false;
        }
        else
        {
            SwitchTurn();
        }


        PlasePlayerStone();
    }

    void PlasePlayerStone()
    {
        //カロムがはじかれるのを待っている状態にする
        gameState = GameState.WAIT_FOR_SHOOT;

        //ターン表示を明らかに行う
        whoseTurnText.text = whoseTurn + "のターン！";
        turnImageSlideAnimation.StartAnimation((StoneRole)whoseTurn);


        switch (whoseTurn)
        {
            case WhoseTurn.PLAYER1:
                stoneProjector.SetNewStone(redTeamInitialStonePoint, ResetPlayerStone, IsWaitForShooting);
                break;
            case WhoseTurn.PLAYER2:
                stoneProjector.SetNewStone(blueTeamInitialStonePoint, ResetPlayerStone, IsWaitForShooting);
                break;
            default:
                break;
        }

    }

    void SwitchTurn()
    {
        whoseTurn = (WhoseTurn)(((int)whoseTurn + 1) % 2);
    }



    /// <summary>
    /// 石が破棄される際に呼び出されるイベントです。
    /// 石の方にこの関数を渡してしまいます。
    /// </summary>
    /// <param name="stoneAttribute"></param>
    void StoneDestroyEvent(StoneRole stoneAttribute)
    {
        //Debug.Log("落ちた石の色は"+stoneAttribute+"色です");

        //ジャックが落ちたときの処理を最初に考える。
        if (stoneAttribute == StoneRole.JUCK)
        {
            if (stoneCounter.IsThereNoStone((StoneRole)whoseTurn))
            {
                //勝利条件を満たしていたら勝ちの処理
                Debug.LogError(whoseTurn + "のかち");
            }
            else
            {
                //勝利条件を満たしてないのにジャックを落としたらダメ
                for (int i = 0;
                    i < juckPenaltyStoneCount && stoneCounter.GetCurrentStoneCount((StoneRole)whoseTurn)<numOfStonesOfOneTeam;
                    i++)
                {
                    stoneCounter.AddOneStone((StoneRole)whoseTurn);
                    stonePlacementer.SetOneStone((StoneRole)whoseTurn, StoneDestroyEvent);
                }
                stonePlacementer.SetOneStone(StoneRole.JUCK, StoneDestroyEvent);
                RefleshCountText();
            }
        }
        else
        {
            //ジャック以外が落ちた場合は普通に処理
            stoneCounter.ReduceOneStone(stoneAttribute);
            RefleshCountText();
        }


        //自身と同じ色の石を落とした場合、連続行動ができるのだ
        if ((StoneRole)whoseTurn == stoneAttribute)
        {
            canContinueTurn = true;
        }



    }


    /// <summary>
    /// 打つ前の状態ならtrueを返す関数。
    /// カウントダウンの際にプレイヤーの石のオブジェクトが使う
    /// </summary>
    /// <returns></returns>
    bool IsWaitForShooting()
    {
        if (gameState == GameState.WAIT_FOR_SHOOT)
        {
            return true;
        }
        return false;
    }

    void RefleshCountText()
    {
        redTeamRemainStoneCountText.text = "" + stoneCounter.GetCurrentStoneCount(StoneRole.RED);
        blueTeamRemainStoneCountText.text = "" + stoneCounter.GetCurrentStoneCount(StoneRole.BLUE);

    }

}

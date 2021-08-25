using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrmeGameState : MonoBehaviour
{

    [SerializeField] StonePlacementer stonePlacementer;
    [SerializeField] PlayerStoneProjector stoneProjector;
    [SerializeField] InputMouseReseaver inputMouseReseaver;

    [SerializeField] Text redTeamRemainStoneCountText;
    [SerializeField] Text blueTeamRemainStoneCountText;

    [Tooltip("1チームあたりが落とさないといけない石の数 > 2")]
    [SerializeField] int numOfStonesOfOneTeam;
    [SerializeField] int juckPenaltyStoneCount;

    StoneCounter stoneCounter;


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
        //指示された数だけ石を置く
        stoneCounter = new StoneCounter(numOfStonesOfOneTeam);
        RefleshCountText();
        stonePlacementer.Initialize(numOfStonesOfOneTeam,StoneDestroyEvent);

        //石を置き、石を弾けるようにする
        PlasePlayerStone();
        inputMouseReseaver.action = ProjectPlayerStone;
    }

    void ProjectPlayerStone(Vector3 targetPoint)
    {
        if(gameState == GameState.WAIT_FOR_SHOOT)
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
        SwitchTurn();
        PlasePlayerStone();
    }

    void PlasePlayerStone()
    {
        gameState = GameState.WAIT_FOR_SHOOT;//カロムがはじかれるのを待っている状態
        stoneProjector.SetNewStone(new Vector3(8, 0.2f, 0), ResetPlayerStone, IsWaitForShooting);
    }

    void SwitchTurn()
    {
        whoseTurn = (WhoseTurn)(((int)whoseTurn + 1) % 2);
        Debug.Log(whoseTurn + "のターンです");
    }



    /// <summary>
    /// 石が破棄される際に呼び出されるイベントです。
    /// 石の方にこの関数を渡してしまいます。
    /// </summary>
    /// <param name="stoneAttribute"></param>
    void StoneDestroyEvent(StoneRole stoneAttribute)
    {
        Debug.Log("落ちた石の色は"+stoneAttribute+"色です");

        if(stoneAttribute == StoneRole.JUCK)
        {
            if (stoneCounter.IsThereNoStone((StoneRole)whoseTurn))
            {
                //勝利条件を満たしていたら勝ちの処理
                Debug.LogError(whoseTurn + "のかち");
            }
            else
            {
                //勝利条件を満たしてないのにジャックを落としたらダメ
                for(int i=0;i< juckPenaltyStoneCount; i++)
                {
                    stoneCounter.AddOneStone((StoneRole)whoseTurn);
                    stonePlacementer.SetOneStone((StoneRole)whoseTurn, StoneDestroyEvent);
                }
                stonePlacementer.SetOneStone(StoneRole.JUCK, StoneDestroyEvent);
            }
        }
        else
        {
            //ジャック以外が落ちた場合は普通に処理
            stoneCounter.ReduceOneStone(stoneAttribute);
            RefleshCountText();
        }

    }
    

    /// <summary>
    /// 打つ前の状態ならtrueを返す関数。
    /// カウントダウンの際にプレイヤーの石のオブジェクトが使う
    /// </summary>
    /// <returns></returns>
    bool IsWaitForShooting()
    {
        if(gameState == GameState.WAIT_FOR_SHOOT)
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

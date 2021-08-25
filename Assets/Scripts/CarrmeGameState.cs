using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrmeGameState : MonoBehaviour
{

    [SerializeField] StonePlacementer stonePlacementer;
    [SerializeField] PlayerStoneProjector stoneProjector;
    [SerializeField] InputMouseReseaver inputMouseReseaver;

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
        stonePlacementer.Initialize(StoneDestroyEvent);
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

        if(stoneAttribute == StoneRole.RED && whoseTurn == WhoseTurn.PLAYER1)
        {

        }
        else if(stoneAttribute == StoneRole.BLUE && whoseTurn == WhoseTurn.PLAYER2)
        {

        }
        else
        {
            stonePlacementer.SetOneStone(stoneAttribute, StoneDestroyEvent);
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



}

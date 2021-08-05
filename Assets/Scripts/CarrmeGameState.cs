using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrmeGameState : MonoBehaviour
{

    [SerializeField] StoneInitialPlacementer stoneInitialPlacementer;
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
        stoneInitialPlacementer.Initialize();
        ResetPlayerStone();

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
        gameState = GameState.WAIT_FOR_SHOOT;//カロムがはじかれるのを待っている状態
        stoneProjector.SetNewStone(new Vector3(8, 0.2f, 0),ResetPlayerStone);
    }


}

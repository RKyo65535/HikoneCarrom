using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrmeGameState : MonoBehaviour
{

    [SerializeField] StoneInitialPlacementer stoneInitialPlacementer;

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
   

    // Start is called before the first frame update
    void Start()
    {
        stoneInitialPlacementer.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoneProjector : MonoBehaviour
{

    [SerializeField] GameObject stone;
    [SerializeField] Material playerMaterial;//プレイヤー用のマテリアル

    GameObject currentMyStone;//現在操作中の石


    public void SetNewStone(Vector3 startPos)
    {
        GameObject currentMyStone = Instantiate(stone);
        currentMyStone.transform.position = startPos;
        currentMyStone.GetComponent<MeshRenderer>().material = playerMaterial;
    }

    public void ProjectStone(Vector3 velocity)
    {
        currentMyStone.GetComponent<Rigidbody>().velocity = velocity;
    }


}

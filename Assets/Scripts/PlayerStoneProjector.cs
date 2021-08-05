using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoneProjector : MonoBehaviour
{

    [SerializeField] GameObject stone;
    [SerializeField] Material playerMaterial;//プレイヤー用のマテリアル

    public GameObject currentMyStone;//現在操作中の石


    public void SetNewStone(Vector3 startPos,Action action)
    {
        currentMyStone = Instantiate(stone);
        currentMyStone.transform.position = startPos;
        currentMyStone.GetComponent<MeshRenderer>().material = playerMaterial;
        currentMyStone.GetComponent<StopAndDeleteStone>().beforeDeleteMyself = action;
    }

    public void ProjectStone(Vector3 target)
    {
        if(currentMyStone == null)
        {
            Debug.Log(currentMyStone);
            return;
        }
        else
        {
            Transform stoneTF = currentMyStone.transform;
            float angle = Mathf.Atan2(target.z - stoneTF.position.z, target.x - stoneTF.position.x);
            currentMyStone.GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle))*100;

        }
    }


}

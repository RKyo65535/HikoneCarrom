using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStoneProjector : MonoBehaviour
{

    [SerializeField] GameObject stone;
    [SerializeField] Vector3 initialPos;
    [SerializeField] Material playerMaterial;//プレイヤー用のマテリアル

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = Instantiate(stone);
            obj.GetComponent<Rigidbody>().position = initialPos;
            obj.transform.position = initialPos;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(initialPos.normalized.x, 0, initialPos.normalized.z) * -100;
            obj.GetComponent<MeshRenderer>().material = playerMaterial;
        }
    }
}

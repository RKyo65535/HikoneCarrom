using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndDeleteStone : MonoBehaviour
{

    [Tooltip("何秒止まっていたら消えるか")]
    [SerializeField] float timeLimit;
    float count;

    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if(RB.velocity == Vector3.zero)
        {
            count += Time.deltaTime;
            if(count > timeLimit)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            count = 0;
        }
    }
}

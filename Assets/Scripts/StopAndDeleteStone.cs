using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAndDeleteStone : MonoBehaviour
{

    [Tooltip("何秒止まっていたら消えるか")]
    [SerializeField] float timeLimit;
    float count;

    public Action beforeDeleteMyself;
    public Func<bool> isWaitForShooting;

    Rigidbody RB;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if(RB.velocity == Vector3.zero && !isWaitForShooting())
        {
            count += Time.deltaTime;
            if(count > timeLimit)
            {
                beforeDeleteMyself();
                Destroy(gameObject);
            }
        }
        else
        {
            count = 0;
        }
    }
}

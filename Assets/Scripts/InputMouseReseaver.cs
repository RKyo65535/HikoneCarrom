using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseReseaver : MonoBehaviour
{

    [SerializeField] Camera mainCamera;

    public Action<Vector3> action;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] hits = Physics.RaycastAll(ray, 30);

            foreach (RaycastHit hit in hits)
            {
                Debug.Log("当たり判定チェック");
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    action(hit.point);
                    Debug.Log(hit.point);
                }
            }

        }
    }
}

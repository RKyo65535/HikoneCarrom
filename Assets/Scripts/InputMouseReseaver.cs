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

            Debug.Log("マウス押しました");
            Debug.Log("始点は"+ mainCamera.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("向きは"+ mainCamera.transform.localEulerAngles);
            //タッチされた部分から、カメラの向きに向かってRayを発射する
            RaycastHit[] hits = Physics.RaycastAll(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.localEulerAngles,30);

            

            Debug.DrawRay(mainCamera.ScreenToWorldPoint(Input.mousePosition), mainCamera.transform.localEulerAngles);

            foreach (RaycastHit hit in hits)
            {
                Debug.Log("当たり判定チェック");
                if (hit.collider.gameObject.CompareTag("Ground"))
                {
                    action(hit.point);
                    Debug.Log("イベント発火");
                }
            }

        }
    }
}

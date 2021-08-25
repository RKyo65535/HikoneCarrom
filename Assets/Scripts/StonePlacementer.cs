using UnityEngine;
using System;


public class StonePlacementer : MonoBehaviour
{

    [SerializeField] GameObject stone;

    [Tooltip("カロムの石の大きさ")]
    [SerializeField] float carromSize;
    [Tooltip("1チームあたりが落とさないといけない石の数 > 2")]
    [SerializeField] int numOfStonesOfOneTeam;
    Transform TF;




    // Start is called before the first frame update
    public void Initialize(Action<StoneRole> destroyEvent)
    {
        //==============================
        //最初に、普通の色の石を配置する
        //==============================

        TF = transform;
        //まずは分母を求める
        float sine1 = Mathf.Sin((numOfStonesOfOneTeam * 2 - 2) * 180f * Mathf.Deg2Rad / (numOfStonesOfOneTeam * 4));
        //分子も求める
        float sine2 = Mathf.Sin(360 * Mathf.Deg2Rad / (numOfStonesOfOneTeam * 2));
        float tempRadius = 2 * carromSize * sine1 / sine2;

        for (int i = 0; i < numOfStonesOfOneTeam * 2; i++)
        {
            float angle = 360f / (numOfStonesOfOneTeam * 2) * i * Mathf.Deg2Rad;
            GameObject obj = Instantiate(stone,TF);
            obj.transform.position = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * tempRadius + new Vector3(0,0.2f,0);
            obj.GetComponent<NormalStoneInfomation>().SetMyAttribute((StoneRole)(i % 2), destroyEvent);
        }
        //==============================
        //キングの石を配置する
        //==============================
        GameObject juckObj = Instantiate(stone, TF);
        juckObj.GetComponent<NormalStoneInfomation>().SetMyAttribute(StoneRole.JUCK, destroyEvent);
        juckObj.transform.position = new Vector3(0, 0.2f, 0);
        juckObj.transform.localScale *= 1.2f;//ちょっと大きめにする

    }

    public void SetOneStone(StoneRole stoneRole, Action<StoneRole> destroyEvent)
    {

        GameObject obj = Instantiate(stone, TF);
        obj.transform.position = new Vector3((UnityEngine.Random.value-0.5f)*0.1f, 0.2f, (UnityEngine.Random.value - 0.5f) * 0.1f);
        obj.GetComponent<NormalStoneInfomation>().SetMyAttribute(stoneRole, destroyEvent);

        if (stoneRole == StoneRole.JUCK)
        {
            obj.transform.localScale *= 1.2f;//ちょっと大きめにする
        }
    }
}

using UnityEngine;

public class StoneInitialPlacementer : MonoBehaviour
{

    [SerializeField] GameObject stone;

    [Tooltip("カロムの石の大きさ")]
    [SerializeField] float carromSize;
    [Tooltip("1チームあたりが落とさないといけない石の数 > 2")]
    [SerializeField] int numOfStonesOfOneTeam;

    [SerializeField] Material[] materials;//青と赤のマテリアルで色を飾る 
    [SerializeField] Material kingMaterial;//最後に倒す用のマテリアル

    // Start is called before the first frame update
    public void Initialize()
    {
        //==============================
        //最初に、普通の色の石を配置する
        //==============================

        Transform TF = transform;
        //まずは分母を求める
        float sine1 = Mathf.Sin((numOfStonesOfOneTeam * 2 - 2) * 180f * Mathf.Deg2Rad / (numOfStonesOfOneTeam * 4));
        //分子も求める
        float sine2 = Mathf.Sin(360 * Mathf.Deg2Rad / (numOfStonesOfOneTeam * 2));
        float tempRadius = 2 * carromSize * sine1 / sine2;

        for (int i = 0; i < numOfStonesOfOneTeam * 2; i++)
        {
            float angle = 360f / (numOfStonesOfOneTeam * 2) * i * Mathf.Deg2Rad;
            GameObject obj = Instantiate(stone,TF);
            obj.transform.position = new Vector3(Mathf.Cos(angle), 1, Mathf.Sin(angle)) * tempRadius;
            SetMaterialWithIndex(obj, i);
        }
        //==============================
        //キングの石を配置する
        //==============================
        GameObject kingObj = Instantiate(stone, TF);
        kingObj.GetComponent<MeshRenderer>().material = kingMaterial;
        kingObj.transform.position = Vector3.up;
        kingObj.transform.localScale *= 1.2f;//ちょっと大きめにする

    }

    void SetMaterialWithIndex(GameObject obj, int index)
    {
        obj.GetComponent<MeshRenderer>().material = materials[index % 2];
    }


}

using UnityEngine;

public class StoneInitialPlacementer : MonoBehaviour
{

    [SerializeField] GameObject stone;

    [Tooltip("カロムの石の大きさ")]
    [SerializeField] float carromSize;
    [Tooltip("1チームあたりが落とさないといけない石の数 > 2")]
    [SerializeField] int numOfStonesOfOneTeam;

    [SerializeField] Material[] materials;//青と赤のマテリアルで色を飾る 

    // Start is called before the first frame update
    void Start()
    {
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


    }

    void SetMaterialWithIndex(GameObject obj, int index)
    {
        obj.GetComponent<MeshRenderer>().material = materials[index % 2];
    }


}

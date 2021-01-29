using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class BezierInstantiator : MonoBehaviour
{
    public List<Transform> BezierPoints;
    public List<GameObject> PrefabList;
    public Transform Parent;
    public int N;
    private float currentT;

    [Space]
    [Header("Random Rotation")]
    [Range(0, 360)] public float X_rot;
    [Range(0, 360)] public float Y_rot;
    [Range(0, 360)] public float Z_rot;
    public Vector3 Starting_rot;
    [Space]
    [Header("Instantiate!")]
    public bool Instantiate;
    public bool Destroy;
    // Start is called before the first frame update

    void Start()
    {
        //currentT = 0;
        //float increment = 1f/N;
        //GameObject go;
        //for(int i =0; i<=N;i++)
        //{
        //    //Debug.Log(currentT);
        //    go = Instantiate(Prefab,Parent);
        //    //go.transform.position = BezierCurve(currentT);
        //    BezierFollower bzf= go.GetComponent<BezierFollower>();

        //    bzf.script = this;
        //    bzf.MyT = currentT;
        //    currentT += increment;
        //}
    }
    void InstantiatePointsFixed()
    {
        currentT = 0;
        float increment = 1f / N;
        GameObject go;
        for (int i = 0; i <= N; i++)
        {
            //Debug.Log(currentT);
            go = Instantiate(PrefabList[Random.Range(0, PrefabList.Count)], Parent);
            //go.transform.position = BezierCurve(currentT);
            go.transform.rotation = Quaternion.Euler(GetRot());
            go.transform.position = BezierCurve(currentT);
            currentT += increment;
        }
    }
    Vector3 GetRot()
    {
        Vector3 newRot = new Vector3(0, 0, 0);

        newRot.x = Random.Range(0, X_rot)+Starting_rot.x;
        newRot.y = Random.Range(0, Y_rot)+Starting_rot.y;
        newRot.z = Random.Range(0, Z_rot)+Starting_rot.z;

        return newRot;
    }
    void Clear()
    {
        for (int i = Parent.childCount; i > 0; --i)
            DestroyImmediate(Parent.GetChild(0).gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (Instantiate)
        {
            Instantiate = false;
            InstantiatePointsFixed();
        }
        if (Destroy)
        {
            Destroy = false;
            Clear();
        }
    }
    float Factorial(int n)
    {
        int nFact = 1;
        for (int i = 0; i < n; i++)
        {
            nFact = nFact * (n - i);
        }
        return nFact;
    }
    float Binomial(int n, int k)
    {
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    public Vector3 BezierCurve(float t)
    {
        Vector3 BGivenT = Vector3.zero;
        int n = BezierPoints.Count;
        for (int i = 0; i < n; i++)
        {
            BGivenT += Binomial(n - 1, i) * Mathf.Pow((1 - t), n - 1 - i) * Mathf.Pow(t, i) * BezierPoints[i].position;
        }
        return BGivenT;
    }
}

using UnityEngine;
using TMPro;

public class MT : MonoBehaviour
{
    public Transform partA;
    public Transform partB;
    public Transform partC;
    public float dis;
    public float fixedData;
    public string moveData;
    public float maxTranslationSpeed = 10f;
    public float accelerationRate = 10f;
    public float decelerationRate = 10f;
    public float maxDistance;

    [SerializeField]
    private float currentTranslationSpeed = 0f;

    private float maxLength = 7.5f; // 部件最大长度

    public void MoveForward()
    {
        //加一下movedata
        if(dis < 1500)
        {
            dis += 1f;
        }
        
    }

    public void BackForward()
    {
        dis -= 1f;
    }


    void Update()
    {
        fixedData = dis / 100 * -1;
        UpdatePosition();

    }
    void UpdatePosition()
    {





        float lengthB = Mathf.Min(Mathf.Abs(fixedData), maxLength) * Mathf.Sign(fixedData);
        partB.localPosition = new Vector3(0, 0, lengthB);

        float lengthC = Mathf.Clamp(Mathf.Max(Mathf.Abs(fixedData) - maxLength, 0), 0, maxLength) * Mathf.Sign(fixedData);
        partC.localPosition = new Vector3(-0.636806f, 0, lengthC);

    }

}




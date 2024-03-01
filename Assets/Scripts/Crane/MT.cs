using UnityEngine;
using TMPro;

public class MT : MonoBehaviour
{
    public Transform partA;
    public Transform partB;
    public Transform partC;
    public float dis = 750f;
    public float fixedData;
    public string moveData;

    [SerializeField]
    private float currentTranslationSpeed = 0f;

    private float maxLength = 7.5f; // 部件最大长度

    public void MoveForward()
    {
        moveData = "1";
    }

    public void BackForward()
    {
        moveData = "2";
    }

    public void Stop()
    {
        moveData = "0";
    }

    void Update()
    {
       

        if (moveData.Equals("1") && dis <1500)
        {
            dis += 1f;
        }
        else if (moveData.Equals("2") && dis > 50)
        {
            dis -= 1f;
        }
        else
        {

        }

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




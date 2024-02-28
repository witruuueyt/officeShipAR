using UnityEngine;
//using game4automation;
using TMPro;

public class MT : MonoBehaviour
{
    public Transform partA;
    public Transform partB;
    public Transform partC;
    private float maxLength = 7.5f; // ������󳤶�
    public TMP_Text uiFeedbackTMP;

    //public float stopThreshold = 5f;

    public float maxTranslationSpeed = 10f;

    public float accelerationRate = 10f;
    public float decelerationRate = 10f;
    public float maxDistance;
    private Transform myTransform;
    float zPosition;
    [SerializeField]
    private float currentTranslationSpeed = 0f;
    private Vector3 initialPositionB;

    //private bool bStopped = false;
    //private Vector3 lastBPosition; // ����B�����λ��


    [Header("Factory Machine")]
    public string factoryMachineID;
    //public OPCUA_Interface Interface;


    [Header("OPCUA Reader")]
    public string nodeBeingMonitored;
    public string nodeID;

    public string dataFromOPCUANode;
    public float fixedData;
    public string moveNodeID;
    public string moveData;


    void Start()
    {
        //Interface.EventOnConnected.AddListener(OnInterfaceConnected);
        //Interface.EventOnConnected.AddListener(OnInterfaceDisconnected);
        //Interface.EventOnConnected.AddListener(OnInterfaceReconnect);
        //InvokeRepeating("UpdateData", 0f, 0.1f);
        myTransform = GetComponent<Transform>();
        initialPositionB = partB.transform.position;
    }


    //private void OnInterfaceConnected()
    //{
    //    Debug.LogWarning("Connected to Factory Machine " + factoryMachineID);
    //    var subscription = Interface.Subscribe(nodeID, NodeChanged);
    //    dataFromOPCUANode = subscription.ToString();
    //    var subscriptionMove = Interface.Subscribe(moveNodeID, MoveNodeChanged);
    //    moveData = subscription.ToString();
    //}

    //private void OnInterfaceDisconnected()
    //{
    //    Debug.LogWarning("Factory Machine " + factoryMachineID + " has disconnected");
    //}

    //private void OnInterfaceReconnect()
    //{
    //    Debug.LogWarning("Factory Machine " + factoryMachineID + " has reconnected");
    //}

    //public void NodeChanged(OPCUANodeSubscription sub, object value)
    //{
    //    dataFromOPCUANode = value.ToString();
    //    Debug.Log("Factory machine " + factoryMachineID + " just registered " + nodeBeingMonitored + " as " + dataFromOPCUANode);
    //}

    //public void MoveNodeChanged(OPCUANodeSubscription sub, object value)
    //{
    //    moveData = value.ToString();
    //    Debug.Log("Factory machine " + factoryMachineID + " just registered " + nodeBeingMonitored + " as " + dataFromOPCUANode);
    //}

    void Update()
    {
        //Move();
        //WriteValue();
        //uiFeedbackTMP.text = factoryMachineID + ":" + dataFromOPCUANode;
        if (float.TryParse(dataFromOPCUANode, out float parsedData))
        {
            fixedData = parsedData / 100f * -1;
            UpdatePosition();
        }
        else
        {
            //Debug.LogWarning("Failed to parse data from OPC UA node.");
        }
    }
    void UpdatePosition()
    {

        if (moveData.Equals("1") || moveData.Equals("2"))
        {

        }

        else
        {

            float lengthB = Mathf.Min(Mathf.Abs(fixedData), maxLength) * Mathf.Sign(fixedData);
            partB.localPosition = new Vector3(0, 0, lengthB);

            float lengthC = Mathf.Clamp(Mathf.Max(Mathf.Abs(fixedData) - maxLength, 0), 0, maxLength) * Mathf.Sign(fixedData);
            partC.localPosition = new Vector3(-0.636806f, 0, lengthC);
        }
    }

    //public void WriteValue()
    //{
    //    zPosition = transform.position.z * -1;

    //    Interface.WriteNodeValue(nodeID, zPosition);
    //    //Debug.Log(nodeID + dataFromOPCUANode);
    //}

    //public void Move()
    //{
    //    //���յ���moveData��ֵΪ1ʱ����ʼ�ƶ�
    //    if (moveData.Equals("1"))
    //    {
    //        //���ٶ�
    //        currentTranslationSpeed = Mathf.MoveTowards(currentTranslationSpeed, maxTranslationSpeed, accelerationRate * Time.deltaTime);

    //        // ��Z��ƽ��
    //        partB.transform.Translate(Vector3.forward * currentTranslationSpeed * Time.deltaTime * -1);

    //        // ����Ƿ�ﵽ�����룬����ﵽ��������ֹͣ
    //        if (Mathf.Abs((partB.transform.position.z - initialPositionB.z)*-1) >= maxLength)
    //        {
    //            currentTranslationSpeed = 0f;
    //            accelerationRate = 0f;
    //        }
    //    }
    //    else
    //    {
    //        // ����
    //        currentTranslationSpeed = Mathf.MoveTowards(currentTranslationSpeed, 0f, decelerationRate * Time.deltaTime);

    //        // ��Z��ƽ��
    //        partB.transform.Translate(Vector3.forward * currentTranslationSpeed * Time.deltaTime * -1);
    //    }
    //}

}
//----------------------------------------------------------
//void UpdateData()
//{
//    if (float.TryParse(dataFromOPCUANode, out float parsedData))
//    {
//        fixedData = parsedData / 100f;
//        UpdatePosition(fixedData);
//    }
//    else
//    {
//        //Debug.LogWarning("Failed to parse data from OPC UA node.");
//    }
//}

//public void UpdatePosition(float displacement)
//{
//    // ͨ��λ��ֵ����B��λ��
//    partB.localPosition = new Vector3(partB.localPosition.x, partB.localPosition.y, displacement);


//    // ����Ƿ���ҪֹͣB���ƶ�
//    if (!bStopped && Mathf.Abs(fixedData) >= stopThreshold)
//    {
//        // ���B�ƶ��ľ��볬����ֵ��ֹͣB���ƶ�
//        bStopped = true;
//        Debug.Log("Bֹͣ�ƶ�");

//        // �������������Ҫִ�е��߼�
//        lastBPosition = partB.position;
//        // ����C���ƶ�
//        StartMovingPartC();
//    }

//    // ���B�Ѿ�ֹͣ���ƶ�C
//    if (bStopped)
//    {
//        MovePartC(displacement);
//        // ����bStopped״̬��ʹ��B�ܹ���fixedData�ٴ�С��5��ʱ������ƶ�
//        if (Mathf.Abs(fixedData) < stopThreshold)
//        {
//            bStopped = false;
//            Debug.Log("B�����ƶ�");
//        }
//    }
//}

//void StartMovingPartC()
//{
//    partB.localPosition = lastBPosition;

//    Debug.Log("C��ʼ�ƶ�");
//}

//void MovePartC(float displacement)
//{
//    // ͨ��λ��ֵ����C��λ��
//    partC.localPosition = new Vector3(partC.localPosition.x, partC.localPosition.y, displacement);
//    partB.position = lastBPosition;
//}





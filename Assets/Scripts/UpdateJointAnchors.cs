using UnityEngine;

public class UpdateJointAnchors : MonoBehaviour
{
    private Transform[] children;
    private Vector3[] connectedAnchor;
    private Vector3[] anchor;

    void Start()
    {
        // ��ȡ�����Ӷ���
        children = transform.GetComponentsInChildren<Transform>();

        // ��ʼ�����ӵ��ê������
        connectedAnchor = new Vector3[children.Length];
        anchor = new Vector3[children.Length];

        // ����ÿ���Ӷ���
        for (int i = 0; i < children.Length; i++)
        {
            // ����Ӷ�������� Joint ���
            if (children[i].GetComponent<Joint>() != null)
            {
                // �������ӵ��ê��λ��
                connectedAnchor[i] = children[i].GetComponent<Joint>().connectedAnchor;
                anchor[i] = children[i].GetComponent<Joint>().anchor;
                Debug.LogWarning("1");
            }
        }
    }

    private void Update()
    {
        // ����ÿ���Ӷ���
        for (int i = 0; i < children.Length; i++)
        {
            // ����Ӷ�������� Joint ���
            if (children[i].GetComponent<Joint>() != null)
            {
                // �������ӵ��ê��λ��
                children[i].GetComponent<Joint>().connectedAnchor = connectedAnchor[i];
                children[i].GetComponent<Joint>().anchor = anchor[i];

            }
        }
    }
}

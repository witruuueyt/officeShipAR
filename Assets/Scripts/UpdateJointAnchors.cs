using UnityEngine;

public class UpdateJointAnchors : MonoBehaviour
{
    private Transform[] children;
    private Vector3[] connectedAnchor;
    private Vector3[] anchor;

    void Start()
    {
        // 获取所有子对象
        children = transform.GetComponentsInChildren<Transform>();

        // 初始化连接点和锚点数组
        connectedAnchor = new Vector3[children.Length];
        anchor = new Vector3[children.Length];

        // 遍历每个子对象
        for (int i = 0; i < children.Length; i++)
        {
            // 如果子对象包含有 Joint 组件
            if (children[i].GetComponent<Joint>() != null)
            {
                // 保存连接点和锚点位置
                connectedAnchor[i] = children[i].GetComponent<Joint>().connectedAnchor;
                anchor[i] = children[i].GetComponent<Joint>().anchor;
                Debug.LogWarning("1");
            }
        }
    }

    private void Update()
    {
        // 遍历每个子对象
        for (int i = 0; i < children.Length; i++)
        {
            // 如果子对象包含有 Joint 组件
            if (children[i].GetComponent<Joint>() != null)
            {
                // 更新连接点和锚点位置
                children[i].GetComponent<Joint>().connectedAnchor = connectedAnchor[i];
                children[i].GetComponent<Joint>().anchor = anchor[i];

            }
        }
    }
}

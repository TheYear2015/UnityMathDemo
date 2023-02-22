using UnityEngine;
using Unity.Mathematics;

[ExecuteInEditMode]
public class AABBGizmos : MonoBehaviour
{
    private Transform m_point1;
    private Transform m_point2;

    private Vector3 m_pos1;
    private Vector3 m_pos2;
    private float3[] m_AabbPos;

    public Color lineColor = Color.red;
    public Color aabbWireColor = Color.green;

    private void Start()
    {
        InitCell();
    }

    private void OnDrawGizmos()
    {
        InitCell();

        if (m_pos1 != m_point1.position || m_pos2 != m_point2.position)
        {
            m_pos1 = m_point1.position;
            m_pos2 = m_point2.position;
            float3 center = 0.5f * (m_pos1 + m_pos2);
            float3 ext = 0.5f * (m_pos1 - m_pos2);

            float deltaX = math.abs(ext.x);
            float deltaY = math.abs(ext.y);
            float deltaZ = math.abs(ext.z);

            m_AabbPos[0] = center + new float3(-deltaX, deltaY, -deltaZ);        // 上前左（相对于中心点）
            m_AabbPos[1] = center + new float3(deltaX, deltaY, -deltaZ);           // 上前右
            m_AabbPos[2] = center + new float3(deltaX, deltaY, deltaZ);              // 上后右
            m_AabbPos[3] = center + new float3(-deltaX, deltaY, deltaZ);           // 上后左

            m_AabbPos[4] = center + new float3(-deltaX, -deltaY, -deltaZ);     // 下前左
            m_AabbPos[5] = center + new float3(deltaX, -deltaY, -deltaZ);        // 下前右
            m_AabbPos[6] = center + new float3(deltaX, -deltaY, deltaZ);           // 下后右
            m_AabbPos[7] = center + new float3(-deltaX, -deltaY, deltaZ);        // 下后左
        }

        Gizmos.color = aabbWireColor;
        for (int i = 0; i < 4; i++)
        {
            if (i == 3)
            {
                Gizmos.DrawLine(m_AabbPos[3], m_AabbPos[0]);
                Gizmos.DrawLine(m_AabbPos[4], m_AabbPos[7]);
            }
            else
            {
                Gizmos.DrawLine(m_AabbPos[i], m_AabbPos[i + 1]);
                Gizmos.DrawLine(m_AabbPos[4+i], m_AabbPos[i + 5]);
            }
            Gizmos.DrawLine(m_AabbPos[i], m_AabbPos[i + 4]);
        }

        Gizmos.color = lineColor;
        Gizmos.DrawLine(m_pos1, m_pos2);
    }

    private void InitCell()
    {
        if(m_point1 == null)
            m_point1 = transform.GetChild(0);
        if(m_point2 == null)
            m_point2 = transform.GetChild(1);

        if (m_AabbPos == null)
            m_AabbPos = new float3[8];
    }
}

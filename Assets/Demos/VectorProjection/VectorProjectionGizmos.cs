using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways]
public class VectorProjectionGizmos : MonoBehaviour
{
    private Transform m_originPoint;
    private Transform m_pointA;
    private Transform m_pointB;
    private Transform m_pointC;

    private void Start()
    {
        InitCell();
    }

    private void OnDrawGizmos()
    {
        InitCell();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_originPoint.position, m_pointA.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_originPoint.position, m_pointB.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_originPoint.position, m_pointC.position);

        float3 originPos = m_originPoint.position;

        float3 projectPos = math.project(m_pointA.position - m_originPoint.position, m_pointB.position - m_originPoint.position) + originPos;
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(projectPos, 0.5f);
        Gizmos.DrawLine(m_pointA.position, projectPos);

        if(math.dot(projectPos - originPos, m_pointB.position - m_originPoint.position) < 0)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(originPos, projectPos);
        }

        projectPos = math.project(m_pointA.position - m_originPoint.position, m_pointC.position - m_originPoint.position) + new float3(m_originPoint.position);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(projectPos, 0.5f);
        Gizmos.DrawLine(m_pointA.position, projectPos);

        if (math.dot(projectPos - originPos, m_pointC.position - m_originPoint.position) < 0)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawLine(originPos, projectPos);
        }

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void InitCell()
    {
        if (m_originPoint == null)
            m_originPoint = this.transform.Find("Origin").transform;
        if (m_pointA == null)
            m_pointA = this.transform.Find("PointA").transform;
        if (m_pointB == null)
            m_pointB = this.transform.Find("PointB").transform;
        if (m_pointC == null)
            m_pointC = this.transform.Find("PointC").transform;
    }
}

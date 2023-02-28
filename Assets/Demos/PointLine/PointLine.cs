using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[ExecuteAlways]
public class PointLine : MonoBehaviour
{
    private Transform m_lineEnd;
    private Transform m_point;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        InitCell();
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Vector3.zero, m_lineEnd.position);

        if(PosXZJudge2(m_lineEnd.position, m_point.position) < 0)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawLine(Vector3.zero, m_point.position);
    }

    private void InitCell()
    {
        if (m_lineEnd == null)
            m_lineEnd = this.transform.Find("LineEnd").transform;
        if (m_point == null)
            m_point = this.transform.Find("Point").transform;
    }

    private float PosXZJudge(Vector3 pos1, Vector3 pos2)
    {
       return (0 - pos2.x) * (pos1.z - pos2.z) - (0 - pos2.z) * (pos1.x - pos2.x);
    }

    private float PosXZJudge2(Vector3 pos1, Vector3 pos2)
    {
        return pos1.x * pos2.z - pos1.z * pos2.x;
    }
}

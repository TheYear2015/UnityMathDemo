using UnityEngine;
using Unity.Mathematics;

[ExecuteAlways]
public class IntersectLineGizmos : MonoBehaviour
{
    private Transform m_a;
    private Transform m_b;
    private Transform m_c;
    private Transform m_d;

    private void Start()
    {
        InitCell();
    }

    private void OnDrawGizmos()
    {
        InitCell();

        Gizmos.color = Color.green;
        Gizmos.DrawLine(m_a.position, m_b.position);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(m_c.position, m_d.position);

        if (TryGetIntersectPoint(m_a.position, m_b.position, m_c.position, m_d.position, out float3 intersectPos))
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(intersectPos, 0.2f);
        }
    }

    /// <summary>
    /// 计算 A B与 CD 两条线段的交点.
    /// </summary>
    /// <param name="a">A点</param>
    /// <param name="b">B点</param>
    /// <param name="c">C点</param>
    /// <param name="d">D点</param>
    /// <param name="intersectPos"> AB 与 CD 的交点</param>
    /// <returns>是否相交 true:相交 false:未相交</returns>
    private bool TryGetIntersectPoint(float3 a, float3 b, float3 c, float3 d, out float3 intersectPos)
    {
        intersectPos = float3.zero;

        float3 ab = b - a;
        float3 ca = a - c;
        float3 cd = d - c;

        float3 v1 = math.cross(ca, cd);

        if (math.abs(math.dot(v1, ab)) > 1e-6)
        {
            return false;
        }

        if (math.lengthsq(math.cross(ab, cd)) <= 1e-6)
        {
            return false;
        }

        if (math.min(a.x, b.x) > math.max(c.x, d.x) || math.max(a.x, b.x) < math.min(c.x, d.x)
           || math.min(a.y, b.y) > math.max(c.y, d.y) || math.max(a.y, b.y) < math.min(c.y, d.y)
           || math.min(a.z, b.z) > math.max(c.z, d.z) || math.max(a.z, b.z) < math.min(c.z, d.z)
        )
            return false;

        float3 ad = d - a;
        float3 cb = b - c;

        if (math.dot(math.cross(-ca, ab), math.cross(ab, ad)) > 0
            && math.dot(math.cross(ca, cd), math.cross(cd, cb)) > 0)
        {
            float3 v2 = math.cross(cd, ab);
            intersectPos = a + ab * (math.dot(v1, v2) / math.lengthsq(v2));
            return true;
        }

        return false;
    }

    private void InitCell()
    {
        if (m_a == null)
            m_a = this.transform.Find("A").transform;
        if (m_b == null)
            m_b = this.transform.Find("B").transform;
        if (m_c == null)
            m_c = this.transform.Find("C").transform;
        if (m_d == null)
            m_d = this.transform.Find("D").transform;
    }
}

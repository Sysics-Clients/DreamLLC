using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFOV: MonoBehaviour
{
    public DroneBehavior droneBehavior;
    public float radius;
    public float angle;

    public List<Vector3> FovPosition;
    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public float meshResolution;
    public MeshFilter viewMeshFilter;
    public Renderer viewRenderer;
    Mesh viewMesh;
    public int edgeResolveIterations;
    private float yPos=1.3f;
    Renderer renderer;

    private void OnEnable()
    {
        droneBehavior.canSeeThePlayer += getCanSeePlayer;
        droneBehavior.setDroneFovColor += setFovColor;
        droneBehavior.disableOrEnableFieldOfView += DisableOrEnableFieldOfView;
        droneBehavior.disableOrEnableRenderingFov += DisableOrEnableRenderingFov;

    }
    private void OnDisable()
    {
        droneBehavior.canSeeThePlayer -= getCanSeePlayer;
        droneBehavior.setDroneFovColor -= setFovColor;
        droneBehavior.disableOrEnableFieldOfView -= DisableOrEnableFieldOfView;
        droneBehavior.disableOrEnableRenderingFov -= DisableOrEnableRenderingFov;
    }
    public void DisableOrEnableFieldOfView(bool state) { enabled = state; viewRenderer.enabled=!viewRenderer.enabled; }
    public void DisableOrEnableRenderingFov(bool state) { viewRenderer.enabled = state; }
    public bool getCanSeePlayer() { return canSeePlayer; }
    private void Start()
    { 
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        viewRenderer.material.color = new Color(Color.green.r, Color.green.g, Color.green.b, viewRenderer.sharedMaterial.color.a);
        renderer = gameObject.GetComponentInParent<Renderer>();
    }
    private void setFovColor(Color col) {
        col.a = viewRenderer.material.color.a;
        viewRenderer.material.color = col;
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            canSeePlayer= FieldOfViewCheck(radius,angle);
            DrawFieldOfView();
        }
    }

    public bool FieldOfViewCheck(float rad,float angl)
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, rad, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position + new Vector3(0, yPos, 0)).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angl / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position + new Vector3(0, yPos, 0), target.position + new Vector3(0, yPos, 0));

                if (!Physics.Raycast(transform.position , directionToTarget, distanceToTarget, obstructionMask))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else 
        {
            return false;
        }
    }
    void DrawFieldOfView() {
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List< Vector3 > ();
        ViewCastInfo oldViewCast = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++) {
            float angl = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = viewCast(angl);
            if (i > 0) {
                if (oldViewCast.hit != newViewCast.hit) {
                    EdgeInfo edge = findEdge(oldViewCast, newViewCast);
                    if (edge.pointA != Vector3.zero) {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero) {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }


            viewPoints.Add(newViewCast.point);
            oldViewCast = newViewCast;
        
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2)*3];
        vertices[0] =Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint( viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }
    EdgeInfo findEdge(ViewCastInfo minViewCast , ViewCastInfo maxViewCast) {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;
        for (int i = 0; i < edgeResolveIterations; i++) {
            float angle = (minAngle + maxAngle) / 2;
            ViewCastInfo newViewCast = viewCast(angle);
            if (newViewCast.hit = minViewCast.hit)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
            

        }
        return new EdgeInfo(minPoint, maxPoint);
    }
    ViewCastInfo viewCast(float globalAngle) {
        Vector3 dir = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, radius, obstructionMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else {
            return new ViewCastInfo(false, transform.position + dir * radius, radius, globalAngle);
        }


    }

    
    private Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal) { angleInDegrees += transform.eulerAngles.y; }
       

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;
        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
    public struct EdgeInfo
    {
        public Vector3 pointA;
        public Vector3 pointB;
        public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}

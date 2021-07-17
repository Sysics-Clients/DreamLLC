using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public EnemyBehavior enemyBehavior;
    public float radius;
    public float angle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public float meshResolution;
    public MeshFilter viewMeshFilter;
    public Renderer viewRenderer;
    Mesh viewMesh;
    
    private void OnEnable()
    {
        enemyBehavior.canSeeThePlayer += getCanSeePlayer;
        enemyBehavior.setEnemyFovColor += setFovColor;
        enemyBehavior.disableOrEnableFieldOfView += DisableOrEnableFieldOfView;

    }
    private void OnDisable()
    {
        enemyBehavior.canSeeThePlayer -= getCanSeePlayer;
        enemyBehavior.setEnemyFovColor -= setFovColor;
        enemyBehavior.disableOrEnableFieldOfView -= DisableOrEnableFieldOfView;
    }
    public void DisableOrEnableFieldOfView(bool state) { enabled = state; viewRenderer.enabled=false; }
    public bool getCanSeePlayer() { return canSeePlayer; }
    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        radius = enemyBehavior.Item.FovRadius;
        angle = enemyBehavior.Item.FovAngle;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
        viewRenderer.sharedMaterial.color = new Color(Color.green.r, Color.green.g, Color.green.b, viewRenderer.sharedMaterial.color.a);
    }
    private void setFovColor(Color col) {
        col.a = viewRenderer.sharedMaterial.color.a;
        viewRenderer.sharedMaterial.color = col;
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            DrawFieldOfView();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
    void DrawFieldOfView() {
        int stepCount = Mathf.RoundToInt(angle * meshResolution);
        float stepAngleSize = angle / stepCount;
        List<Vector3> viewPoints = new List< Vector3 > ();
        for (int i = 0; i <= stepCount; i++) {
            float angl = transform.eulerAngles.y - angle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = viewCast(angl);
            viewPoints.Add(newViewCast.point);
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2)*3];
        vertices[0] =new Vector3(0,1.3f,0);
        for (int i = 0; i < vertexCount - 1; i++) {
            vertices[i + 1] = transform.InverseTransformPoint( viewPoints[i])+ new Vector3(0, 1.3f, 0);
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
}
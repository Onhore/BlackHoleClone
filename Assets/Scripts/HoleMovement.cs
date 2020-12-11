using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{
    [Header("Hole Mesh")]
    [SerializeField] private MeshFilter MeshFilter;
    [SerializeField] private MeshCollider MeshCollider;

    [Space]

    [Header("Hole center options")]
    [SerializeField] private Transform HoleCenter;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Vector2 MoveLimits;
    [SerializeField] private float Radius;
    [SerializeField] private Transform RotatingCircle;

    [Space]

    [Header("Other options")]
    [SerializeField] private Color GizmosColor;

    private Mesh mesh;
    private List<int> holeVertices;
    private List<Vector3> offsets;
    private int holeVerticesCount;

    private float x;
    private float y;
    private Vector3 touch;
    private Vector3 targetPos;

    private void Start()
    {
        Game.IsGameOver = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = MeshFilter.mesh;

        FindHoleVertices();

        RotateCircle();
    }

    private void Update() 
    {
        #if UNITY_EDITOR
            Game.IsMoving = Input.GetMouseButton(0);
        #else
            Game.IsMoving = Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved;
        #endif

        if (!Game.IsGameOver && Game.IsMoving)
        {
            MoveHole();

            UpdateHoleVerticesPosition();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawWireSphere(HoleCenter.position, Radius);
    }

    private void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(HoleCenter.position, HoleCenter.position + new Vector3(x, 0f, y), MoveSpeed * Time.deltaTime);

        targetPos = new Vector3 // :3
        (
            Mathf.Clamp(touch.x, -MoveLimits.x, MoveLimits.x), 
            touch.y, 
            Mathf.Clamp(touch.z, -MoveLimits.y, MoveLimits.y)
        );

        HoleCenter.position = targetPos;
    }

    private void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = HoleCenter.position + offsets[i];
        }

        mesh.vertices = vertices;
        MeshFilter.mesh = mesh;
        MeshCollider.sharedMesh = mesh;
    }

    private void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            
            float distance = Vector3.Distance(HoleCenter.position, mesh.vertices[i]);

            if (distance < Radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - HoleCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    private void RotateCircle()
    {
        // :3

        RotatingCircle.DORotate(new Vector3(90f, 0f, -90f), 0.2f)
                      .SetEase(Ease.Linear)
                      .From(new Vector3(90f, 0f, 0f))
                      .SetLoops(-1, LoopType.Incremental);
    }
}

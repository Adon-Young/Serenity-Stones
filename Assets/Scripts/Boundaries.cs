using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public GameObject PillarPrefab;
    public GameObject WaterPrefab;
    private float pillarScaleX = 2f;
    private float pillarScaleY = 1f; 
    private float pillarScaleZ = 2f;
    private float waterWidth = 20f;
    private float waterHeight = 0.1f;
    private float waterLength = 10f;
    private float waterPositionY = -1f; 
    void Start()
    {
        // Create the pillar
        CreatePillar();

        // Create the water plane
        CreateWater();
    }

    void CreatePillar()
    {
        // Create the pillar GameObject
        PillarPrefab = new GameObject("Pillar");
        PillarPrefab.transform.position = new Vector3(0, waterPositionY + pillarScaleY / 2, 0);

        // Add components for physics
        MeshFilter pillarMeshFilter = PillarPrefab.AddComponent<MeshFilter>();
        MeshRenderer pillarMeshRenderer = PillarPrefab.AddComponent<MeshRenderer>();
        MeshCollider pillarMeshCollider = PillarPrefab.AddComponent<MeshCollider>();

        // Create the cube mesh for the pillar
        Mesh pillarMesh = CreateCubeMesh();
        pillarMeshFilter.mesh = pillarMesh;

        // Assign a material to the pillar
        pillarMeshRenderer.material = new Material(Shader.Find("Standard"));
        pillarMeshRenderer.material.color = Color.gray;

        // Set the collider to use the generated mesh
        pillarMeshCollider.sharedMesh = pillarMesh;

        // Set pillar scale
        PillarPrefab.transform.localScale = new Vector3(pillarScaleX, pillarScaleY, pillarScaleZ);
    }

    void CreateWater()
    {
        // Create the water GameObject
        WaterPrefab = new GameObject("Water");
        WaterPrefab.transform.position = new Vector3(0, -1, 0); // Position water at the origin

        // Add components for physics
        MeshFilter waterMeshFilter = WaterPrefab.AddComponent<MeshFilter>();
        MeshRenderer waterMeshRenderer = WaterPrefab.AddComponent<MeshRenderer>();
        MeshCollider waterMeshCollider = WaterPrefab.AddComponent<MeshCollider>();

        // Create the flat mesh for the water
        Mesh waterMesh = CreatePlaneMesh();
        waterMeshFilter.mesh = waterMesh;

        // Assign a material to the water
        waterMeshRenderer.material = new Material(Shader.Find("Standard"));
        waterMeshRenderer.material.color = Color.blue;

        // Set the collider to use the generated mesh
        waterMeshCollider.sharedMesh = waterMesh;

        // Set water scale
        WaterPrefab.transform.localScale = new Vector3(waterWidth, waterHeight, waterLength);
    }

    Mesh CreateCubeMesh()
    {
        Mesh mesh = new Mesh();

        // Create vertices for a cube
        Vector3[] vertices = {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f)
        };

        // Create triangles for the cube
        int[] triangles = {
            0, 2, 1, 0, 3, 2, // Front face
            4, 5, 6, 4, 6, 7, // Back face
            0, 1, 5, 0, 5, 4, // Bottom face
            2, 3, 7, 2, 7, 6, // Top face
            0, 4, 7, 0, 7, 3, // Left face
            1, 2, 6, 1, 6, 5  // Right face
        };

        // Create UV coordinates for the cube
        Vector2[] uv = {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }

    Mesh CreatePlaneMesh()
    {
        Mesh mesh = new Mesh();

        // Create vertices for a flat plane
        Vector3[] vertices = {
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, -0.5f),
            new Vector3(0.5f, 0, 0.5f),
            new Vector3(-0.5f, 0, 0.5f),
        };

        // Create triangles for the plane
        int[] triangles = {
            0, 2, 1,
            0, 3, 2,
        };

        // Create UV coordinates
        Vector2[] uv = {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1),
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();

        return mesh;
    }
}

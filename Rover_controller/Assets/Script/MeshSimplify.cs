using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class MeshSimplify : MonoBehaviour
{
    public float quality = 0.5f;


    void Start()
    {
        var originalMesh = GetComponent<MeshFilter>().sharedMesh;
        var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
        meshSimplifier.Initialize(originalMesh);
        meshSimplifier.SimplifyMesh(quality);
        var destMesh = meshSimplifier.ToMesh();
        GetComponent<MeshFilter>().sharedMesh = destMesh;
    }
}
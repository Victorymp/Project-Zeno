using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveManager : MonoBehaviour
{
    MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        //meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3[] vertices = meshFilter.mesh.vertices;
        //for (int i = 0; i < vertices.Length; i++)
        //{
            //vertices[i].y = WaterLogic.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        //}

    }
}

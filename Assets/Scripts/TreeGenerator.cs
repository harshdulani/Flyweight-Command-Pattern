using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGenerator : MonoBehaviour
{
    public static Action<int> treesGenerated = delegate { };
    
    [SerializeField] private GameObject ground, treePrefab;

    [SerializeField] private Color leavesColorLight, leavesColorDark;
    [SerializeField] private float minDistance, maxDistance;
    [SerializeField] private float zVariance, scaleVariance;

    private float _halfWidth;

    private void Start()
    {
        if(!ground) return;
        
        _halfWidth = ground.transform.localScale.x / 2;
        Generate();
    }

    private void Generate()
    {
        int count = 0;
        var parent = new GameObject();
        for (float x = -_halfWidth; x < _halfWidth; x += Random.Range(minDistance, maxDistance))
        {
            for (float z = -_halfWidth; z < _halfWidth; z += 3f)
            {
                var position = new Vector3(x, 0.25f, z + Random.Range(-zVariance, zVariance));
                
                if(position.x > _halfWidth - zVariance || position.z > _halfWidth - zVariance) continue;
                
                var rotation = Quaternion.AngleAxis(Random.Range(-180f, 180f), Vector3.up);
                var tree = Instantiate(treePrefab, position, rotation);

                tree.transform.localScale = Vector3.one * (1 + Random.Range(0, scaleVariance) / 10f);

                tree.transform.parent = parent.transform;
                
                var props1 = new MaterialPropertyBlock();
                var props2 = new MaterialPropertyBlock();
                props1.SetColor("_Color", Color.Lerp(leavesColorLight, leavesColorDark, Random.value));
                props2.SetColor("_Color", Color.Lerp(leavesColorLight, leavesColorDark, Random.value));
                
                MeshRenderer renderer = tree.GetComponent<MeshRenderer>();
                renderer.SetPropertyBlock(props1, 2);

                count++;
            }
        }
        treesGenerated?.Invoke(count);
    }
}
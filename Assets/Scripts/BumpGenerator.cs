using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public sealed class BumpGenerator : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;

    [field:SerializeField] public int Repeat { get; set; } = 20;
    [field:SerializeField] public float3 Extent { get; set; } = 1;
    [field:SerializeField] public float3 Rotation { get; set; } = 10;
    [field:SerializeField] public uint Seed { get; set; } = 1234;

    void Start()
      => GenerateBumps();

    void GenerateBumps()
    {
        var seed = Seed + (uint)transform.position.z;
        var rand = new Random(seed);
        rand.NextUInt4();

        for (var i = 0; i < Repeat; i++)
        {
            var instance = Instantiate(_prefab, transform);
            var xform = instance.GetComponent<Transform>();

            var p = rand.NextFloat3(-0.5f, 0.5f) * Extent;
            var a = rand.NextFloat3(-0.5f, 0.5f) * Rotation;
            var r = quaternion.Euler(math.radians(a));

            xform.localPosition = p;
            xform.localRotation = r;
        }
    }
}

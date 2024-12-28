using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public sealed class BumpGenerator : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;

    [field:SerializeField] public float2 Extent { get; set; }
    [field:SerializeField] public int BumpCount { get; set; }
    [field:SerializeField] public float BumpCountInc { get; set; }
    [field:SerializeField] public float Height { get; set; }
    [field:SerializeField] public float HeightInc { get; set; }
    [field:SerializeField] public float Undulation { get; set; }
    [field:SerializeField] public float UndulationInc { get; set; }
    [field:SerializeField] public uint Seed { get; set; }

    void Start()
    {
        var prog = Mathf.RoundToInt(transform.position.z);

        var bumpCount = (int)(BumpCount + BumpCountInc * prog);
        var height = Height + HeightInc * prog;
        var undulation = Undulation + UndulationInc * prog;

        var p_min = math.float3(-Extent.x, 0, -Extent.y);
        var p_max = math.float3(Extent.x, height, Extent.y);

        var r_min = math.radians(math.float3(-undulation, -180, -undulation));
        var r_max = math.radians(math.float3( undulation,  180,  undulation));

        var rand = new Random(Seed + (uint)prog);
        rand.NextUInt4(); // warming up

        for (var i = 0; i < bumpCount; i++)
        {
            var instance = Instantiate(_prefab, transform);
            var xform = instance.GetComponent<Transform>();
            xform.localPosition = rand.NextFloat3(p_min, p_max);
            xform.localRotation = quaternion.Euler(rand.NextFloat3(r_min, r_max));
        }
    }
}

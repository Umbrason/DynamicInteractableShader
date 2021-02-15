

float SqrDst(float3 A, float3 B)
{
    return (A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y) + (A.z - B.z) * (A.z - B.z);
}

SamplerState my_point_clamp_sampler;
void GetNearestPosition_float(float interactors, Texture2D positionTexture, float3 position, out float3 Out)
{
    float4 sampled = positionTexture.SampleLevel(my_point_clamp_sampler,float2(.5 / interactors, 0),0);
    float3 closest = sampled;
    float closestSqrDst = SqrDst(closest, position);
    for(int i = 1; i < interactors; i++)
    {
        float4 sampled = positionTexture.SampleLevel(my_point_clamp_sampler,float2((i+.5) / interactors, 0),0);
        float3 cur = sampled;
        float sqrDst = SqrDst(cur, position);
        if(closestSqrDst >= sqrDst) {
            closest = cur;
            closestSqrDst = sqrDst;
        }
    }
    Out = closest;
}


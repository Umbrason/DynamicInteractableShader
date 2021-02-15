

float SqrDst(float4 A, float3 Pos)
{
    return (A.x - Pos.x) * (A.x - Pos.x) + (A.y - Pos.y) * (A.y - Pos.y) + (A.z - Pos.z) * (A.z - Pos.z);
}

SamplerState my_point_clamp_sampler;
void GetNearestPosition_float(float interactors, Texture2D positionTexture, float3 position, out float4 Out)
{
    float4 cur = positionTexture.SampleLevel(my_point_clamp_sampler,float2(.5 / interactors, 0),0);
    float4 closest = cur;
    float closestSqrDst = SqrDst(closest, position) / cur.w / cur.w;
    for(int i = 1; i < interactors; i++)
    {
        cur = positionTexture.SampleLevel(my_point_clamp_sampler,float2((i+.5) / interactors, 0),0);        
        float sqrDst = SqrDst(cur, position) / cur.w / cur.w;
        if(closestSqrDst >= sqrDst) {
            closest = cur;
            closestSqrDst = sqrDst;
        }
    }
    Out = closest;
}


#include <Packages/com.blendernodesgraph.core/Editor/Includes/Importers.hlsl>

void Clouds_float(float3 _POS, float3 _PVS, float3 _PWS, float3 _NOS, float3 _NVS, float3 _NWS, float3 _NTS, float3 _TWS, float3 _BTWS, float3 _UV, float3 _SP, float3 _VVS, float3 _VWS, out float4 Color, out float3 Normal, out float Smoothness, out float4 Emission, out float AmbientOcculusion, out float Metallic, out float4 Specular)
{
	
	float _SimpleNoiseTexture_11152_fac; float4 _SimpleNoiseTexture_11152_col; node_simple_noise_texture_full(float4(_PWS, 0), 0, 10, 7, 0.4, 5, 1, _SimpleNoiseTexture_11152_fac, _SimpleNoiseTexture_11152_col);
	float4 _BrightContrast_11136 = brightness_contrast(_SimpleNoiseTexture_11152_fac, 0.3, 0);

	Color = _BrightContrast_11136;
	Normal = float3(0.0, 0.0, 0.0);
	Smoothness = 0.0;
	Emission = float4(0.0, 0.0, 0.0, 0.0);
	AmbientOcculusion = 0.0;
	Metallic = 0.0;
	Specular = float4(0.0, 0.0, 0.0, 0.0);
}
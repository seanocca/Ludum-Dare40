// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// NEED THIS FOR QUBICLE
// DO NOT DELETE
// SHADER IS TO BE DRAGGED ONTO MATERIAL
// MATERIAL TO BE DRAGGED ONTO MODEL

Shader "Custom/VertexColor" {
    Properties {
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
 
            struct vertexInput {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };
            struct vertexOutput {
                float4 pos : SV_POSITION;
                float4 vertCol : COLOR;
            };
 
            vertexOutput vert(vertexInput v){
                vertexOutput o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.vertCol = v.color;
                return o;
            }
 
            float4 frag(vertexOutput i) : COLOR
            {
                return i.vertCol;
            }
            ENDCG
        }
    }
}
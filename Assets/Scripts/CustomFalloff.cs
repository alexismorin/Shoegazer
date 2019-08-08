using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class CustomFalloff : MonoBehaviour
{
    public void OnEnable()
    {
        Lightmapping.RequestLightsDelegate testDel = (Light[] requests, Unity.Collections.NativeArray<LightDataGI> lightsOutput) =>
        {
            DirectionalLight dLight = new DirectionalLight();
            PointLight point = new PointLight();
            SpotLight spot = new SpotLight();
            RectangleLight rect = new RectangleLight();
            LightDataGI ld = new LightDataGI();

            for (int i = 0; i < requests.Length; i++)
            {
                Light l = requests[i];
                switch (l.type)
                {
                    case UnityEngine.LightType.Directional: LightmapperUtils.Extract(l, ref dLight); ld.Init(ref dLight); break;
                    case UnityEngine.LightType.Point: LightmapperUtils.Extract(l, ref point); ld.Init(ref point); break;
                    case UnityEngine.LightType.Spot: LightmapperUtils.Extract(l, ref spot); ld.Init(ref spot); break;
                    case UnityEngine.LightType.Area: LightmapperUtils.Extract(l, ref rect); ld.Init(ref rect); break;
                    default: ld.InitNoBake(l.GetInstanceID()); break;
                }

                ld.falloff = FalloffType.InverseSquared;
                lightsOutput[i] = ld;
            }
        };

        Lightmapping.SetDelegate(testDel);
    }

    void OnDisable()
    {
        Lightmapping.ResetDelegate();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightManager : MonoBehaviour
{
    struct GameLight
    {
        public float minIntensity, maxIntensity, variationSpeed, currentIntensity, targetIntensity;
        //public bool increasingIntensity;

        public Light pointLight, spotLight;

        public GameLight(Transform parent)
        {
            pointLight = parent.GetChild(0).GetComponent<Light>();
            spotLight = parent.GetChild(1).GetComponent<Light>();

            minIntensity = 0.0f;
            maxIntensity = 0.14f;
            variationSpeed = 140f;
            //increasingIntensity = true;

            currentIntensity = pointLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }

    public GameObject lightsGameObject;
    List<GameLight> lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = new List<GameLight>();
        foreach (Transform transform in lightsGameObject.transform)
        {
            lights.Add(new GameLight(transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            GameLight light = lights[i];
            // Update current intensity towards target intensity
            light.currentIntensity = Mathf.MoveTowards(light.currentIntensity, light.targetIntensity, light.variationSpeed * Time.deltaTime);

            // Apply current intensity to the light
            //light.pointLight.intensity = light.currentIntensity;
            //light.spotLight.intensity = light.currentIntensity;

            // If current intensity reaches the target, set a new target
            if (Mathf.Approximately(light.currentIntensity, light.targetIntensity))
            {
                light.targetIntensity = Random.Range(light.minIntensity, light.maxIntensity);
            }
        }
    }
}

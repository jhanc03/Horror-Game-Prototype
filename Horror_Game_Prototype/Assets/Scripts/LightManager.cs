using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightManager : MonoBehaviour
{
    class GameLight
    {
        public float minIntensity, maxIntensity, variationSpeed, currentIntensity, targetIntensity;
        public bool increasingIntensity;

        public Light pointLight, spotLight;

        public GameLight(Transform parent)
        {
            pointLight = parent.GetChild(0).GetComponent<Light>();
            spotLight = parent.GetChild(1).GetComponent<Light>();

            minIntensity = 0.0f;
            maxIntensity = 0.14f;
            variationSpeed = 0.84f;
            increasingIntensity = false;

            currentIntensity = pointLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }

    public GameObject lightsGameObject;
    List<GameLight> lights;
    GameLight light;

    // Start is called before the first frame update
    void Start()
    {
        lights = new List<GameLight>();
        foreach (Transform transform in lightsGameObject.transform)
        {
            light = new GameLight(transform);
            lights.Add(new GameLight(transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            light = lights[i];
            // Update current intensity towards target intensity
            lights[i].currentIntensity = Mathf.MoveTowards(lights[i].currentIntensity, lights[i].targetIntensity, lights[i].variationSpeed * Time.deltaTime);

            // Apply current intensity to the lights[i]
            lights[i].pointLight.intensity = lights[i].currentIntensity;
            lights[i].spotLight.intensity = lights[i].currentIntensity;

            // If current intensity reaches the target, set a new target
            if (Mathf.Approximately(lights[i].currentIntensity, lights[i].targetIntensity))
            {
                if (!lights[i].increasingIntensity)
                {
                    lights[i].targetIntensity = Random.Range(lights[i].currentIntensity, lights[i].maxIntensity);
                }
                else
                {
                    lights[i].targetIntensity = Random.Range(lights[i].minIntensity, lights[i].currentIntensity);
                }
                lights[i].increasingIntensity = !lights[i].increasingIntensity;
            }
        }
    }
}

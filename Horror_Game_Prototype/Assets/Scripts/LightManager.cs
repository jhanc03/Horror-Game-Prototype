using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightManager : MonoBehaviour
{
    class GameLight
    {
        public float minIntensity, maxIntensity, variationSpeed, currentIntensity, targetIntensity;
        public bool increasingIntensity, on;

        public Light pointLight, spotLight;

        public GameLight(Transform parent, bool on)
        {
            pointLight = parent.GetChild(0).GetComponent<Light>();
            spotLight = parent.GetChild(1).GetComponent<Light>();

            minIntensity = 0.0f;
            maxIntensity = 0.14f;
            variationSpeed = 0.84f;
            increasingIntensity = false;
            this.on = on;

            currentIntensity = pointLight.intensity;
            targetIntensity = Random.Range(minIntensity, maxIntensity);
        }
    }

    public GameObject lightsGameObject;
    List<GameLight> lights;

    // Start is called before the first frame update
    void Start()
    {
        lights = new List<GameLight>
        {
            new GameLight(GameObject.Find("LDoor Light").transform, false),
            new GameLight(GameObject.Find("RDoor Light").transform, false)
        };

        foreach (Transform transform in lightsGameObject.transform)
        {
            lights.Add(new GameLight(transform, true));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            if (lights[i].on && !MainMenuScript.flashingLights)
            {
                lights[i].currentIntensity = Mathf.MoveTowards(lights[i].currentIntensity, lights[i].targetIntensity, lights[i].variationSpeed * Time.deltaTime);

                lights[i].pointLight.intensity = lights[i].currentIntensity;
                lights[i].spotLight.intensity = lights[i].currentIntensity;

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

    public void ToggleLDoorLight()
    {
        lights[0].on = !lights[0].on;
        if (lights[0].on)
        {
			lights[0].pointLight.intensity = 0.14f;
			lights[0].spotLight.intensity = 0.14f;
		}
        else
        {
			lights[0].pointLight.intensity = 0.0f;
			lights[0].spotLight.intensity = 0.0f;
		}
    }
    public void ToggleRDoorLight()
    {
        lights[1].on = !lights[1].on;
		if (lights[1].on)
		{
			lights[1].pointLight.intensity = 0.14f;
			lights[1].spotLight.intensity = 0.14f;
		}
		else
		{
			lights[1].pointLight.intensity = 0.0f;
			lights[1].spotLight.intensity = 0.0f;
		}
	}

    public void LightsOff()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].pointLight.intensity = 0.0f;
            lights[i].spotLight.intensity = 0.0f;
            lights[i].on = false;
        }
    }
}

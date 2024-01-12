using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    new Light light;
    int rand;
    float onTime;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    public void StartFlicker(float case1OnTime, float case2OnTime, float case3OnTime, float intensity, float offTime, bool toggle)
    {
        StartCoroutine(Flicker(case1OnTime,  case2OnTime,  case3OnTime,  intensity,  offTime, toggle));
    }
    public IEnumerator Flicker(float case1OnTime, float case2OnTime, float case3OnTime, float intensity, float offTime, bool toggle)
    {
        while(toggle)
        {
            rand = Random.Range(1, 4);
            onTime = rand == 1 ? case1OnTime : rand == 2 ? case2OnTime : case3OnTime;

            light.intensity = intensity;
            yield return new WaitForSeconds(onTime);
            light.intensity = intensity / 5;
            yield return new WaitForSeconds(offTime);
            light.intensity = intensity;
            yield return new WaitForSeconds(onTime);
            light.intensity = intensity / 5;
        }
    }
}

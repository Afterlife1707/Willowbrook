using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] AudioSource lightSound;
    [SerializeField] float case1OnTime, case2OnTime, case3OnTime;
    [SerializeField] bool toggle;
    float onTime;
    [SerializeField] float offTime;
    [SerializeField] float intensity = 0.8f;
    [SerializeField] List<GameObject> lights;
    [SerializeField] GameObject blocker;
    private int rand;
    public static Action TriggerFlicker;

    private void OnEnable()
    {
        TriggerFlicker += StartFlicker;
    }
    private void OnDisable()
    {
        TriggerFlicker -= StartFlicker;
    }

    void StartFlicker()
    {
        toggle = true;
        blocker.SetActive(true);
        foreach (GameObject light in lights)
        {
            light.GetComponent<Lights>().StartFlicker(case1OnTime, case2OnTime, case3OnTime, intensity, offTime, toggle);
        }
        StartCoroutine(PlayFlickerSound());
    }

    IEnumerator PlayFlickerSound()
    {
        while(toggle)
        {
            rand = UnityEngine.Random.Range(1, 4);
            onTime = rand == 1 ? case1OnTime : rand == 2 ? case2OnTime : case3OnTime;

            yield return new WaitForSeconds(onTime);
            lightSound.Stop();
            yield return new WaitForSeconds(offTime);
            lightSound.Play();
            yield return new WaitForSeconds(onTime);
        }
    }

}





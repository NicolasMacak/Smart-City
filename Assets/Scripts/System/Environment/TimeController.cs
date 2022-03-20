using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    public float timeMultiplier;

    [SerializeField]
    public float startHour;

    [SerializeField]
    public TextMeshProUGUI timeText;

    [SerializeField]
    public Light sunLight;

    [SerializeField]
    public float sunriseHour;

    [SerializeField]
    public float sunsetHour;

    [SerializeField]
    public Color dayAmbientLight;

    [SerializeField]
    public Color nightAmbientLight;

    [SerializeField]
    public AnimationCurve lightChangeCurve;

    [SerializeField]
    public float maxSunlightIntensity;

    [SerializeField]
    public Light moonLight;

    [SerializeField]
    public float maxMoonLightIntensity;

    public LightsController lightsController;

    public DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    //public enum LightMode
    //{
    //    Day,
    //    Night
    //}

    private bool isDay;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        isDay = IsEvening();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();

        if(IsEvening() != isDay) // day or night just started
        {
            ExecuteDayNightCycleDuties();
        }
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm:ss");
        }
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunlightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
 
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        } else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinseSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinseSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            //Debug.Log("NOC: " + percentage);
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24); //difference = toTime - fromTime;
        }

        return difference;
    }

    private bool IsEvening()
    {
        return currentTime.Hour >= sunriseHour && currentTime.Hour <= sunsetHour - 2; // ludia zacinaju svietit aj skor
    }

    private void ExecuteDayNightCycleDuties()
    {
        isDay = !isDay;
        if (!isDay)
        {
            lightsController.TurnOnBuildingLights();
        }
    }
}

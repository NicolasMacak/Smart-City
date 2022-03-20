using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class LightsController : MonoBehaviour
{

    public TimeController timeController;

    public void TurnOnBuildingLights()
    {
        GameObject housesParrentObject = GameObject.Find(CONSTANTS.STRUCTURE_CONTAINER.HOUSES);

        for(int i=0; i < housesParrentObject.transform.childCount; i++)
        {
            Transform house = housesParrentObject.transform.GetChild(i);
            StartCoroutine(TurnOnLightsAndSetTurnOffTime(house));
        }

        /*
         offset = random
        NightDuration = random

        LightDUration = nieci - offset
        lightFrom = offset+lightDuration

        To kedy sa zaznu svetla sa initne z Time controllera

        Na tych co to potiahnu do 22
        Tych co do 11:30
        Tych co do 02 00 
        A tych co do 04 00
         */
    }

    private IEnumerator TurnOnLightsAndSetTurnOffTime(Transform house)
    {
        BuildingLightsManager buildingLithgs = house.transform.GetComponentInChildren<BuildingLightsManager>();

        buildingLithgs.SwitchOnLights();
        int sleepHour = GetRandomSleepHour();
        //Debug.Log(sleepHour);

        while (timeController.currentTime.Hour < sleepHour)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(2.5f);
 
        buildingLithgs.SwitchOffLights();
    }

    private int GetRandomSleepHour()
    {
        System.Random r = new System.Random();
        int random = r.Next(0, 100);

        if(random > 70) // 20% do 2
        {
            return 2;
        }
        else if(random > 40) // 30% do 22
        {
            return 22;
        }
        else
        {
            return 23; // 40% do 23 
        }
    }
}

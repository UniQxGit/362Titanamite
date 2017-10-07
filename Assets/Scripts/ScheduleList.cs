using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleList : MonoBehaviour {
    public TextMesh previousText;
    public TextMesh currentText;
    public TextMesh nextText;

    public GameObject[] dayLists;
    public string[] dayNames = {"Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"};
    
    
    private int currentDay;
    
	// Use this for initialization
	void Start () {
        currentDay = 0;
        PopulateList(); //Implement Later
	}

    void PopulateList()
    {
        //PopulateList

        //Set to Monday
        foreach(GameObject d in dayLists)
        {
            d.SetActive(false);
        }

        dayLists[currentDay].SetActive(true);
        currentText.text = dayNames[currentDay];
        previousText.text = "";
        nextText.text = dayNames[currentDay + 1];
    }

    void NextDay()
    {
        if(currentDay >=6)
            return;

        dayLists[currentDay].SetActive(false);
        dayLists[currentDay + 1].SetActive(true);

        previousText.text = dayNames[currentDay];
        currentText.text = dayNames[currentDay+1];
        if (currentDay < 5)
        {
            nextText.text = dayNames[currentDay + 2];
        }
        else
        {
            nextText.text = "";
        }

        currentDay++;
    }

    void PreviousDay()
    {
        if (currentDay == 0)
            return;
        
        dayLists[currentDay].SetActive(false);
        dayLists[currentDay - 1].SetActive(true);


        currentText.text = dayNames[currentDay - 1];
        nextText.text = dayNames[currentDay];

        if (currentDay > 1)
        {
            previousText.text = dayNames[currentDay - 2];
        }
        else
        {
            previousText.text = "";
        }

        currentDay--;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

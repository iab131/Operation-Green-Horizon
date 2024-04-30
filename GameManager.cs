using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gold;
    public int water;
    public int CO2initial;
    public int CO2final;
    public float CO2;
    public int electricity;

    public int goldCost;
    public int waterCost;
    public int electricCost;
    public int CO2Cost;
   
    public float CO2GainByMine;
    public int CO2Loss;
    public int goldGain;
    public int electricityPerSolar;
    //public int electricityPerWind;
    public int waterProduce;
    public int waterNeed;
    public int electricityNeed;
    [SerializeField] private float airWaitTime = 2f;
    [SerializeField] private float goldWaitTime = 0.5f;
    

    private int waterBuilding = 0;
    private int CO2Building;
    private int goldBuilding;
    private int solarBuilding = 0;
    private int windBuilding = 0;

    private bool popUpShown = false;

    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI waterDisplay;
    public TextMeshProUGUI electricityDisplay;
    public TextMeshProUGUI CO2Display;

    public PopUpSystem pop;
    //public CustomCursor customCursor;

    //public Tile[] tiles;
    public Image progressBar;
    public TextMeshProUGUI progressPercentage;

    public ProgressHover progressBarScript;
    public Toggle progressToggle;
    // Start is called before the first frame update
    void Start()
    {
        PopUpSystem pop = GameObject.Find("UI Control").GetComponent<PopUpSystem>();

        StartCoroutine(UpdateGoldDisplay());
        StartCoroutine(UpdateCO2Display());

        electricityDisplay.text = electricity.ToString() + " kWh";
        waterDisplay.text = water.ToString() + " ML";
    }

    // Update is called once per frame
    void Update()
    {
        CO2Building = GlobalVariable.numCO2FilterBuiling;
        goldBuilding = GlobalVariable.numGoldMineBuiling;
        CheckGameEnd();
        Progress();
        progressBarScript.SetGameObjectActive(progressToggle.isOn);
    }
    IEnumerator UpdateGoldDisplay()
    {
        while (true)
        {
            Gold();
            goldDisplay.text = gold.ToString();

            yield return new WaitForSeconds(goldWaitTime / GlobalVariable.speedInGame);
        } 
    }
    IEnumerator UpdateCO2Display()
    {
        while (true)
        {
            if (CO2Building * waterNeed <= water && CO2Building * electricityNeed <= electricity)
            {
                AirFilter();
                CO2Display.text = ((int)CO2).ToString() + " ppm";

                popUpShown = false;

                yield return new WaitForSeconds(airWaitTime / GlobalVariable.speedInGame);
            }
            else
            {
                if(!popUpShown)
                {
                    if (CO2Building * waterNeed > water && CO2Building * electricityNeed > electricity)
                    {
                        pop.PopUp("Not Enough Water and electricity For CO2 Filters");
                    }
                    else if (CO2Building * electricityNeed > electricity)
                    {
                        pop.PopUp("Not Enough Electricity for CO2 Filters");
                    }
                    else
                    {
                        pop.PopUp("Not Enough Water for CO2 Filters");
                    }
                    popUpShown = true;
                }
                CO2Display.text = ((int)CO2).ToString() + " ppm";
                yield return null;
            }
        }
    }
    void Gold()
    {
        gold += (int) (goldBuilding * goldGain * goldWaitTime);
        CO2 += goldBuilding * CO2GainByMine * goldWaitTime;
    }

    void AirFilter()
    {
        CO2 -= CO2Building * CO2Loss * airWaitTime;
    }

    public void DisplayWater()
    {
        waterBuilding++;
        GlobalVariable.numWaterFilterBuiling++;
        water = waterProduce * waterBuilding;
        waterDisplay.text = water.ToString() + " ML";
    }
    public void DisplayElectricity()
    {
        solarBuilding ++;
        GlobalVariable.numElectricBuilding++;
        electricity = electricityPerSolar * solarBuilding;//plus the wind one
        electricityDisplay.text = electricity.ToString() + " kWh";
    }

    void CheckGameEnd()
    {
        if (CO2 >= CO2initial)
        {
            pop.PopUp("Condolences! \nYou worsened the earth!");
            GlobalVariable.gameFinished = true;
            StopAllCoroutines();
        }
        if (CO2 <= CO2final)
        {
            pop.PopUp("Congratulations! \nyou saved the earth!");
            GlobalVariable.gameFinished = true;
            StopAllCoroutines();
        }
    }

    void Progress()
    {
        progressBar.fillAmount = (CO2initial - CO2) / (CO2initial - CO2final);
        //Debug.Log((CO2initial - CO2) / (CO2initial - CO2final));
        progressPercentage.text = ((int)((CO2initial - CO2) / (CO2initial - CO2final)*100f)).ToString() + "%";
    }
    //public void BuyBuilding(Building building)
    //{
    //    if (gold >= building.cost)
    //    {
    //        customCursor.gameObject.SetActive(true);
    //        customCursor.GetComponent<SpriteRenderer>().sprite = building.GetComponent<SpriteRenderer>().sprite;
    //        Cursor.visible = false;

    //        gold -= building.cost;
    //        buildingToPlace = building;
    //        grid.SetActive(true);
    //    }
    //}
}
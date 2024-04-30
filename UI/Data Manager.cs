using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{

    public TextMeshProUGUI CO2Num;
    public TextMeshProUGUI waterNum;
    public TextMeshProUGUI goldNum;
    public TextMeshProUGUI solarNum;

    public TextMeshProUGUI CO2Produce;
    public TextMeshProUGUI waterProduce;
    public TextMeshProUGUI goldProduce;
    public TextMeshProUGUI solarProduce;

    public TextMeshProUGUI CO2Consume1;
    public TextMeshProUGUI CO2Consume2;
    public TextMeshProUGUI waterConsume;
    public TextMeshProUGUI goldConsume;
    public TextMeshProUGUI solarConsume;

    public GameManager gameManager;

    private static DataManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    

    private void Building()
    {
        CO2Num.text = GlobalVariable.numCO2FilterBuiling.ToString();
        waterNum.text = GlobalVariable.numWaterFilterBuiling.ToString();
        goldNum.text = GlobalVariable.numGoldMineBuiling.ToString();
        solarNum.text = GlobalVariable.numElectricBuilding.ToString();
    }

    private void Produce()
    {
        CO2Produce.text = (-gameManager.CO2Loss * GlobalVariable.numCO2FilterBuiling).ToString() + " ppm/s";
        waterProduce.text = (gameManager.waterProduce * GlobalVariable.numWaterFilterBuiling).ToString() + " ML";
        goldProduce.text = (gameManager.goldGain * GlobalVariable.numGoldMineBuiling).ToString() +" G/S";
        solarProduce.text = (gameManager.electricityPerSolar * GlobalVariable.numElectricBuilding).ToString() + " kwh";
    }
    private void Consume()
    {
        CO2Consume1.text = (gameManager.waterNeed * GlobalVariable.numCO2FilterBuiling).ToString() + " ML";
        CO2Consume2.text = (gameManager.electricityNeed * GlobalVariable.numCO2FilterBuiling).ToString() + " kwh";
        waterConsume.text = "0";//(gameManager.waterProduce * GlobalVariable.numWaterFilterBuilding).ToString() + " ML";
        goldConsume.text = (gameManager.CO2GainByMine * GlobalVariable.numGoldMineBuiling).ToString() + " ppm/s";
        solarConsume.text = "0";//(gameManager.electricityPerSolar * GlobalVariable.numElectricBuilding).ToString() + " kwh";
    }

    public static void UpdateValue()
    {
        instance.Building();
        instance.Produce();
        instance.Consume();
    }
}

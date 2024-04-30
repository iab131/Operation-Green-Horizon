using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static bool isBuilding;
    public static bool buildingWaterFilter;
    public static bool buildingCO2Filter;
    public static bool buildingGoldMine;
    public static bool buildingElectricGenerator;

    public static int numWaterFilterBuiling = 0;
    public static int numCO2FilterBuiling = 0;
    public static int numGoldMineBuiling = 0;
    public static int numElectricBuilding = 0;

    public static bool gameFinished;

    public static int speedInGame = 1;

    public static bool popping;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

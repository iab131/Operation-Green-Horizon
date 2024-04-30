using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiles : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    private Color newColor = new Color(1f, 1f, 1f, 0.39f);
    private Color originalColor;
    private Transform childTransform;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer highlightSpriteRenderer;

    private string tileName;
    private int x, y;

    public float minScale = 0.5f; // Minimum scale value
    public float maxScale = 1.0f; // Maximum scale value
    public float duration = 1.0f; // Duration of each breath (in seconds)

    private bool taken = false;


    [SerializeField] private Texture2D waterFilter;
    [SerializeField] private Texture2D goldMine;
    [SerializeField] private Texture2D CO2Filter;
    [SerializeField] private Texture2D electricGenerator;

    public GameManager gameManager;
    public PopUpSystem pop;
    // Start is called before the first frame update
    void Start()
    {
        childTransform = highlight.transform;

        tileName = gameObject.name;
        EvenOrOdd(tileName, out x, out y);

        highlightSpriteRenderer = highlight.GetComponent<SpriteRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject scriptAGameObject = GameObject.Find("GameManager");
        gameManager = scriptAGameObject.GetComponent<GameManager>();
        PopUpSystem pop = GameObject.Find("UI Control").GetComponent<PopUpSystem>();

        originalColor = highlightSpriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        Selecting();
    }


    void OnMouseEnter()
    {
        if (!taken && GlobalVariable.isBuilding)
        {
            highlightSpriteRenderer.color = newColor;
        }
        
    }

    void OnMouseExit()
    {
        highlightSpriteRenderer.color = originalColor;
    }

    void OnMouseDown()
    {
        if(!taken && GlobalVariable.isBuilding)
        {
            if (GlobalVariable.buildingWaterFilter)
            {
                if (gameManager.gold >= gameManager.waterCost)
                {
                    // change variable
                    gameManager.gold -= gameManager.waterCost;
                    gameManager.DisplayWater();

                    // changing the image
                    Sprite newSprite = Sprite.Create(waterFilter, new Rect(0, 0, waterFilter.width, waterFilter.height), Vector2.one * 0.5f);
                    spriteRenderer.sprite = newSprite;

                    // changing the alpha
                    Color currentColor = spriteRenderer.color;
                    currentColor.a = 1.0f;
                    spriteRenderer.color = currentColor;
                    taken = true;
                }
                else
                {
                    pop.PopUp("Not Enough Gold");
                }
            }
            else if (GlobalVariable.buildingCO2Filter)
            {
                if (gameManager.gold >= gameManager.CO2Cost)
                {
                    // change variables
                    GlobalVariable.numCO2FilterBuiling += 1;
                    gameManager.gold -= gameManager.CO2Cost;

                    // changing the image
                    Sprite newSprite = Sprite.Create(CO2Filter, new Rect(0, 0, CO2Filter.width, CO2Filter.height), Vector2.one * 0.5f);
                    spriteRenderer.sprite = newSprite;

                    // changing the alpha
                    Color currentColor = spriteRenderer.color;
                    currentColor.a = 1.0f;
                    spriteRenderer.color = currentColor;
                    taken = true;
                }
                else
                {
                    pop.PopUp("Not Enough Gold");
                }
            }
            else if (GlobalVariable.buildingGoldMine)
            {
                if(gameManager.gold >= gameManager.goldCost)
                {
                    // change variables
                    GlobalVariable.numGoldMineBuiling += 1;
                    gameManager.gold -= gameManager.goldCost;

                    // changing the image
                    Sprite newSprite = Sprite.Create(goldMine, new Rect(0, 0, goldMine.width, goldMine.height), Vector2.one * 0.5f);
                    spriteRenderer.sprite = newSprite;

                    // changing the alpha
                    Color currentColor = spriteRenderer.color;
                    currentColor.a = 1.0f;
                    spriteRenderer.color = currentColor;
                    taken = true;
                }
                else
                {
                    
                    pop.PopUp("Not Enough Gold");

                    //pop out not enough goldDebug.Log("Not enough gold");
                }
            }
            else if (GlobalVariable.buildingElectricGenerator)
            {
                if(gameManager.gold >= gameManager.electricCost)
                {
                    // change variable
                    GlobalVariable.numElectricBuilding += 1;
                    gameManager.gold -= gameManager.electricCost;
                    gameManager.DisplayElectricity();

                    // changing the image
                    Sprite newSprite = Sprite.Create(electricGenerator, new Rect(0, 0, electricGenerator.width, electricGenerator.height), Vector2.one * 0.5f);
                    spriteRenderer.sprite = newSprite;

                    // changing the alpha
                    Color currentColor = spriteRenderer.color;
                    currentColor.a = 1.0f;
                    spriteRenderer.color = currentColor;
                    taken = true;
                }
                else
                {
                    pop.PopUp("Not Enough Gold");
                }
            }
            // else if other buildings
        }
    }


    void Selecting()
    {
        if (!taken && GlobalVariable.isBuilding)
        {
            highlight.SetActive(true);
            StartCoroutine(BreathingCoroutine());
        }
        else
        {
            highlight.SetActive(false);
            StopCoroutine(BreathingCoroutine());
        }
    }

    private IEnumerator BreathingCoroutine()
    {
        while (GlobalVariable.isBuilding)
        {
            if (!EvenOrOdd(tileName, out x, out y))
            {
                yield return ScaleOverTime(childTransform.localScale, maxScale, duration);
                yield return ScaleOverTime(childTransform.localScale, minScale, duration);

            }
            else
            {
                yield return ScaleOverTime(childTransform.localScale, minScale, duration);
                yield return ScaleOverTime(childTransform.localScale, maxScale, duration);
            }
            
        }
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, float targetScale, float duration)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            // Interpolate between startScale and targetScale over time
            childTransform.localScale = Vector3.Lerp(startScale, new Vector3(targetScale, targetScale, 1.0f), elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure that the final scale is exactly the target scale
        childTransform.localScale = new Vector3(targetScale, targetScale, 1.0f);
    }

    bool EvenOrOdd(string name, out int x, out int y)
    {
        x = 0;
        y = 0;

        string[] parts = name.Split(',');

        if (parts.Length == 2)
        {
            if (int.TryParse(parts[0], out x) && int.TryParse(parts[1], out y))
            {
                if ((x + y) % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
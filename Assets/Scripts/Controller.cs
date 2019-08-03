using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Controller : MonoBehaviour, IPointerClickHandler
{
    private readonly float snapVal = 0.02f;

    private float rate;
    private float timeSinceInit;
    private SpriteRenderer sr;
    private HexGrid hexGrid;
    public int x, y;



    public static void ChangeColor(SpriteRenderer sr, Color target, float rate, float snapVal)
    {
        float r, g, b;

        if (Mathf.Abs(sr.color.r - target.r) > snapVal)
        {
            if (sr.color.r > target.r)
                r = sr.color.r - Time.deltaTime * rate;
            else
                r = sr.color.r + Time.deltaTime * rate;
        }
        else
        {
            r = target.r;
        }


        if (Mathf.Abs(sr.color.g - target.g) > snapVal)
        {
            if (sr.color.g > target.g)
                g = sr.color.g - Time.deltaTime * rate;
            else
                g = sr.color.g + Time.deltaTime * rate;
        }
        else
        {
            g = target.g;
        }


        if (Mathf.Abs(sr.color.b - target.b) > snapVal)
        {
            if (sr.color.b > target.b)
                b = sr.color.b - Time.deltaTime * rate;
            else
                b = sr.color.b + Time.deltaTime * rate;
        }
        else
        {
            b = target.b;
        }

        sr.color = new Color(r, g, b);
    }

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hexGrid = Camera.main.GetComponent<HexGrid>();
        sr.color = HexGrid.COLOR_BG;
        rate = 1.1f;
        timeSinceInit = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceInit < 1.0f/rate)
        {
            timeSinceInit += Time.deltaTime;
        } else
        {
            rate = 2.4f;
        }
        Color target;
        switch (hexGrid.model[x,y])
        {
            case 0:
                target = HexGrid.COLOR_BG;
                break;
            case 1:
                target = HexGrid.COLOR_1;
                break;
            case 2:
                target = HexGrid.COLOR_2;
                break;
            case 3:
                target = HexGrid.COLOR_3;
                break;
            default:
                Debug.Log("Should not be reaching this default");
                target = Color.white;
                break;
        }

        ChangeColor(sr, target, rate, snapVal);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(hexGrid.active)
            hexGrid.ClickOnTile(x, y);
    }
}

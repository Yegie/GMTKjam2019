using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Controller : MonoBehaviour, IPointerClickHandler
{
    private readonly float rate = 2.8f;
    private readonly float snapVal = 0.02f;

    private SpriteRenderer sr;
    private HexGrid hexGrid;
    public int x, y;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        hexGrid = Camera.main.GetComponent<HexGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        Color target;
        switch (hexGrid.model[x,y])
        {
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
                target = Color.black;
                break;
        }
        float r, g, b;

        if (Mathf.Abs(sr.color.r - target.r) > snapVal)
        {
            if(sr.color.r > target.r)
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(hexGrid.model[x, y]);
        Debug.Log("Clicked " + x + ", " + y);
        hexGrid.ClickOnTile(x, y);
        Debug.Log(hexGrid.model[x, y]);
    }
}

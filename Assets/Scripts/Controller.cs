using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Controller : MonoBehaviour, IPointerClickHandler
{
    private readonly float rate = 3.5f;
    private readonly float snapVal = 0.013f;

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

        if (sr.color.r - target.r > snapVal)
        {
            r = sr.color.r - Time.deltaTime * rate;
        }
        else if (sr.color.r - target.r < snapVal)
        {
            r = sr.color.r + Time.deltaTime * rate;
        }
        else
        {
            r = target.r;
        }


        if (sr.color.g - target.g > snapVal)
        {
            g = sr.color.g - Time.deltaTime * rate;
        }
        else if (sr.color.g - target.g < snapVal)
        {
            g = sr.color.g + Time.deltaTime * rate;
        } else
        {
            g = target.g;
        }


        if (sr.color.b - target.b > snapVal)
        {
            b = sr.color.b - Time.deltaTime * rate;
        }
        else if (sr.color.g - target.g < snapVal)
        {
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

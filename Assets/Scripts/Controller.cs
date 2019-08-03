using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Controller : MonoBehaviour, IPointerClickHandler
{
    private readonly float rate = 2.5f;
    private readonly float snapVal = 0.015f;

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
        Color target = HexGrid.COLOR_1;
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
            g = sr.color.g - Time.deltaTime * rate;
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
            b = sr.color.b - Time.deltaTime * rate;
        }
        else
        {
            b = target.b;
        }

        sr.color = new Color(r, g, b);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked " + x + ", " + y);
        hexGrid.ClickOnTile(x, y);
    }
}

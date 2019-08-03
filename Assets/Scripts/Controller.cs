using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Controller : MonoBehaviour, IPointerClickHandler
{

    SpriteRenderer sr;
    private readonly float rate = 0.25f;
    private bool falling;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        falling = false;
        sr.color = new Color(1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (falling && sr.color.r > 0)
        {
            Debug.Log("Old " + sr.color.r);
            sr.color = new Color(sr.color.r - Time.deltaTime * rate, 0, 0);
            Debug.Log("New " + sr.color.r);
        } else if (falling)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!falling)
        {
            falling = true;
            sr.color = new Color(1, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AICursor : MonoBehaviour
{
    Image img;
    public bool isVisible;
    public bool isClicking;
    public Sprite cursorGeneral;
    public Sprite cursorDrag;
    public Sprite cursorClickable;
    public Sprite cursorClickdown;
    public Sprite cursorInfo;
    public Sprite cursorBlocked;
    public Sprite cursorIndustrial;
    public Sprite cursorIndustrialdown;

    // Start is called before the first frame update
    void Start()
    {

        img = GetComponent<Image>();
        // Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        isVisible = AI.Instance.isAIActive && !GameManager.paused;
        img.enabled = isVisible;
    }

    public void PerformClick()
    {
        StartCoroutine("SimulateClick");
    }

    IEnumerator SimulateClick()
    {
        img.sprite = cursorClickdown;
        yield return new WaitForSeconds(0.1F);
        img.sprite = cursorGeneral;
    }
}

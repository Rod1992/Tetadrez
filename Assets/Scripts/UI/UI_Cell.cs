using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Cell : MonoBehaviour
{
    [SerializeField]
    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize(float width, float height, Vector2 pos)
    {
        rectTransform.SetLocalPositionAndRotation(pos, Quaternion.Euler(0,0,0));
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}

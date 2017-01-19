using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingtextController : MonoBehaviour {

    private static FloatingText popupText;
    private static FloatingText popupTextCritical;
    private static GameObject canvas;

    public static void initialize()
    {
        canvas = GameObject.Find("Canvas");
        if (!popupText){
            popupText = Resources.Load<FloatingText>("Prefabs/PopUpText");
            popupTextCritical = Resources.Load<FloatingText>("Prefabs/CriticalText");
        }
    }
    public static void createFloatingtext(string text, Transform location){
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }

    public static void createCriticalText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupTextCritical);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }
}

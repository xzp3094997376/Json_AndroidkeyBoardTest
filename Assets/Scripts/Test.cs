using UniSoftwareKeyboardArea;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public CanvasScaler m_canvasScaler;
    public RectTransform m_rectTransform;

    private void Update()
    {
        var rate = m_canvasScaler.referenceResolution.y / Screen.height;
        var pos = m_rectTransform.anchoredPosition;
        pos.y = SoftwareKeyboardArea.GetHeight(true) * rate;
        m_rectTransform.anchoredPosition = pos;
    }

    private void OnGUI()
    {
        //GUILayout.Label();
        GUIStyle style = new GUIStyle();
        style.fontSize = 100;
        GUI.Label(new Rect(100, 100, 200, 100), SoftwareKeyboardArea.GetHeight(true).ToString(),style);
        GUI.Label(new Rect(100, 200, 200, 100), "decord:  "+SoftwareKeyboardArea.mDecorHeight.ToString(), style);
    }
}
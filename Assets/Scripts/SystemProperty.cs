using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemProperty 
{

    public static int GetRelativeKeyboardHeight(RectTransform rectTransform, bool includeInput)
    {
#if UNITY_EDITOR
        return 300;
#endif
        return GetKeyboardHeightAndroid();

        //int keyboardHeight = GetKeyboardHeight(includeInput);
        //float screenToRectRatio = Screen.height / rectTransform.rect.height;
        //float keyboardHeightRelativeToRect = keyboardHeight / screenToRectRatio;

        //return (int)keyboardHeightRelativeToRect;
    }

    public static int GetKeyboardHeightAndroid()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").
                Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);
                return Screen.height - Rct.Call<int>("height");
            }
        }
    }
    

    private static int GetKeyboardHeight(bool includeInput)
    {
#if UNITY_EDITOR
        return 300;
#elif UNITY_ANDROID
        using (AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject unityPlayer = unityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            AndroidJavaObject view = unityPlayer.Call<AndroidJavaObject>("getView");
            AndroidJavaObject dialog = unityPlayer.Get<AndroidJavaObject>("mSoftInputDialog");
            if (view == null || dialog == null)
                return -1;
            var decorHeight = 0;
            if (includeInput)
            {
                AndroidJavaObject decorView = dialog.Call<AndroidJavaObject>("getWindow").Call<AndroidJavaObject>("getDecorView");
                if (decorView != null)
                    decorHeight = decorView.Call<int>("getHeight");
            }
            using (AndroidJavaObject rect = new AndroidJavaObject("android.graphics.Rect"))
            {
                view.Call("getWindowVisibleDisplayFrame", rect);
                return Screen.height - rect.Call<int>("height") + decorHeight;
            }
        }
#elif UNITY_IOS
        return (int)TouchScreenKeyboard.area.height;
#endif
    }

        
}

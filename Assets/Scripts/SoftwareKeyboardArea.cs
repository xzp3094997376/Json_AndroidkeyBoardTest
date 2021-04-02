using UnityEngine;

namespace UniSoftwareKeyboardArea
{	
	public static class SoftwareKeyboardArea
	{
        public static float mDecorHeight=0;

        public static float GetHeight()
		{
         
            return GetHeight( false );
           
        }
        
		public static float GetHeight(bool includeInput)
		{
#if !UNITY_EDITOR && UNITY_ANDROID
			using ( var unityClass = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" ) )
			{
				var currentActivity = unityClass.GetStatic<AndroidJavaObject>( "currentActivity" );
				var unityPlayer = currentActivity.Get<AndroidJavaObject>( "mUnityPlayer" );
				var view = unityPlayer.Call<AndroidJavaObject>( "getView" );

				if ( view == null ) return 0;

				float result;

				using ( var rect = new AndroidJavaObject( "android.graphics.Rect" ) )
				{
					view.Call( "getWindowVisibleDisplayFrame", rect );
					result = Screen.height - rect.Call<int>( "height" );
				}

				if ( !includeInput ) return result;

				var softInputDialog = unityPlayer.Get<AndroidJavaObject>( "mSoftInputDialog" );
				var window = softInputDialog?.Call<AndroidJavaObject>( "getWindow" );
				var decorView = window?.Call<AndroidJavaObject>( "getDecorView" );

				if ( decorView == null ) return result;

				var decorHeight = decorView.Call<int>( "getHeight" );                  
				result +=float.Parse(decorHeight.ToString())*1.5f;
                 
                mDecorHeight=1.5f*decorHeight;
                Debug.Log("decorHeight   = "+decorHeight);

				return result;
			}
#else
            var area   = TouchScreenKeyboard.area;
			var height = Mathf.RoundToInt(area.height);
			return Screen.height <= height ? 0 : height;
#endif
		}
	}
}
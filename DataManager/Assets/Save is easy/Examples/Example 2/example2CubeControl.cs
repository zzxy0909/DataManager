using UnityEngine;

public class example2CubeControl : MonoBehaviour {

    bool _isStarted = false;
    void Start()
    {
        _isStarted = true;
    }
    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 20), "Save Game")) {
            SaveIsEasy.SaveAll();
        }
        if (GUI.Button(new Rect(10, 50, 150, 20), "Load Game")) {
            SaveIsEasy.LoadAll(true);
        }
    }

    public void OnApplicationQuit()
    {
        SaveIsEasy.SaveAll();
    }

    /* We also save on application pause in iOS, as OnAppicationQuit isn't always called */
//#if UNITY_IPHONE && !UNITY_EDITOR
	public void OnApplicationPause( bool b)
	{
        Debug.Log("~~~~~~~~~~~ OnApplicationPause() ");
        // 로딩 된 후라면
        if(_isStarted == true)
        {
            SaveIsEasy.SaveAll();
        }
        
	}
//#endif
}
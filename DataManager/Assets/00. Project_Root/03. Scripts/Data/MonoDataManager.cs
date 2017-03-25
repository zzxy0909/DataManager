using UnityEngine;
using System.Collections;
using Boomlagoon.JSON;
using System.IO;

public enum E_DBSetupOK_Type
{
    File_Save,
    Web_Download,
}
public class MonoDataManager : MonoBehaviour {


	public bool _ViewLog = true;
	private string _log;
    Vector2 scrollPosition;
	void OnGUI()
	{
		if(_ViewLog == true)
		{
            // GUI.Label(new Rect(10, 70, 100, 100), _log);

            GUILayout.BeginArea(new Rect(10, 70, 400, 200));
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            /*changes made in the below 2 lines */
            GUI.skin.box.wordWrap = true;     // set the wordwrap on for box only.
            GUILayout.Box(_log);        // just your message as parameter.

            GUILayout.EndScrollView();

            GUILayout.EndArea();
		}
	}
	public bool _FileSetupOK = false;
    public bool _DBSetupOK = false;
	public int _SetupDbCount = 0;
	public int _CompleteCount = 2;

    public bool _StartupData = false;
    public float _SetupDelay = 1f;
	
    public GameObject _pfWaitPopup;
//    public Transform _PopupBase;
    public GameObject _objWaitPopup=null;
    public Transform _DownloadRatio_1;
    public Transform _DownloadRatio_2;
    public Transform _DownloadRatio_3;

    public string m_balance_query;

    public void RunBalance_query()
    {
        JSONObject objRows = DataManager.Instance.Get_JsonDataRec_BalanceDB(m_balance_query);
        if(objRows != null)
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ objRows:" + objRows.ToString());
    }

    void Awake()
    {
        if (DataManager.Instance._SetupDataManager == null)
        {
            DataManager.Instance._SetupDataManager = this;
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ DataManager.Instance._SetupDataManager = this; ");
        }
        else
        {
            Destroy(this.gameObject);
            Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ Destroy(this.gameObject); DataManager.Instance._SetupDataManager = this; ");
            return;
        }
        transform.parent = null;
        DontDestroyOnLoad(this);

        // StartSetupDB_Web();
        StartSetupDB();
    }

    // Use this for initialization
	void Start () {
        
    }
    public void ViewWaitPopup()
    {
        if (_pfWaitPopup && _objWaitPopup == null)
        {
            _objWaitPopup = (GameObject)Instantiate(_pfWaitPopup);
            GameObject obj = GameObject.FindGameObjectWithTag("GUI_Camera");
            if (obj)
            {
                _objWaitPopup.transform.parent = obj.transform;
                _objWaitPopup.transform.localScale = new Vector3(1f, 1f, 1f);
                _objWaitPopup.transform.localPosition = new Vector3(0f, 0f, -100f);

                _DownloadRatio_1 = _objWaitPopup.transform.FindChild("Label_Message1");
                _DownloadRatio_2 = _objWaitPopup.transform.FindChild("Label_Message2");
                _DownloadRatio_3 = _objWaitPopup.transform.FindChild("Label_Message3");
            }
            else
            {
                Debug.Log("~~~~~~~~~~~~~~~~~~ GameObject.FindGameObjectWithTag(\"GUI_Camera\") == null ");
            }
        }
        else if (_objWaitPopup)
        {
            _objWaitPopup.SetActive(true);
            SetLabelText(_DownloadRatio_1.gameObject, "");
            SetLabelText(_DownloadRatio_2.gameObject, "");
            SetLabelText(_DownloadRatio_3.gameObject, "");
        }
    }
    public void HideWaitPopup()
    {
        // Destroy(_objWaitPopup);
        if (_objWaitPopup) _objWaitPopup.SetActive(false); 
    }
    public void SetLabelText(GameObject obj, string strText)
    {
        if (obj)
        {
            UILabel lb_ratio = obj.GetComponent<UILabel>();
            if (lb_ratio)
            {
                lb_ratio.text = strText;
            }
        }
    }
    void StartSetupDB_Web()
    {
        StartCoroutine(IE_StartSetupDB_Web());
    }
    void StartSetupDB()
    {
        init_DBSetupOK();
        StartCoroutine(IE_StartSetupDB(3));
        StartCoroutine(IE_DBSetupOK(E_DBSetupOK_Type.File_Save));
    }
    void init_DBSetupOK()
    {
        _DBSetupOK = false;
        _FileSetupOK = false;
        _isDownload_Share01DB = false;
    }

    public string _userTypeID_default = "default";
    public string _userTypeID_master1 = "master1";
    public string _userTypeID_master2 = "master2";
    public string _userTypeID_master3 = "master3";
    IEnumerator IE_StartSetupDB_Web()
    {
        init_DBSetupOK();
        yield return StartCoroutine(IE_StartSetupDB(2));
        yield return new WaitForEndOfFrame();
        _FileSetupOK = true;
        //while (_FileSetupOK == false)  // 대기
        //{
        //    yield return new WaitForEndOfFrame();
        //}

        yield return StartCoroutine(E_Download_Share01DB_Start(_userTypeID_default));

        _isDownload_Share01DB = true;
        _DBSetupOK = true;
    }
    
    IEnumerator IE_DBSetupOK(E_DBSetupOK_Type t)
    {
        while (true)
        {
            if(t == E_DBSetupOK_Type.File_Save)
            {
                if (_FileSetupOK == true)
                    break;
            }
            else if (t == E_DBSetupOK_Type.Web_Download)
            {
                if (_isDownload_Share01DB == true)
                    break;
            }
            yield return new WaitForEndOfFrame();
        }

        _DBSetupOK = true;
    }
    IEnumerator IE_StartSetupDB(int a_completCount)
    {
        _SetupDbCount = 0;
        _CompleteCount = a_completCount;
        _DBSetupOK = false;
        _FileSetupOK = false;
        if (CheckSaveFile() == false)
        {
            StartCoroutine(IE_SetupTestBalance());
            StartCoroutine(IE_SetupTestData());
            if(_CompleteCount >= 3) StartCoroutine(IE_Setup_data_share01());
        }
        else
        {
            _FileSetupOK = true;
        }
        ViewWaitPopup();      
        while (true)
        {
            if (_SetupDbCount == _CompleteCount && _FileSetupOK == false)
            {
                _FileSetupOK = true;
                CheckData();
                break;
            }
            //else if (_FileSetupOK == true && _DBSetupOK == false)
            //{
            //    CheckData();
            //}
            //else if (_DBSetupOK == true)
            //{
            //    break;
            //}
            yield return new WaitForFixedUpdate();
        }
        HideWaitPopup(); 
    }
    public IEnumerator IE_Setup_data_share01_reset()
    {
        _SetupDbCount = 0;
        _CompleteCount = 1;

        ViewWaitPopup();
        _DBSetupOK = false;
        _FileSetupOK = false;
        yield return StartCoroutine(IE_Setup_data_share01());

        //while (true)
        //{
        //    if (_SetupDbCount == _CompleteCount && _FileSetupOK == false)
        //    {
        //        _FileSetupOK = true;
        //        break;
        //    }
        //    yield return new WaitForFixedUpdate();
        //}

        _FileSetupOK = true;
        _DBSetupOK = true;
        HideWaitPopup(); 
    }

	// Update is called once per frame
	void Update () {
		
	
	}
    void FixedUpdate()
    {
        //if (_SetupDbCount == _CompleteCount && _FileSetupOK == false)
        //{
        //    _FileSetupOK = true;
        //    CheckData();
        //}
        //else if (_FileSetupOK == true && _objWaitPopup != null
        //    && _DBSetupOK == false)
        //{
        //    CheckData();
        //}
    }
    void CheckData()
    {
        if (_DBSetupOK == true)
        {
            return;
        }
        // 그냥 한번 읽어봄.
        int gold = DataManager.Instance.GetInfo_gold();
    }

    public void ResetBalanceFile()
    {
        StartCoroutine(IE_SetupTestBalance());
    }
    public void ResetDataFile()
    {
        StartCoroutine(IE_SetupTestData());
    }
    public void ResetData_share01()
    {
        StartCoroutine(IE_Setup_data_share01_reset()); // IE_Setup_data_share01());
    }
    
	public bool CheckSaveFile()
	{
		// ver 관리 필요...

        if (_StartupData == true)
        {
            return false;
        }
		
		string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_SaveDB;
		if(File.Exists(filename))
		{
			return true;
		}else{
			return false;
		}
	}

    public bool _isDownload_Share01DB = false;
    public void Download_Share01DB(string userTypeID)
    {
        StartCoroutine(E_Download_Share01DB_Start(userTypeID));
    }
    IEnumerator E_Download_Share01DB_Start(string userTypeID)
    {
        ViewWaitPopup();
        _isDownload_Share01DB = false;

        yield return StartCoroutine(IE_Download_Share01DB(userTypeID));

        if (_isDownload_Share01DB == false)
        {
            // Download Error popup
            SetLabelText(_DownloadRatio_1.gameObject, "Download Error !!! " + userTypeID);
        }
        else
        {
            HideWaitPopup();
        }
    }
    IEnumerator IE_Download_Share01DB(string userTypeID)
    {
        string strFirst = "share01_";
        string strExt = ".db";
        string url_default = "http://115.71.232.123/game/default/";   // "http://115.68.25.250/game/default/";
        string dbfilename = DataManager._DataBaseFileName_Share01DB;
        
        byte[] bytes = null;
        string dbpath_fullURL = url_default + strFirst + userTypeID + strExt; _log += "asset path is: " + dbpath_fullURL;
        WWW www = new WWW(dbpath_fullURL);
        UILabel lb_ratio = null;
        while (!www.isDone)
        {
            if (_DownloadRatio_1)
            {
                lb_ratio = _DownloadRatio_1.GetComponent<UILabel>();
                if (lb_ratio)
                {
                    lb_ratio.text = string.Format("{0}%", www.progress * 100);
                }
            }
            yield return null;
        }
        if (lb_ratio)
            lb_ratio.text = "100%";
//        Debug.Log("~~~~~~~~~~~~~~~~~~ IE_Download_Default ");
        if (www.error != null)
        {
            Debug.LogError(www.error);
        }
        else
        {
            // Debug.Log("read file.");
            bytes = www.bytes; _log += "\n www.error = [" + www.error + "] www.size = " + www.size;
        }
        
        if (bytes != null)
        {
            try
            {
                string filename = Application.persistentDataPath + "/" + dbfilename;						
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length); _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
                }
                _isDownload_Share01DB = true;

            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
            }
        }
    }
    public void UploadTeat_Share01DB(string userTypeID)
    {
        StartCoroutine(UploadData_Share01DB(userTypeID));
    }
    public IEnumerator UploadData_Share01DB(string userTypeID)
    {
        string strFirst = "share01_";
        string strExt = ".db";
        string urlphp = "http://115.71.232.123/game/upload_default.php"; // "http://115.68.25.250/game/upload_default.php";
        string dbWriteFileName = strFirst + userTypeID + strExt;

        string dbfilename = DataManager._DataBaseFileName_Share01DB;
        byte[] bytes = null;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string dbpath = "file://" + Application.persistentDataPath + "/" + dbfilename; _log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		string dbpath =  Application.persistentDataPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
        WWW wwwread = new WWW(dbpath);
        yield return wwwread;
        if (wwwread.error != null)
        {
            Debug.LogError(wwwread.error);
        }
        else
        {
            Debug.Log("read file.");
        }
        bytes = wwwread.bytes;

        if (bytes != null)
        {
            WWWForm form = new WWWForm();
            form.AddBinaryData("file", bytes, dbWriteFileName); //, "image/png" ); //, "theFile", "image/png");
            WWW www = new WWW(urlphp, form);
            yield return www;
            if (www.error != null)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.Log("Finished Uploading -- " + www.text);
            }
            www.Dispose();
        }

        wwwread.Dispose();
    }

	IEnumerator IE_SetupTestData()
	{
        yield return new WaitForSeconds(_SetupDelay);

		string dbfilename = DataManager._DataBaseFileName_StartupSaveDB;
		byte[] bytes = null;				
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; 	_log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		string dbpath =  Application.streamingAssetsPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
		WWW www = new WWW(dbpath);
        UILabel lb_ratio = null;
        while (!www.isDone)
        {
            if (_DownloadRatio_1)
            {
                lb_ratio = _DownloadRatio_1.GetComponent<UILabel>();
                if (lb_ratio)
                {
                    lb_ratio.text = string.Format("{0}%", www.progress * 100);
                }
            }
            yield return null;
        }
        if (lb_ratio)
            lb_ratio.text = "100%"; 
//        progress = 1.0f;

//		yield return www;

        Debug.Log("~~~~~~~~~~~~~~~~~~ WWW www = new WWW(dbpath); ");
//		if (!string.IsNullOrEmpty(www.error) )
//		{
//		    log += " Can't read";
//		}
		
		bytes = www.bytes;

		_log += "\n www.error = [" + www.error + "] www.size = " + www.size;
				
		if ( bytes != null )
		{
			try{	
				string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_SaveDB; // demo_from_streamingAssets.db";						
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
				{
					fs.Write(bytes,0,bytes.Length);             _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
				}
				
				//_SetupOK = true;
				_SetupDbCount++;
				
			}catch (System.Exception e){
				_log += 	"\nTest Fail with Exception " + e.ToString();
				_log += 	"\n\n Did you copy File into StreamingAssets ?\n";
			}
		}
	}
    
    public string _BalanceDataUrl = "";
	IEnumerator IE_SetupTestBalance()
	{
        yield return new WaitForSeconds(_SetupDelay);
        
        string dbfilename = DataManager._DataBaseFileName_StartupBalanceDB;
		byte[] bytes = null;				
		
		//=============
		#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
		string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename;
		#elif UNITY_ANDROID
		string dbpath =  Application.streamingAssetsPath + "/" + dbfilename;
		#endif
        //==================
        
 //       dbpath = _BalanceDataUrl;

        _log += "asset path is: " + dbpath;
		WWW www = new WWW(dbpath);
        UILabel lb_ratio = null;
        while (!www.isDone)
        {
            if (_DownloadRatio_2)
            {
                lb_ratio = _DownloadRatio_2.GetComponent<UILabel>();
                if (lb_ratio)
                {
                    lb_ratio.text = string.Format("{0}%", www.progress * 100);
                }
            }
            yield return null;
        }
        if (lb_ratio)
            lb_ratio.text = "100%"; 
//		yield return www;	
	
		if (!string.IsNullOrEmpty(www.error) )
		{
            _log += " Can't read";

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; _log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		    dbpath =  Application.streamingAssetsPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
            www = new WWW(dbpath);
            yield return www;
		}
		
		bytes = www.bytes;
		
		_log += "\n www.error = [" + www.error + "] www.size = " + www.size;
		
		if ( bytes != null )
		{
			try{	
				string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_BalanceDB; // demo_from_streamingAssets.db";						
				using( FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write) )
				{
					fs.Write(bytes,0,bytes.Length);             _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
				}
				
				//_SetupOK = true;
				_SetupDbCount++;
				
			}catch (System.Exception e){
				_log += 	"\nTest Fail with Exception " + e.ToString();
				_log += 	"\n\n Did you copy File into StreamingAssets ?\n";
			}
		}
	}

    IEnumerator IE_Setup_data_share01()
    {
        yield return new WaitForSeconds(_SetupDelay);

        string dbfilename = DataManager._DataBaseFileName_StartupShare01DB;
        byte[] bytes = null;
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string dbpath = "file://" + Application.streamingAssetsPath + "/" + dbfilename; _log += "asset path is: " + dbpath;
#elif UNITY_ANDROID
		string dbpath =  Application.streamingAssetsPath + "/" + dbfilename;	            _log += "asset path is: " + dbpath;
#endif
        WWW www = new WWW(dbpath);
        UILabel lb_ratio = null;
        while (!www.isDone)
        {
            if (_DownloadRatio_3)
            {
                lb_ratio = _DownloadRatio_3.GetComponent<UILabel>();
                if (lb_ratio)
                {
                    lb_ratio.text = string.Format("{0}%", www.progress * 100);
                }
            }
            yield return null;
        }
        if (lb_ratio)
            lb_ratio.text = "100%";	

 //       yield return www;
        //		if (!string.IsNullOrEmpty(www.error) )
        //		{
        //		    log += " Can't read";
        //		}

        bytes = www.bytes;

        _log += "\n www.error = [" + www.error + "] www.size = " + www.size;

        if (bytes != null)
        {
            try
            {
                string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_Share01DB; // demo_from_streamingAssets.db";						
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length); _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
                }

                _SetupDbCount++;

            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
            }
        }
    }
}

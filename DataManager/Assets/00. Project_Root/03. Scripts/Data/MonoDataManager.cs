using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Boomlagoon.JSON;
using System.IO;



public enum E_DBSetupOK_Type
{
    File_Save,
    Web_Download,
}
public class MonoDataManager : MonoBehaviour {

    public rows_char_ability m_test_list_char_ability = new rows_char_ability();

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
        Debug.Log(" query start:" + Time.realtimeSinceStartup);
        JSONObject objRows = DataManager.Instance.Get_JsonDataRec_BalanceDB(m_balance_query);
        //if (objRows != null)
        //    Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~ objRows:" + objRows.ToString());

        m_test_list_char_ability = JsonUtility.FromJson<rows_char_ability>(objRows.ToString());

        Debug.Log(" query end:" + Time.realtimeSinceStartup);

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
    void StartSetupDB()
    {
        init_DBSetupOK();
        StartCoroutine(IE_StartSetupDB(1));
        StartCoroutine(IE_DBSetupOK(E_DBSetupOK_Type.File_Save));
    }
    void init_DBSetupOK()
    {
        _DBSetupOK = false;
        _FileSetupOK = false;
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
            yield return new WaitForEndOfFrame();
        }
    // file setup 끝난뒤 처리할것들....


        _DBSetupOK = true;
    }
    IEnumerator IE_StartSetupDB(int a_completCount)
    {
        _CompleteCount = a_completCount;
        _DBSetupOK = false;
        _FileSetupOK = false;
        if (CheckSaveFile() == false)
        {
            StartCoroutine(IE_SetupBalanceDB_FromTextAsset()); // IE_SetupTestBalance());
        }
        else
        {
            _FileSetupOK = true;
        }
        ViewWaitPopup();      
        
        HideWaitPopup();

        yield return 0;
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
    
    public void ResetBalanceFile()
    {
        StartCoroutine(IE_SetupBalanceDB_FromTextAsset()); // IE_SetupTestBalance());
    }
    
    
	public bool CheckSaveFile()
	{
		// ver 관리 필요...

        if (_StartupData == true)
        {
            return false;
        }
		
		string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_BalanceDB;
		if(File.Exists(filename))
		{
			return true;
		}else{
			return false;
		}
	}

    public TextAsset m_BalanceDB;
    IEnumerator IE_SetupBalanceDB_FromTextAsset()
    {
        yield return new WaitForSeconds(_SetupDelay);

        byte[] bytes = null;
        if(m_BalanceDB != null)
        {
            bytes = m_BalanceDB.bytes;
        }
        if (bytes != null)
        {
            try
            {
                string filename = Application.persistentDataPath + "/" + DataManager._DataBaseFileName_BalanceDB; // demo_from_streamingAssets.db";						
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(bytes, 0, bytes.Length); _log += "\nCopy database from streaminAssets to persistentDataPath: " + filename;
                }

                _FileSetupOK = true;

            }
            catch (System.Exception e)
            {
                _log += "\nTest Fail with Exception " + e.ToString();
                _log += "\n\n Did you copy File into StreamingAssets ?\n";
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

                _FileSetupOK = true;
				
			}catch (System.Exception e){
				_log += 	"\nTest Fail with Exception " + e.ToString();
				_log += 	"\n\n Did you copy File into StreamingAssets ?\n";
			}
		}
	}
    
}

#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public struct ST_S_zonedataRec
{
    public int idx;
    public int building_layer_ix;
    public string data_code;
    public float pos_x;
    public float pos_y;
    public float pos_z;
    public int direction_no;

}

public class SqlSavedata_zonedata {

	private SQLiteDB _db = null;

    private string _queryUpdate_pos = "Update savedata_zonedata set pos_x=?, pos_y=?, pos_z=? where idx = {0} ;";
    private string _querySelect_all_from_idx = "SELECT * FROM savedata_zonedata where idx = {0} ;";
    private string _querySelect_all = "SELECT * FROM savedata_zonedata ;";

    public SqlSavedata_zonedata()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_SaveDB();
		
	}
    public ST_S_zonedataRec[] Get_SelectAll()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        List<ST_S_zonedataRec> rtnlist = new List<ST_S_zonedataRec>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_S_zonedataRec rec = new ST_S_zonedataRec();

                rec.idx = qr.GetInteger("idx");
                rec.building_layer_ix = qr.GetInteger("building_layer_ix");
                rec.direction_no = qr.GetInteger("direction_no");
                rec.data_code = qr.GetString("data_code");
                try
                {
                    rec.pos_x = (float)qr.GetDouble("pos_x");
                }
                catch
                {
                    try
                    {
                        rec.pos_x = (float)qr.GetInteger("pos_x");
                    }
                    catch
                    {
                        rec.pos_x = 0f;
                    }
                }
                try
                {
                    rec.pos_y = (float)qr.GetDouble("pos_y");
                }
                catch
                {
                    try
                    {
                        rec.pos_y = (float)qr.GetInteger("pos_y");
                    }
                    catch
                    {
                        rec.pos_y = 0f;
                    }
                }
                try
                {
                    rec.pos_z = (float)qr.GetDouble("pos_z");
                }
                catch
                {
                    try
                    {
                        rec.pos_z = (float)qr.GetInteger("pos_z");
                    }
                    catch
                    {
                        rec.pos_z = 0f;
                    }
                }

                rtnlist.Add(rec);
            }
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return rtnlist.ToArray();
    }

    public ST_S_zonedataRec Get_All_From_idx(int a_idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        ST_S_zonedataRec rtn = new ST_S_zonedataRec();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_from_idx, a_idx); // _querySelect_exp
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rtn.idx = qr.GetInteger("idx");
                    rtn.building_layer_ix = qr.GetInteger("building_layer_ix");
                    rtn.direction_no = qr.GetInteger("direction_no");
                    rtn.data_code = qr.GetString("data_code");
                    try
                    {
                        rtn.pos_x = (float) qr.GetDouble("pos_x");
                    }
                    catch
                    {
                        try
                        {
                            rtn.pos_x = (float)qr.GetInteger("pos_x");
                        }
                        catch
                        {
                            rtn.pos_x = 0f;
                        }
                    }
                    try
                    {
                        rtn.pos_y = (float)qr.GetDouble("pos_y");
                    }
                    catch
                    {
                        try
                        {
                            rtn.pos_y = (float)qr.GetInteger("pos_y");
                        }
                        catch
                        {
                            rtn.pos_y = 0f;
                        }
                    }
                    try
                    {
                        rtn.pos_z = (float)qr.GetDouble("pos_z");
                    }
                    catch
                    {
                        try
                        {
                            rtn.pos_z = (float)qr.GetInteger("pos_z");
                        }
                        catch
                        {
                            rtn.pos_z = 0f;
                        }
                    }
            }
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return rtn;
    }
    public void Update_pos(float a_x, float a_y, float a_z, int idx)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }
        try
        {
            _db.Open(GetFileName_DB());

            SQLiteQuery qr;
            string strsql = string.Format(_queryUpdate_pos, idx); //

            qr = new SQLiteQuery(_db, strsql);
            qr.Bind(a_x);
            qr.Bind(a_y);
            qr.Bind(a_z);
            qr.Step();
            qr.Release();
            _db.Close();

        }
        catch (Exception e)
        {
            if (_db != null)
            {
                _db.Close();
                _db = null;
            }
            UnityEngine.Debug.LogError(e.ToString());
        }

        return;
    }
}

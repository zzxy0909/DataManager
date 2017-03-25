#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

public struct ST_B_Style_Info
{
    public string _data_code;
    public int _upgrade_level;
    public int _up_gold;
    public int _attack;
    public float _critical;
    public float _speed;
}
public class SqlBalance_style_info {

	private SQLiteDB _db = null;

    private string _querySelect_all_level = "SELECT * FROM balance_style_info where data_code = '{0}' and upgrade_level = {1} ;";

    public SqlBalance_style_info()
    {
		if(	_db == null)
		{
			_db = new SQLiteDB();
		}
	}
	
	string GetFileName_DB()
	{
		return DataManager.Instance.GetDataFilePath_BalanceDB();
		
	}

    public ST_B_Style_Info[] GetList_all_where_datacode_level(string a_datacode, int a_level)
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        // ST_B_Style_Info rec;
        List<ST_B_Style_Info> rtnlist = new List<ST_B_Style_Info>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_level, a_datacode, a_level); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_B_Style_Info rec = new ST_B_Style_Info();

                rec._attack = qr.GetInteger("attack");
                rec._critical = (float)qr.GetDouble("critical");
                rec._speed = (float)qr.GetDouble("speed");
                rec._up_gold = qr.GetInteger("up_gold");
                rec._data_code = qr.GetString("data_code");
                rec._upgrade_level = qr.GetInteger("upgrade_level");

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

}

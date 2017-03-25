#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_bt_drink_material
{
    public int material_index ;
    public string material_name ;
    public string drink_img ;
    public int material_pricetype ;
    public int material_priceamount ;
}

public class Sql_bt_drink_material {

	private SQLiteDB _db = null;

    private string _querySelect_idx = "select * from bt_drink_material where material_index = {0} ;";
    private string _querySelect_all = "select * from bt_drink_material ;";

    public Sql_bt_drink_material()
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
    public ST_bt_drink_material[] Get_DataRec_All()
    {
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        List<ST_bt_drink_material> rtnlist = new List<ST_bt_drink_material>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_bt_drink_material rec = new ST_bt_drink_material();
                rec.material_index = qr.GetInteger("material_index");
                rec.material_name = qr.GetString("material_name");
                rec.drink_img = qr.GetString("drink_img");
                rec.material_pricetype = qr.GetInteger("material_pricetype");
                rec.material_priceamount = qr.GetInteger("material_priceamount");

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
    public ST_bt_drink_material Get_DataRec(int idx)
	{
        if(	_db == null)
		{
			_db = new SQLiteDB();
		}

        ST_bt_drink_material rtn = new ST_bt_drink_material();
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = "";

            strsql = string.Format(_querySelect_idx, idx); // _querySelect_exp
            
//            UnityEngine.Debug.Log("~~~~~~~~~~" + strsql);
			qr = new SQLiteQuery(_db, strsql); 
			while( qr.Step() )
			{
                rtn.material_index = qr.GetInteger("material_index");
                rtn.material_name = qr.GetString("material_name");
                rtn.drink_img = qr.GetString("drink_img");
                rtn.material_pricetype = qr.GetInteger("material_pricetype");
                rtn.material_priceamount = qr.GetInteger("material_priceamount");
            }
			qr.Release();                                     
			_db.Close();
			
		} catch (Exception e){
			if(_db != null)
			{
				_db.Close();
				_db = null;
			}			
			UnityEngine.Debug.LogError( e.ToString() );			
		}

//        UnityEngine.Debug.Log("~~~~~~~~~~" + rtn.idx);
        return rtn;
	}

}

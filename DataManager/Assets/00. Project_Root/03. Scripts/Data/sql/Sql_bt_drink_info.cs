#define ENCRYPTION

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;

public struct ST_bt_drink_info
{
    public int drink_index ;
    public string drink_name ;
    public string drink_img ;
    public int drink_type;
    public int drink_grade;
    public int drink_quantity;
    public int drink_gaingold ;
    public int drink_gainexp ;
    public int mix1_material_index ;
    public int mix1_material_amount ;
    public int mix2_material_index ;
    public int mix2_material_amount ;
    public int mix3_material_index ;
    public int mix3_material_amount ;
}

public class Sql_bt_drink_info {

	private SQLiteDB _db = null;

    private string _querySelect_all_param3 = "select * from bt_drink_info where mix1_material_index = {0} and mix2_material_index = {1} and mix3_material_index = {2} ;";
    private string _querySelect_all_idx = "select * from bt_drink_info where drink_index = {0} ;";

    public Sql_bt_drink_info()
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
    public ST_bt_drink_info Get_DataRec_idx(int idx)
	{
        if(	_db == null)
		{
			_db = new SQLiteDB();
		}

        ST_bt_drink_info rec = new ST_bt_drink_info();
		
		string filename = GetFileName_DB();
		try{
			_db.Open(filename);
			
			SQLiteQuery qr;
            string strsql = "";

            strsql = string.Format(_querySelect_all_idx, idx); // _querySelect_exp
            
//            UnityEngine.Debug.Log("~~~~~~~~~~" + strsql);
			qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                rec.drink_index = qr.GetInteger("drink_index");
                rec.drink_name = qr.GetString("drink_name");
                rec.drink_img = qr.GetString("drink_img");
                rec.drink_grade = qr.GetInteger("drink_grade");
                rec.drink_type = qr.GetInteger("drink_type");
                rec.drink_quantity = qr.GetInteger("drink_quantity");
                rec.drink_gaingold = qr.GetInteger("drink_gaingold");
                rec.drink_gainexp = qr.GetInteger("drink_gainexp");
                rec.mix1_material_index = qr.GetInteger("mix1_material_index");
                rec.mix1_material_amount = qr.GetInteger("mix1_material_amount");
                rec.mix2_material_index = qr.GetInteger("mix2_material_index");
                rec.mix2_material_amount = qr.GetInteger("mix2_material_amount");
                rec.mix3_material_index = qr.GetInteger("mix3_material_index");
                rec.mix3_material_amount = qr.GetInteger("mix3_material_amount");
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
        return rec;
    }

    public ST_bt_drink_info[] Get_DataRec(int mix1, int mix2, int mix3)
	{
        if (_db == null)
        {
            _db = new SQLiteDB();
        }

        // ST_B_Style_Info rec;
        List<ST_bt_drink_info> rtnlist = new List<ST_bt_drink_info>();

        string filename = GetFileName_DB();
        try
        {
            _db.Open(filename);

            SQLiteQuery qr;
            string strsql = string.Format(_querySelect_all_param3, mix1, mix2, mix3); // 
            qr = new SQLiteQuery(_db, strsql);
            while (qr.Step())
            {
                ST_bt_drink_info rec = new ST_bt_drink_info();

                rec.drink_index = qr.GetInteger("drink_index");
                rec.drink_name = qr.GetString("drink_name");
                rec.drink_img = qr.GetString("drink_img");
                rec.drink_grade = qr.GetInteger("drink_grade");
                rec.drink_type = qr.GetInteger("drink_type");
                rec.drink_quantity = qr.GetInteger("drink_quantity");
                rec.drink_gaingold = qr.GetInteger("drink_gaingold");
                rec.drink_gainexp = qr.GetInteger("drink_gainexp");
                rec.mix1_material_index = qr.GetInteger("mix1_material_index");
                rec.mix1_material_amount = qr.GetInteger("mix1_material_amount");
                rec.mix2_material_index = qr.GetInteger("mix2_material_index");
                rec.mix2_material_amount = qr.GetInteger("mix2_material_amount");
                rec.mix3_material_index = qr.GetInteger("mix3_material_index");
                rec.mix3_material_amount = qr.GetInteger("mix3_material_amount");

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

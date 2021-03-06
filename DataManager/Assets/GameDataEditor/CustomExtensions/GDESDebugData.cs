// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by the Game Data Editor.
//
//      Changes to this file will be lost if the code is regenerated.
//
//      This file was generated from this data file:
//      /Users/EthanW/Documents/UnityProjects/EPoker/Assets/GameDataEditor/Resources/gde_data.txt
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataEditor;

namespace GameDataEditor
{
    public class GDESDebugData : IGDEData
    {
        static string SinglePlayerStartForTestKey = "SinglePlayerStartForTest";
		bool _SinglePlayerStartForTest;
        public bool SinglePlayerStartForTest
        {
            get { return _SinglePlayerStartForTest; }
            set {
                if (_SinglePlayerStartForTest != value)
                {
                    _SinglePlayerStartForTest = value;
					GDEDataManager.SetBool(_key, SinglePlayerStartForTestKey, _SinglePlayerStartForTest);
                }
            }
        }

        static string FreeDealCardsRuleForTestKey = "FreeDealCardsRuleForTest";
		bool _FreeDealCardsRuleForTest;
        public bool FreeDealCardsRuleForTest
        {
            get { return _FreeDealCardsRuleForTest; }
            set {
                if (_FreeDealCardsRuleForTest != value)
                {
                    _FreeDealCardsRuleForTest = value;
					GDEDataManager.SetBool(_key, FreeDealCardsRuleForTestKey, _FreeDealCardsRuleForTest);
                }
            }
        }

        static string LessCardsCountForTestKey = "LessCardsCountForTest";
		int _LessCardsCountForTest;
        public int LessCardsCountForTest
        {
            get { return _LessCardsCountForTest; }
            set {
                if (_LessCardsCountForTest != value)
                {
                    _LessCardsCountForTest = value;
					GDEDataManager.SetInt(_key, LessCardsCountForTestKey, _LessCardsCountForTest);
                }
            }
        }

        public GDESDebugData(string key) : base(key)
        {
            GDEDataManager.RegisterItem(this.SchemaName(), key);
        }
        public override Dictionary<string, object> SaveToDict()
		{
			var dict = new Dictionary<string, object>();
			dict.Add(GDMConstants.SchemaKey, "SDebug");
			
            dict.Merge(true, SinglePlayerStartForTest.ToGDEDict(SinglePlayerStartForTestKey));
            dict.Merge(true, FreeDealCardsRuleForTest.ToGDEDict(FreeDealCardsRuleForTestKey));
            dict.Merge(true, LessCardsCountForTest.ToGDEDict(LessCardsCountForTestKey));
            return dict;
		}

        public override void UpdateCustomItems(bool rebuildKeyList)
        {
        }

        public override void LoadFromDict(string dataKey, Dictionary<string, object> dict)
        {
            _key = dataKey;

			if (dict == null)
				LoadFromSavedData(dataKey);
			else
			{
                dict.TryGetBool(SinglePlayerStartForTestKey, out _SinglePlayerStartForTest);
                dict.TryGetBool(FreeDealCardsRuleForTestKey, out _FreeDealCardsRuleForTest);
                dict.TryGetInt(LessCardsCountForTestKey, out _LessCardsCountForTest);
                LoadFromSavedData(dataKey);
			}
		}

        public override void LoadFromSavedData(string dataKey)
		{
			_key = dataKey;
			
            _SinglePlayerStartForTest = GDEDataManager.GetBool(_key, SinglePlayerStartForTestKey, _SinglePlayerStartForTest);
            _FreeDealCardsRuleForTest = GDEDataManager.GetBool(_key, FreeDealCardsRuleForTestKey, _FreeDealCardsRuleForTest);
            _LessCardsCountForTest = GDEDataManager.GetInt(_key, LessCardsCountForTestKey, _LessCardsCountForTest);
        }

        public GDESDebugData ShallowClone()
		{
			string newKey = Guid.NewGuid().ToString();
			GDESDebugData newClone = new GDESDebugData(newKey);

            newClone.SinglePlayerStartForTest = SinglePlayerStartForTest;
            newClone.FreeDealCardsRuleForTest = FreeDealCardsRuleForTest;
            newClone.LessCardsCountForTest = LessCardsCountForTest;

            return newClone;
		}

        public GDESDebugData DeepClone()
		{
			GDESDebugData newClone = ShallowClone();
            return newClone;
		}

        public void Reset_SinglePlayerStartForTest()
        {
            GDEDataManager.ResetToDefault(_key, SinglePlayerStartForTestKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetBool(SinglePlayerStartForTestKey, out _SinglePlayerStartForTest);
        }

        public void Reset_FreeDealCardsRuleForTest()
        {
            GDEDataManager.ResetToDefault(_key, FreeDealCardsRuleForTestKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetBool(FreeDealCardsRuleForTestKey, out _FreeDealCardsRuleForTest);
        }

        public void Reset_LessCardsCountForTest()
        {
            GDEDataManager.ResetToDefault(_key, LessCardsCountForTestKey);

            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            dict.TryGetInt(LessCardsCountForTestKey, out _LessCardsCountForTest);
        }

        public void ResetAll()
        {
            GDEDataManager.ResetToDefault(_key, SinglePlayerStartForTestKey);
            GDEDataManager.ResetToDefault(_key, LessCardsCountForTestKey);
            GDEDataManager.ResetToDefault(_key, FreeDealCardsRuleForTestKey);


            Dictionary<string, object> dict;
            GDEDataManager.Get(_key, out dict);
            LoadFromDict(_key, dict);
        }
    }
}

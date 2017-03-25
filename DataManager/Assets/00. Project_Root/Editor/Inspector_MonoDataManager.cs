using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MonoDataManager))]
[CanEditMultipleObjects]
public class Inspector_MonoDataManager : Editor {

	// Use this for initialization
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();
        MonoDataManager l_MonoDataManager = target as MonoDataManager;

        if (GUILayout.Button("Run balance query"))
        {
            l_MonoDataManager.RunBalance_query();
        }
    }
}

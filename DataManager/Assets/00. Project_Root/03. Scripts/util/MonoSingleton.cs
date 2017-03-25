using UnityEngine;
/// <summary>
/// GameObject ¸¦ À§ÇÑ Singleton
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T _Instance = null;

    public static T instance {
        get {
            // Instance requiered for the first time, we look for it
            if (_Instance == null) {
                _Instance = GameObject.FindObjectOfType (typeof(T)) as T;

                // Object not found, we create a temporary one
                if (_Instance == null) {
                    Debug.LogWarning ("No instance of " + typeof(T).ToString () + ", a temporary one is created.");
                    _Instance = new GameObject ("Temp Instance of " + typeof(T).ToString (), typeof(T)).GetComponent<T> ();

                    // Problem during the creation, this should not happen
                    if (_Instance == null) {
                        Debug.LogError ("Problem during the creation of " + typeof(T).ToString ());
                    }
                }
                _Instance.Init ();
            }
            return _Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake ()
    {
        if (_Instance == null) {
            _Instance = this as T;
            _Instance.Init ();
        }
    }

    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    protected virtual void Init ()
    {
    }

    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    private void OnApplicationQuit ()
    {
        _Instance = null;
    }
}
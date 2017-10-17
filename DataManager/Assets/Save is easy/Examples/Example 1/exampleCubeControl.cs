using UnityEngine;

[RequireComponent(typeof(SerializableComponent))]
public class exampleCubeControl : MonoBehaviour {

    public GameObject Prefab;

    [Save]
    public float time;

    void Update() {
        if (Random.Range(1, 70) == 1) {
            GameObject newObject = SaveIsEasy.PrefabInstantiate(Prefab);
            newObject.transform.position = transform.position + new Vector3(Random.Range(-10, 10), 0, 0);
        }

        time += Time.deltaTime;
    }


    void OnGUI() {
        if (GUI.Button(new Rect(10, 10, 150, 20), "Save Game")) {
            SaveIsEasy.SaveAll();
        }
        if (GUI.Button(new Rect(10, 50, 150, 20), "Load Game")) {
            SaveIsEasy.LoadAll(true);
        }

        GUI.Label(new Rect(10, 300, 200, 20), "Time: " + (int)time);
    }
}
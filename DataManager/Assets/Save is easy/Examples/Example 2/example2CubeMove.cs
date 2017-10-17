using UnityEngine;

[RequireComponent(typeof(SerializableComponent))]
public class example2CubeMove : MonoBehaviour {

    public GameObject cube;
    public bool canMove;

    [Save]
    int speed;
    [Save]
    public TestSerializableObject _TestObj = new TestSerializableObject();
    [Save]
    public StringStringDictionary _TestDict = new StringStringDictionary();

    void Awake() {
        if (speed == 0)
            speed = Random.Range(100, 600);
    }

    void Update() {
        if (canMove) {

            if (Input.GetKey(KeyCode.W)) {
                transform.Translate(Vector3.up * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.Translate(Vector3.down * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.Translate(Vector3.left * Time.deltaTime * 5);
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.Translate(Vector3.right * Time.deltaTime * 5);
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                SaveIsEasy.PrefabInstantiate(cube, transform.position, Quaternion.identity);
            }
        } else {
            transform.Rotate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    //if the object is too far go back to the center
    private void OnBecameInvisible() {
        transform.position = Vector3.zero;
    }

    void OnLoad() {
        //Example of load event
        string str = "";
        if (_TestObj != null)
        {
            str = string.Format("On Load event! speed:{0}, _TestObj:{1}, {2},  _TestDict.count: {3} ", speed, _TestObj.n1, _TestObj.n2, _TestDict.Count );
        }else
        {
            str = string.Format("On Load event! speed:{0}", speed);
        }
        
        Debug.Log(str);

    }
}
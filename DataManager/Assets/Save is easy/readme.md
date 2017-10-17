Info on how to use "Save is easy"


## How to use it

Whenever you have a class that uses some kind of attribute associated with the system saved it is necessary that you add at the beginning of the class:
[RequireComponent (typeof (SerializableComponent))]
This makes unity to add the "Serializable component" Component to your GameObject.

All the fields containing [Save] will be saved and loaded.

If the objects you want to be saved is created at runtime you need to have the prefab assigned to do you need to use:
      "SaveIsEasy.PrefabInstantiate (GameObject prefab)"
This will return it the GameObject that instantiate, Also you need to have the prefab in a **Resources folder** inside the Assets folder of your project.

If you want to save position, rotation and scale you have to select it in the SerializableComponent at the inspector.

You need to have one "SerializableManager" in a GameObject in the scene you want to load or save, in this component, you can configure AutoSave, If the save is Encrypted, To save at exit, and more.

## Warning

Never use Destroy (Object obj, float time) with time, in an object that you want to be saved. This causes when the game is close unity remove it before it can be saved.

Never use the Start() event in an object you want to load, because at start event the object not has been fully loaded, you need to use: **Awake()**

## Events

Whenever an object of any type is loaded it will call this function
void OnLoad() {}

## Objects supporting be saved

It can be stored any object that is a Serializable type, more information at:
https://msdn.microsoft.com/en-us/library/ms973893.aspx


Supports the saving of the following objects of unity:
Vector2, Vector3, Vector4, Quaternion, and Color

## File Viewer
Is a powerful tool that is found at "Windows -> Save is easy -> File Viewer", that allows you to explore you saved files and more.

#### Contact
For any questions, problems or recommendations you can contact me by email: zedgeincorporation@gmail.com
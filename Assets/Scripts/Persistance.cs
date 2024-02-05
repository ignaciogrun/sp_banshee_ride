using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistance : MonoBehaviour
{
    public int leftMin;
    public int leftMax;
    public int rightMin;
    public int rightMax;
    public int halfLeft;
    public int halfRight;

    private static Persistance _instance; // this var holds the instance reference but is private (access via the Getter)
    public static Persistance instance // this Getter is public and encapsulates the "_instance" singleton var
    {
        get
        {
            // If a singleton instance is null we must create one, otherwise
            // we return a reference to the existing one, thus ensuring there is
            // always exactly one instance.

            if (_instance == null)
            {
                GameObject go = new GameObject("PersistanceSingleton");

                // Get a reference to the component, this will be our singleton instance 
                _instance = go.AddComponent<Persistance>();

                // Prevent this object from getting unloaded/destroyed when changing scenes
                DontDestroyOnLoad(go);
                _instance.initialize();
            }

            // Return the instance
            return _instance;
        }
    }

    public void initialize()
    {
        print("initialized");
    }
}

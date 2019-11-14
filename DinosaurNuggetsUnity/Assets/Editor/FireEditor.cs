using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Flame))]
public class FireEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Flame flame = (Flame)target;
        if(GUILayout.Button ("Ignite"))
        {
            flame.Ignite();
        }
    }
}
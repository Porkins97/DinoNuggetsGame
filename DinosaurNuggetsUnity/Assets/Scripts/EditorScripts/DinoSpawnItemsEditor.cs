using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DinoSpawnItems)), CanEditMultipleObjects]
public class DinoSpawnItemsEditor : Editor
{

    SerializedProperty typeProp, ingredientA, ingredientB, ingredientC, spawnPoint2, spawnPoint3, ingredientsParents;

    private void OnEnable()
    {

        typeProp = serializedObject.FindProperty("amountOfSpawns");
        ingredientA = serializedObject.FindProperty("ingredientA");
        ingredientB = serializedObject.FindProperty("ingredientB");
        ingredientC = serializedObject.FindProperty("ingredientC");
        spawnPoint2 = serializedObject.FindProperty("twoSpawns");
        spawnPoint3 = serializedObject.FindProperty("threeSpawns");
        ingredientsParents = serializedObject.FindProperty("ingredientsParents");
    }

    public override void OnInspectorGUI()
    {
        

        serializedObject.Update();

        DinoSpawnItems spawnItems = (DinoSpawnItems)target;

        EditorGUILayout.PropertyField(typeProp);
        DinoSpawnItems.AmountofSpawns st = (DinoSpawnItems.AmountofSpawns)typeProp.intValue;


        EditorGUILayout.PropertyField(ingredientA);
        EditorGUILayout.PropertyField(ingredientB);
        if(st == DinoSpawnItems.AmountofSpawns.Three)
        {
            EditorGUILayout.PropertyField(ingredientC);
        }

        if (GUILayout.Button("Refresh Spawners"))
        {
            spawnItems.StartingMethods();
        }
        if (GUILayout.Button("Spawn Items"))
        {
            spawnItems.SpawnItems();
        }

        EditorGUILayout.PropertyField(spawnPoint2);
        EditorGUILayout.PropertyField(spawnPoint3);

        spawnItems.UpdateMethods();

        if (GUILayout.Button("Refresh Entirety"))
        {
            spawnItems.Start();
            Debug.Log("Refreshed");
        }

        


        serializedObject.ApplyModifiedProperties();
        DrawDefaultInspector();

    }
}
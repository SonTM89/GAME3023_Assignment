using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbilityTable))]
public class AbilityTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AbilityTable abilityTable = (AbilityTable)target;
        if(abilityTable)
        {
            if(GUILayout.Button("Assign ability IDs"))
            {
                abilityTable.AssignAbilityIDs();
            }
        }
    }
}

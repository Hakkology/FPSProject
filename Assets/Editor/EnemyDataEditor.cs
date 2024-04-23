using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    SerializedProperty attackTypeProp;
    SerializedProperty projectilePrefabProp;
    SerializedProperty projectileSpeedProp;

    private void OnEnable()
    {
        // Cache the SerializedProperties
        attackTypeProp = serializedObject.FindProperty("attackType");
        projectilePrefabProp = serializedObject.FindProperty("enemyProjectilePrefab");
        projectileSpeedProp = serializedObject.FindProperty("enemyProjectileSpeed");
    }

    public override void OnInspectorGUI()
    {
        // Update the serialized object
        serializedObject.Update();

        // Draw the default inspector excluding the properties we want to conditionally show
        DrawPropertiesExcluding(serializedObject, "enemyProjectilePrefab", "enemyProjectileSpeed");

        // Conditional drawing based on the attack type
        if (attackTypeProp.enumValueIndex == (int)AttackType.Ranged) // Cast to int since enumValueIndex is an int
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ranged Attack Properties", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(projectilePrefabProp);
            EditorGUILayout.PropertyField(projectileSpeedProp);
        }

        // Apply changes to the serialized object
        serializedObject.ApplyModifiedProperties();
    }
}

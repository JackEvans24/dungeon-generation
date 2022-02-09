using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonGenerator), editorForChildClasses: true)]
public class DungeonGeneratorEditor : Editor
{
    private DungeonGenerator generator;

    protected void Awake()
    {
        this.generator = (DungeonGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(20f);

        if (GUILayout.Button("Generate Dungeon"))
            generator.GenerateDungeon();
    }
}

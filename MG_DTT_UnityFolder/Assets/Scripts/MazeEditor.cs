using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MazeGenerator))]
public class MazeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Generate"))
        {

            MazeGenerator mazeGenerator = (MazeGenerator) target;
            mazeGenerator.Generate(mazeGenerator.x,mazeGenerator.y);


        }

    }


}

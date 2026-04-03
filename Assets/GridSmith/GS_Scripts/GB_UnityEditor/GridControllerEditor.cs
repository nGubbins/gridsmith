using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using JetBrains.Annotations;

#if UNITY_EDITOR
[CustomEditor(typeof(GridSmith.GridController))]
public class GridControllerEditor : Editor
{

    Vector2Int coord1; 
    Vector2Int coord2;
    bool neighboursOnly;

    public override void OnInspectorGUI()
    {
        GridSmith.GridController myTarget = (GridSmith.GridController)target;

        EditorGUI.BeginChangeCheck();
        Undo.RecordObject(myTarget, "Change Settings");

        GUIStyle italicStyle = new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Italic };

        //========================Settings Changes Below Here========================================

        //Set Tiles
        EditorGUILayout.LabelField("Tile Objects", EditorStyles.boldLabel);
        myTarget.GridTile = (GameObject)EditorGUILayout.ObjectField("Tile A: ", myTarget.GridTile, typeof(GameObject), true);
        EditorGUI.BeginDisabledGroup(!myTarget.UseAlt);
        myTarget.GridTileAlt = (GameObject)EditorGUILayout.ObjectField("Tile B: ", myTarget.GridTileAlt, typeof(GameObject), true);
        EditorGUI.EndDisabledGroup();
        myTarget.UseAlt = EditorGUILayout.Toggle("Use Alternate: ", myTarget.UseAlt);
        EditorGUILayout.Space();

        //Set Grid Dimensions
        EditorGUILayout.LabelField("Grid Dimensions", EditorStyles.boldLabel);
        myTarget.Dimensions = Vector2Int.Max(new Vector2Int(1, 1), 
        EditorGUILayout.Vector2IntField("Grid Dimensions", myTarget.Dimensions));
        EditorGUILayout.Space();

        //Load Options
        EditorGUILayout.LabelField("Load Options", EditorStyles.boldLabel);
        myTarget.Offset = EditorGUILayout.Vector2Field("Offset: ", myTarget.Offset);
        myTarget.LoadOnStart = EditorGUILayout.Toggle("Load On Start: ", myTarget.LoadOnStart);
        EditorGUILayout.Space();

        //Space Between Sections
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.LabelField("Spawn Grid", EditorStyles.boldLabel);
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if (GUILayout.Button("Generate Grid", GUILayout.Height(30)))
        {
            myTarget.SetUp();
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("Swap by Coordinates", EditorStyles.boldLabel);
        coord1 = EditorGUILayout.Vector2IntField("Tile 1", coord1);
        coord2 = EditorGUILayout.Vector2IntField("Tile 2", coord2);
        neighboursOnly = EditorGUILayout.Toggle("Neighbour Swaps Only: ", neighboursOnly);
        EditorGUI.BeginDisabledGroup(!Application.isPlaying);
        if (GUILayout.Button("Swap Tiles", GUILayout.Height(30)))
        {
            myTarget.SwapByCoordinates(coord1, coord2, neighboursOnly);
        }

        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space();

        //===================Settings Changes Above Here==================================

        if (EditorGUI.EndChangeCheck()) 
        {
        EditorUtility.SetDirty(myTarget);
        }
    }
}
#endif
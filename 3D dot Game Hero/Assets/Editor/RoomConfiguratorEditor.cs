using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomConfigurator))]
public class RoomConfiguratorEditor : Editor {

    private RoomConfigurator mapTextureManager;

    void ButtonsRoutine() {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Getters");
        GUILayout.Label("Setters");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Get Walls")) {
            mapTextureManager.GetWalls();
        }else if (GUILayout.Button("Get Ground")) {
            mapTextureManager.GetGrounds();
        }else if (GUILayout.Button("Set Walls")) {
            mapTextureManager.ChangeWalls();
        }else if (GUILayout.Button("Set Ground")) {
            mapTextureManager.ChangeGrounds();
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Doors Management");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("North")) {
            mapTextureManager.UpdateDoor(0);
        }else if (GUILayout.Button("East")) {
            mapTextureManager.UpdateDoor(1);
        }else if (GUILayout.Button("South")) {
            mapTextureManager.UpdateDoor(2);
        }else if (GUILayout.Button("West")) {
            mapTextureManager.UpdateDoor(3);
        }
        GUILayout.EndHorizontal();

        GUILayout.Label("Pilars Management");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("NorthWest")) {
            mapTextureManager.UpdatePilars(0);
        }else if (GUILayout.Button("NorthEast")) {
            mapTextureManager.UpdatePilars(1);
        }else if (GUILayout.Button("SouthEast")) {
            mapTextureManager.UpdatePilars(2);
        }else if (GUILayout.Button("SouthWest")) {
            mapTextureManager.UpdatePilars(3);
        }
        GUILayout.EndHorizontal();
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        mapTextureManager = (RoomConfigurator)target;

        ButtonsRoutine();
    }
}

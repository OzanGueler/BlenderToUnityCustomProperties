using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BTU))]
public class BTUEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        BTU myBtu = target as BTU;

        for (int i = 0; i < myBtu.operators.Count; i++) {
            if (GUILayout.Button(myBtu.operators[i].fj_prop_name)) {
                myBtu.operators[i].Execute(myBtu.gameObject);
            }
        }
    }
}

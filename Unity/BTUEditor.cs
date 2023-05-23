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

        BTU myFroggy = target as BTU;

        for (int i = 0; i < myFroggy.operators.Count; i++) {
            if (GUILayout.Button(myFroggy.operators[i].fj_prop_name)) {
                myFroggy.operators[i].Execute(myFroggy.gameObject);
            }
        }
    }
}

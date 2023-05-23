using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.GameObjectUtility;

[BTUProp("StaticFlags")]
public class BTUPropFlagOperator : BTUPropOperator {
    public int flagValue = 0;
    public override void Init() {
        flagValue = Int32.Parse((string) fj_prop_value);
    }

    public override void Execute(GameObject target) {
        int myFlags = flagValue;
        List<int> editorFlags = new List<int> {
            (int)StaticEditorFlags.ContributeGI,
            (int)StaticEditorFlags.OccluderStatic,
            (int)StaticEditorFlags.BatchingStatic,
            (int)StaticEditorFlags.NavigationStatic,
            (int)StaticEditorFlags.OccludeeStatic,
            (int)StaticEditorFlags.OffMeshLinkGeneration,
            (int)StaticEditorFlags.ReflectionProbeStatic
        };
        for (int i = editorFlags.Count-1; i >= 0; i--) {
            if (myFlags >= Mathf.Pow(2, i)) {
                myFlags += editorFlags[i];
                myFlags -= (int)Mathf.Pow(2, i);
            }
        }
        SetStaticEditorFlags(target, (StaticEditorFlags) myFlags);
    }
}

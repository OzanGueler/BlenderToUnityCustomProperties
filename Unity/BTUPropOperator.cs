using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BTUPropOperator {
    public string fj_prop_name;
    public abstract void Execute(GameObject target);
}

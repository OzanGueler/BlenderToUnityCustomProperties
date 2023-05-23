using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTUPropAttribute : Attribute {
    public string propName;
    public BTUPropAttribute(string name) {
        propName = name;
    }
}

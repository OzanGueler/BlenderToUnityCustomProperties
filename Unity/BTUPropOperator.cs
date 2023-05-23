using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BTUPropOperator {
    //Set by the coder to the custom property name that they want to react to
    public string fj_prop_name;
    //Set by BTU to the value that was passed down from the AssetPostProcessor
    public object fj_prop_value;
    //Set by the coder to the description that will be on the button that calls the method Execute
    public string fj_button_description = "";

    //Will be called after fj_prop_value is set
    //Should be used for all initializations that need the value of the property of the imported model
    public abstract void Init();
    public abstract void Execute(GameObject target);
}

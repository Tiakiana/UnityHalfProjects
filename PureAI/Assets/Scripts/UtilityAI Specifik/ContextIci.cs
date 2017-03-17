using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ContextIci : MonoBehaviour {

    List<IResource> Resources = new List<IResource>();
    Stats stats;

    void Start() {
        stats = GetComponent<Stats>();

    }

}

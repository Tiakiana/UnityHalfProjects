using UnityEngine;
using System.Collections;

public interface IActionable {

    void Attack(float damage, GameObject target);

    void Move(Vector3 destination);
    void Spawn();


}

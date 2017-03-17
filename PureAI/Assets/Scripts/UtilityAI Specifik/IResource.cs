using UnityEngine;
using System.Collections;
public enum ResourceType {

}

 public interface IResource  {
     ResourceType ResourceTyp { get; set; }
	 int Value { get; set; }
    Vector3 Position { get; set; }
}

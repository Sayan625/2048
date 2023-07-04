using UnityEngine;

public class Node : MonoBehaviour
{

  public Vector2 pos{
      get {return transform.position;}
  }

  public Block occupiedBlock;


}

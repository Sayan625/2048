using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int value;
    public SpriteRenderer renderer;
    public TextMeshPro text;
    public Vector2 pos{
        get {return transform.position;}
    }

    public Node Node;

    public void Init(BlockType block){
        value=block.value;
        renderer.color=block.color;
        text.text=block.value.ToString();
    }


    public void SetBlock(Node node){
        if(Node!= null)
        Node.occupiedBlock=null;
        Node=node;
        Node.occupiedBlock=this;
    }
}

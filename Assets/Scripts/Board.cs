using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class Board : MonoBehaviour
{
    public GameController gameController;
    public EffectController effectController;
    public GameObject node;
    public GameObject Block;
    public SpriteRenderer background;
    public SpriteRenderer foreground;
    public List<Node> allNodes;
    public List<Block> allBlocks;
    public List<BlockType> blockTypes;
    public bool isBusy=false;

    int width;
    int height;

    void Start()
    {
        width=(int)gameController.width;
        height=(int)gameController.height;
        SetupGrid();
        SpawnBlocks(2);
    }


    void SetupGrid()
    {
        allNodes= new List<Node>();
        allBlocks= new List<Block>();

        for (int i = 0; i < width; i++){
            for (int j = 0; j < height; j++){
                GameObject tempNode= Instantiate(node,new Vector3(i,j,0), Quaternion.identity) as GameObject;
                tempNode.transform.parent=transform;
                allNodes.Add(tempNode.GetComponent<Node>());
            }   
        }
        Vector2 center= new Vector2((float) width/2 - 0.5f, (float) height/2 -0.5f);
        SpriteRenderer boardBg=Instantiate(background,center,Quaternion.identity);
        boardBg.transform.parent=transform;
        boardBg.transform.localScale = new Vector3(width+0.2f,height+0.2f,0);

        SpriteRenderer boardFg=Instantiate(foreground,center,Quaternion.identity);
        boardFg.transform.parent=transform;
        boardFg.transform.localScale = new Vector3(width+0.2f,height+0.2f,0);
    }

    void SpawnBlocks(int amount)
    {
        List<Node> freeNodes= new List<Node>();
        freeNodes= allNodes.Where(n=>n.occupiedBlock == null).OrderBy(n=> Random.value).ToList();
        foreach (var node in freeNodes.Take(amount)){
            StartCoroutine(spawn(node));

        }
    }

    BlockType getBlockByValue(int value){
       return blockTypes.First(t=>t.value==value);
    }

    Node GetNodeAtPos(Vector2 pos){
        return allNodes.FirstOrDefault(n=>n.pos==pos);
    }

    public void MoveBlock(Vector2 dir){
        if(!isBusy){
        List<Block> orderedBlock =allBlocks.OrderBy(b=>b.pos.x).ThenBy(b=>b.pos.y).ToList();
        if(dir==Vector2.right || dir==Vector2.up)
        orderedBlock.Reverse();

        foreach (var block in orderedBlock){
            Node next=block.Node;
            do{
                block.SetBlock(next);
                Node possibleNode= GetNodeAtPos(next.pos + dir);
                if(possibleNode!=null)
                    if(possibleNode.occupiedBlock == null)
                        next= possibleNode;
            }
            while(next !=block.Node);
            if(block.transform!=null && block.Node.transform!=null)
            StartCoroutine(move(block,dir));
        }
        SpawnBlocks(1);
        }
    }

    IEnumerator move(Block block, Vector2 dir){
        if(!gameController.isOver){
        float lapsedTime=0f;
        isBusy=true;
        while(lapsedTime<effectController.moveSpeed){           
            lapsedTime+=Time.deltaTime;
            effectController.MoveEffect(block.transform, block.Node.transform, lapsedTime);
            yield return null;
        }
        
        Node match=GetNodeAtPos(block.Node.pos + dir);
        if(match!=null && match.occupiedBlock?.value==block.value){
            allBlocks.Remove(match.occupiedBlock);
            Destroy(match.occupiedBlock.transform.gameObject);
            block.SetBlock(match);
            block.Init(getBlockByValue(2*block.value));
            block.transform.position=block.Node.pos;
            gameController.UpdateScore();
            if(allBlocks.Max(x=>x.value)==gameController.winTarget){
            yield return new WaitForSeconds(effectController.moveSpeed);
            gameController.GameWon();
            }
        }
        }
        isBusy=false;
    }


    IEnumerator spawn(Node node){
        if(!gameController.isOver){
        isBusy=true;
        GameObject tempBlock =Instantiate(Block,node.pos,Quaternion.identity);
        tempBlock.GetComponent<Block>().Init(getBlockByValue(2));
        tempBlock.GetComponent<Block>().SetBlock(node);
        tempBlock.transform.parent=transform;
        allBlocks.Add(tempBlock.GetComponent<Block>());
        yield return new WaitForSeconds(0.1f);
        if(allBlocks.Count==(width*height))
        gameController.GameLose();
        }
        isBusy=false;
    }
}


[System.Serializable]
public struct BlockType{
    public int value;
    public Color color;
}

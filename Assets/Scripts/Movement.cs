using UnityEngine;

public class Movement : MonoBehaviour
{
    public Board board;
    Vector3 startPos;
    Vector3 endPos;
    void Start()
    {
        board=transform.parent.GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){
        startPos=Camera.main.ScreenToWorldPoint(Input.mousePosition)*-1;

    }

    void OnMouseUp(){
        endPos=Camera.main.ScreenToWorldPoint(Input.mousePosition)*-1;

        if(startPos!=endPos && !board.isBusy){

            float swipeAngle=Mathf.Atan2(endPos.y-startPos.y,endPos.x-startPos.x)*180/Mathf.PI;
            if(swipeAngle>-45 && swipeAngle<=45){
            board.MoveBlock(Vector2.left);
            }
            if(swipeAngle>45 && swipeAngle<=135){
            board.MoveBlock(Vector2.down);
            }
            if(swipeAngle>135 || swipeAngle<=-135){
            board.MoveBlock(Vector2.right);
            }
            if(swipeAngle<-45 && swipeAngle>= -135){
            board.MoveBlock(Vector2.up);
            }
            

        }
        startPos=endPos=new Vector3(0,0,0);
    }


}

using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;


public class GameController : MonoBehaviour
{
    public static string level;
    public Board board;
    public float borderSize;
    public float width;
    public float height;
    public TextMeshProUGUI scoreText;
    public bool isOver=false;
    public int winTarget;
    public Slider slider;
    public EffectController effectController;
    public Button nextBtn;
    public GameObject winScreen;
    public GameObject loseScreen;
    
    
    public data tempData;


    void Awake(){
        scoreText.text="Level "+level;
        string filepath = Application.persistentDataPath+"/save.json";
        initiate(level);
        SetupCamera();
    }

    void Start(){
        slider.maxValue=winTarget;
        slider.value=0;
        slider.GetComponentInChildren<TextMeshProUGUI>().text=winTarget.ToString();
        if(int.Parse(level)==9)
        nextBtn.interactable=false;

        
    }


    void SetupCamera(){
        Camera.main.transform.position = new Vector3((float)(width - 1)/2,(float)(height - 1)/2, -10f);
        float aspectRatio= (float) Screen.width/ (float) Screen.height;
        float verticalSize= (float)height/2f + borderSize;
        float horizontalSize= ((float)width/2f +  borderSize)/aspectRatio;
        Camera.main.orthographicSize = verticalSize>horizontalSize? verticalSize:horizontalSize;
        Vector2 center= new Vector2((float) width/2 - 0.5f, (float) height/2 -0.5f);
        Camera.main.transform.position= new Vector3(center.x,center.y,-10f);
    }

    public void UpdateScore(){
        slider.value=board.allBlocks.Max(x=>x.value);
    }


    public void GameOver(){
        isOver=true;
        int nextLevel=int.Parse(level)+1;
        
        tempData.lockedLevels.Remove(nextLevel.ToString());
        string saveData=JsonUtility.ToJson(tempData);
        string filepath = Application.persistentDataPath+"/save.json";

        System.IO.File.WriteAllText(filepath,saveData);

    }

    public void GameWon(){
        GameOver();
        effectController.ConfetiEffect(new Vector3(-1,-1,0),new Vector3(width+1,-1,0));
        winScreen.SetActive(true);

    }

    public void GameLose(){
        GameOver();
        loseScreen.SetActive(true);
    }

    public void GoToHome(){
        SceneManager.LoadScene(0);
    }

    public void Restart(){
        level=level;
        SceneManager.LoadScene(1);
        
    }
    public void Next(){
        int nextLevel=(int.Parse(level)+1);
        level=nextLevel.ToString();
        SceneManager.LoadScene(1);     
    }

    public void Exit(){
        Application.Quit();
    }

    void initiate(string level){

        switch (level)
        {
            case "1":{
                width=3f;
                height=3f;
                winTarget=8;
            }
            break;
            case "2":{
                width=3f;
                height=3f;
                winTarget=16;
            }
            break;
            case "3":{
                width=3f;
                height=4f;
                winTarget=32;
            }
            break;
            case "4":{
                width=4f;
                height=4f;
                winTarget=64;
            }
            break;
            case "5":{
                width=5f;
                height=4f;
                winTarget=128;
            }
            break;
            case "6":{
                width=5f;
                height=5f;
                winTarget=256;
            }
            break;
            case "7":{
                width=5f;
                height=6f;
                winTarget=512;
            }
            break;
            case "8":{
                width=6f;
                height=6f;
                winTarget=1024;
            }
            break;
            case "9":{
                width=7f;
                height=6f;
                winTarget=2048;
            }
            break;
        }

        string filepath = Application.persistentDataPath+"/save.json";
        string saveData= System.IO.File.ReadAllText(filepath);
        tempData=JsonUtility.FromJson<data>(saveData);

    }

    


    [System.Serializable]
    public class data{
        public List<string> lockedLevels= new List<string>(){"2","3","4","5","6","7","8","9"};

    }



}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.IO;



public class LevelController : MonoBehaviour
{
    public GameObject levelHolder;
    public GameObject levelIcon;
    public int totalLevel;
    
    public data tempData;

    void Awake(){
        string filepath = Application.persistentDataPath+"/save.json";
        if(!File.Exists(filepath)){
            data data= new data();
            string createData=JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(filepath,createData);
        }
        string savedData= System.IO.File.ReadAllText(filepath);
        tempData=JsonUtility.FromJson<data>(savedData);
        
    }

    void Start(){
        Rect panelDimention= levelHolder.GetComponent<RectTransform>().rect;
        Rect iconDimention= levelIcon.GetComponent<RectTransform>().rect;
        int maxRow=Mathf.FloorToInt(panelDimention.width/iconDimention.width);
        int maxCol=Mathf.FloorToInt(panelDimention.height/iconDimention.height);
        int amountPerPage=maxCol*maxRow;
    
        LoadPanel(totalLevel,levelHolder);
    }

    void LoadPanel(int totalLevel, GameObject parent){
        for (int i = 1; i <= totalLevel; i++){
            GameObject icon=Instantiate(levelIcon);
            icon.transform.SetParent(transform,false);
            
            icon.transform.SetParent(parent.transform);
            icon.GetComponentInChildren<TextMeshProUGUI>().SetText(i.ToString());
            icon.name ="level "+i;
            if(tempData.lockedLevels.Any(x=>x==i.ToString())){
            icon.GetComponent<Button>().interactable=false;
            }
        }
    }

    public void Exit(){
        Application.Quit();
    }
    
    public class data{
        public List<string> lockedLevels= new List<string>(){"2","3","4","5","6","7","8","9"};
    }


}

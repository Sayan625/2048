using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class ScreenLoader : MonoBehaviour
{
    string level;

    public void LoadScene(){



        GameController.level=this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;


        SceneManager.LoadScene(1);
    }


}

using UnityEngine;
using TMPro;

public class EffectController : MonoBehaviour
{
    public TextMeshPro floatText;
    public float moveSpeed;
    public GameObject confeti;


    public void MoveEffect(Transform from, Transform to, float lapsedTime){
        float t=Mathf.Clamp(lapsedTime/moveSpeed,0,1);
        t=t*t*t*(t*(t*6-15)+10);
        if(from!=null && to!=null)
        from.position= Vector3.Lerp(from.position,to.position,t);
    }

    public void ConfetiEffect(Vector3 pos1, Vector3 pos2){
        GameObject confeti1=Instantiate(confeti,pos1,Quaternion.identity);
        GameObject confeti2=Instantiate(confeti,pos2,Quaternion.Euler(new Vector3(0,0,90)));
        Destroy(confeti1,2f);
        Destroy(confeti2,2f);
    }


}

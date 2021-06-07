using UnityEngine;
using UnityEngine.EventSystems;

public class RebootItem : MonoBehaviour,  IPointerClickHandler 
{
    
        
    public void OnPointerClick(PointerEventData eventData)
    {
        System.Random rnd1 = new System.Random();
       // seed = rnd1.Next(1111, 9999);

        System.Random rnd = new System.Random();
       // amplitude = rnd.Next(1, 5);

        System.Random rnd2 = new System.Random();
        //frequency = (float)rnd2.Next(1, 9) / 10f;
        
        if (eventData.pointerId == -1)
        {
            //Debug.Log("object name click = " + gameObject.name);
           var generator = this.GetComponent<NoiseMapGeneration>();
            generator.seed = rnd1.Next(1111, 9999); 
            generator.amplitude = rnd.Next(1, 5);
            generator.frequency = (float)rnd2.Next(1, 9) / 10f;
            GetComponent<TileGeneration>().GenerateTile();

        }
        
    }
       
    
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class EquipmentController : MonoBehaviour
{
    public Player player;
    public enum Equipment
    {
        Sword,
        Shoot
    }    

    public Equipment currentEquipment;
    [SerializeField] private GameObject[] EquipmentImages; 
    [SerializeField] private Image HoverImage;
    
    
    void Start()
    {
        currentEquipment = Equipment.Sword;
        
    }

   
    void Update()
    {   
            

        switch (currentEquipment) //state değişimleri player state classlarına göre oluyor.
        {
            case Equipment.Sword:
                ChangeEquipmentImage("SwordImage");
                HoverImage.DOFade(0.27f,0f);
                HoverImage.fillAmount=1;
                break;

            case Equipment.Shoot:
                ChangeEquipmentImage("ShootImage");
                HoverImage.fillAmount = Mathf.InverseLerp(0,player.ShootState.chargeTimerMax,player.ShootState.chargeTimer);
                if (HoverImage.fillAmount==1f)
                {
                    FullCharge();
                }
                break;
        }

    }

    private void FullCharge(){
        HoverImage.DOFade(0.5f,0.5f);
    }
    
   

    public void ChangeState(Equipment state){
        currentEquipment = state;

        

    }

    private void ChangeEquipmentImage(string current){
        foreach (var images in EquipmentImages)
        {   
            images.SetActive(false);
            if (images.name==current)
            {
                images.SetActive(true);
                
            }
        }
        
    }
}

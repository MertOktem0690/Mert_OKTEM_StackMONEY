using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedMoneyController : MonoBehaviour
{
    public BuildingController buildingController;

    private void OnTriggerEnter(Collider other)
    {
        if (buildingController.isItBuy)
        {   
            if (other.CompareTag("MoneyCollector"))
            {
                PlayerController.instance.playerScore += buildingController.moneyCount;

                if (PlayerController.instance.bag.transform.childCount<30)
                {
                    if (buildingController.moneyCount > 30 - PlayerController.instance.bag.transform.childCount)
                    {
                        //binan�n paray� almas�n� burada �a��r�yoruz
                        PlayerController.instance.MoneyFromBuilding( 30 - PlayerController.instance.bag.transform.childCount);
                    }
                    else
                    {
                        PlayerController.instance.MoneyFromBuilding(buildingController.moneyCount);
                    }
                }

                //parent objesi'nin child'lar�n� yok etme
                foreach (Transform child in buildingController.moneyHolder)
                {
                    Destroy(child.gameObject);
                }

                buildingController.moneyCount = 0;
                buildingController.xAxis = 0;
                buildingController.yAxis = -1;
                buildingController.zAxis = 0;
            }
        }

        //s�rt�m�zdaki paralar� buildValue kadar almas�n� sa�layan kod
        if (!buildingController.isItBuy)
        {
            //s�rt�m�zdaki paralar�n ne kadar�n� al�ca��n�n hesaplamas� 
            int scoreAtFirst = PlayerController.instance.playerScore;
            if (PlayerController.instance.playerScore < buildingController.buildValue)
            {
                if (buildingController.buildValue - buildingController.givenMoney >= PlayerController.instance.playerScore)
                {
                    buildingController.givenMoney += PlayerController.instance.playerScore;
                    PlayerController.instance.playerScore = 0;
                }
                else
                {
                    PlayerController.instance.playerScore -= (buildingController.buildValue - buildingController.givenMoney);
                    buildingController.givenMoney = buildingController.buildValue;
                }
            }
            else
            {
                if (buildingController.givenMoney == 0)
                {
                    buildingController.givenMoney = buildingController.buildValue;
                    PlayerController.instance.playerScore -= buildingController.buildValue;
                }
                else if (buildingController.givenMoney < buildingController.buildValue && buildingController.givenMoney > 0)
                {
                    PlayerController.instance.playerScore -= (buildingController.buildValue - buildingController.givenMoney);
                    buildingController.givenMoney = buildingController.buildValue;
                }
            }
            //s�rt�m�zdaki paralar� buildValue kadar ald���nda s�rt�m�zdaki paralar�n yok olmas�
            if (PlayerController.instance.bag.transform != null)
            {
                for (int i = 0; i < scoreAtFirst - PlayerController.instance.playerScore; i++)
                {
                    Destroy(PlayerController.instance.bag.transform.GetChild(i).gameObject);
                }
            }

            if (PlayerController.instance.playerScore > 0)
            {
                PlayerController.instance.MoneyFromBuilding(PlayerController.instance.playerScore);
            }

            if (buildingController.givenMoney==buildingController.buildValue)
            {
                buildingController.isItBuy = true;
            }
        }
    }
}
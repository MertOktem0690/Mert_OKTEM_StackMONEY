using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MoneyCollector"))
        {
            //skor sayma
            PlayerController.instance.playerScore++;

            if (PlayerController.instance.bag.transform.childCount < 30)
            {
                //s�rt�m�za al�nan paray� burada �a��r�yoruz
                PlayerController.instance.MoneytoBag();
            }
               
            //yerdeki paralar�n kaybolmas�
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(RegenerateMoney());
        }
    }

    //yerdeki paralar�n tekrar olu�mas�
    IEnumerator RegenerateMoney()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
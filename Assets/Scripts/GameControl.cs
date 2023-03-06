using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{
   
    public  List<GameObject> CollectablesObject = new List<GameObject>();
    public GameObject GameOverPanel;
    public GameObject GameWinPanel;
    private int Point = 0;
    public TextMeshProUGUI PointText;
  




    void Start()
    {
     
        InvokeRepeating("UpdateLeadTransform", 0.05f, 0.05f);
        PointText.text = Point.ToString();
      
       
    }

  
   
    public void UpdateLeadTransform()
    {
        for (int i = 0; i < CollectablesObject.Count; i++)
        {
            if (i == 0)
            {
                CollectablesObject[i].GetComponent<SmoothDamp>().CurrentLeadTransform = transform;
                CollectablesObject[i].transform.position = transform.position + new Vector3(0, -.75f, 2f);

            }
            else
            {
                CollectablesObject[i].GetComponent<SmoothDamp>().CurrentLeadTransform = CollectablesObject[i - 1].transform;
                CollectablesObject[i].transform.position = new Vector3(CollectablesObject[i].transform.position.x,
                                                                        CollectablesObject[i].transform.position.y,
                                                                        CollectablesObject[i - 1].transform.position.z + 2f);

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
         
            other.transform.gameObject.GetComponent<SmoothDamp>().enabled = true;
            CollectablesObject.Add(other.gameObject);
            other.transform.SetParent(transform.root);


        }
        else if (other.gameObject.CompareTag("Door"))
        {
            if(CollectablesObject.Count == 0)
            {
                GameOverPanel.SetActive(true);
                Movement.speed = 0f;
                Time.timeScale = 0;
            }
            else if(CollectablesObject.Count != 0)
            {
                PointText.text = CollectablesObject.Count.ToString();
                foreach (var Obj in CollectablesObject)
                {
                    Obj.GetComponent<SmoothDamp>().enabled = false;
                    Destroy(Obj.gameObject);
                }
                CollectablesObject.Clear();
               
            }
            
        }
        else if (other.gameObject.CompareTag("FinishLine"))
        {
            if (CollectablesObject.Count == 0)
            {
                GameOverPanel.SetActive(true);
                Movement.speed = 0f;
                Time.timeScale = 0;
            }
            else if (CollectablesObject.Count != 0)
            {

                Vector3 Temppos = CollectablesObject[CollectablesObject.Count - 1].transform.position;
                LeanTween.moveZ(transform.root.gameObject, Temppos.z, 1f).setOnComplete(    ()=> 
                {
                    foreach (var Obj in CollectablesObject)
                    {
                        Obj.GetComponent<SmoothDamp>().enabled = false;
                        Destroy(Obj.gameObject);


                    }
                    CollectablesObject.Clear();
                    if (CollectablesObject.Count == 0)
                    {
                        GameWinPanel.SetActive(true);
                        Movement.speed = 0f;


                    }

                });
            
            }
        }
    }
   
    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Movement.speed = 2f;
        Time.timeScale = 1;
    }
   
}

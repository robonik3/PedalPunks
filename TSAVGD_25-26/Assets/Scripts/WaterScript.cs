using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> overlayList;
    [SerializeField] private GameObject waterOverlay;
    [SerializeField] bool top;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animator>().Play((top ? "TopWave" : "BottomWave"));
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        for (i=0; i < overlayList.Count; i++) { overlayList[i].SetActive(false); }
        i = 0;
        foreach(Collider2D c in Physics2D.OverlapBoxAll(transform.position+new Vector3(0,top?1:0), transform.localScale, 0))
        {

            if (c.TryGetComponent(out EnemyScript cen))
            {
                if (cen.height == 0)
                {
                    
                    if (overlayList.Count <= i) { overlayList.Add(Instantiate(waterOverlay)); }
                    overlayList[i].transform.position = cen.transform.position + new Vector3(0,-.5f,0);
                    overlayList[i].SetActive(true);
                    i++;
                }
            }
            else if (c.TryGetComponent(out PlayerScript player))
            {
                if (player.height == 0)
                {

                    if (overlayList.Count <= i) { overlayList.Add(Instantiate(waterOverlay)); }
                    overlayList[i].transform.position = player.transform.position + new Vector3(0, -.5f, 0);
                    overlayList[i].SetActive(true);
                    i++;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerScript player))
        {
            player.speed = .8f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerScript player))
        {
            player.speed = 1;
        }
    }
    private void OnDisable()
    {
        for (int i=0; i < overlayList.Count; i++) { overlayList[i].SetActive(false); }

    }
    private void OnEnable()
    {
        GetComponent<Animator>().Play((top ? "TopWave" : "BottomWave"));

    }
}

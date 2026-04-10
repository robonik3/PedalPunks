using UnityEngine;

public class Level12Handler : MonoBehaviour
{
    public int CurrentLevel;
    [SerializeField] Color[] BGColors;
    [SerializeField] Transform roads;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLevel()
    {
        CurrentLevel++;
        Camera.main.backgroundColor = BGColors[CurrentLevel];
        for (int i=0;i<roads.childCount;i++)
        {
            Transform t = roads.GetChild(i);
            if (i == CurrentLevel) { t.gameObject.SetActive(true); }
            else { t.gameObject.SetActive(false); }
        }
    }
}

using UnityEngine;

public class Level12Handler : MonoBehaviour
{
    public int CurrentLevel;
    [SerializeField] Color[] BGColors;
    [SerializeField] Transform roads;
    [SerializeField] Transform props;
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

        for (int i=0;i<roads.childCount;i++)
        {
            Transform t = roads.GetChild(i);
            Transform tj = props.GetChild(i);
            if (i == CurrentLevel) { t.gameObject.SetActive(true); tj.gameObject.SetActive(true); }
            else { t.gameObject.SetActive(false); tj.gameObject.SetActive(false); }
        }
        Camera.main.backgroundColor = BGColors[CurrentLevel];
    }
}

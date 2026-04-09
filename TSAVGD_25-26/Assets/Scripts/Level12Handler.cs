using UnityEngine;

public class Level12Handler : MonoBehaviour
{
    public int CurrentLevel;
    [SerializeField] AudioClip[] songs;
    [SerializeField] Color[] BGColors;
    [SerializeField] Transform roads;
    [SerializeField] AudioSource songplayer;
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
        songplayer.clip = songs[CurrentLevel];
        songplayer.Play();
        Camera.main.backgroundColor = BGColors[CurrentLevel];
/*        foreach (Transform t in roads.GetComponentsInChildren<Transform>())
        {
            if (t.GetSiblingIndex() == CurrentLevel) { t.gameObject.SetActive(true); }
            else { t.gameObject.SetActive(false); }
        }*/
    }
}

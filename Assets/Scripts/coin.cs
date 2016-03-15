using UnityEngine;
using System.Collections;

public class coin : MonoBehaviour {
    public int coins = 3;//number of coins
    private int score=100;

    //public instance progress map
    public progressMap pm;
   

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OntriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.pm.totalScores += score;
        }
    }
}

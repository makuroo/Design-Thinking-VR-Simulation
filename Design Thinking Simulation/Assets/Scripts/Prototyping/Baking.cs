using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baking : MonoBehaviour
{
    public List<int> bowlContent = new();

    // Start is called before the first frame update
    void Start()
    {
        bowlContent = new() {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TepungTeriguAdded()
    {
        var temp = bowlContent[0];
        bowlContent.RemoveAt(0);
        bowlContent.Insert(0, temp + 1);
    }

    public void BakingPowderAdded()
    {
        var temp = bowlContent[1];
        bowlContent.RemoveAt(1);
        bowlContent.Insert(1, temp + 1);
    }

    public void GulaPasirAdded()
    {
        var temp = bowlContent[2];
        bowlContent.RemoveAt(2);
        bowlContent.Insert(2, temp + 1);
    }

    public void SeaSaltAdded()
    {
        var temp = bowlContent[3];
        bowlContent.RemoveAt(3);
        bowlContent.Insert(3, temp + 1);
    }

    public void BubukKakaoAdded()
    {
        var temp = bowlContent[4];
        bowlContent.RemoveAt(4);
        bowlContent.Insert(4, temp + 1);
    }

    public void MentegaAdded()
    {
        var temp = bowlContent[5];
        bowlContent.RemoveAt(5);
        bowlContent.Insert(5, temp + 1);
    }

    public void SusuAdded()
    {
        var temp = bowlContent[6];
        bowlContent.RemoveAt(6);
        bowlContent.Insert(6, temp + 1);
    }

    public void EkstrakVanillaAdded()
    {
        var temp = bowlContent[7];
        bowlContent.RemoveAt(7);
        bowlContent.Insert(7, temp + 1);
    }

    public void AirPerasanLemonAdded()
    {
        var temp = bowlContent[8];
        bowlContent.RemoveAt(8);
        bowlContent.Insert(8, temp + 1);
    }

    public void DarkChocolateAdded()
    {
        var temp = bowlContent[9];
        bowlContent.RemoveAt(9);
        bowlContent.Insert(9, temp + 1);
    }

    public void LemonZestAdded()
    {
        var temp = bowlContent[9];
        bowlContent.RemoveAt(9);
        bowlContent.Insert(9, temp + 1);
    }
}

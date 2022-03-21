using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TropheObtenu : MonoBehaviour
{
    bool valid = false;
    // Start is called before the first frame update
    void Start()
    {
        valid = GameManager.Instance
            .PlayerData.checkTrophe(this.gameObject.name);
        
        if (valid)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

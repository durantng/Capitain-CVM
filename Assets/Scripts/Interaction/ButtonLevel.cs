using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLevel : MonoBehaviour
{
    int level;
    string Buttonname;
    // Start is called before the first frame update
    void Start()
    {
        Buttonname = gameObject.name;

        level = GameManager.Instance
            .PlayerData.level;

        if (level == 2 && name == "ButtonNiv2")
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
        else if (level == 3 && name == "ButtonNiv3")
        {
            this.gameObject.GetComponent<Button>().interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

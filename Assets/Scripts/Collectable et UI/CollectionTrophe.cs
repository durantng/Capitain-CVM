using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionTrophe : MonoBehaviour
{
    /// <summary>
    /// Valeur de l'énergie regagner au contact
    /// </summary>
    [SerializeField]
    private int _gain = 1;
    [SerializeField]
    private AudioClip _clip;

    bool valid = false;

    private void Start()
    {
        valid = GameManager.Instance
            .PlayerData.checkTrophe(this.gameObject.name);

        if (valid)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.AudioManager
                .PlayClipAtPoint(_clip, this.transform.position);
            GameManager.Instance
                .PlayerData.ajouterTrophe(this.gameObject.name);
            GameObject.Destroy(this.gameObject);
        }
    }
}

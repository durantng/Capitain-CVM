using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Représente les données de jeu
/// </summary>
[System.Serializable]
public class PlayerData
{
    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. général
    /// </summary>
    [Range(-80, 0)]
    private float _volumeGeneral = 0;
    public float VolumeGeneral { get { return _volumeGeneral; } set { _volumeGeneral = value; } }

    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. de la musique
    /// </summary>
    [Range(-80, 0)]
    private float _volumeMusique = 0;
    public float VolumeMusique { get { return _volumeMusique; } set { _volumeMusique = value; } }

    /// <summary>
    /// Niveau sélectionné par l'utilisateur pour le vol. de la musique
    /// </summary>
    [Range(-80, 0)]
    private float _volumeEffet = 0;
    public float VolumeEffet { get { return _volumeEffet; } set { _volumeEffet = value; } }

    /// <summary>
    /// Représente le nombre de points de vie du personnage
    /// </summary>
    private int _vie;
    /// <summary>
    /// Représente le nombre d'énergie (entre 0 et 4)
    /// </summary>
    private int _energie;
    /// <summary>
    /// Liste des trophés collectionnés dans le jeu
    /// </summary>
    private List<string> listTrophe;
    /// <summary>
    /// Représente le score obtenu
    /// </summary>
    private int _score;
    /// <summary>
    /// Représente le niveau maximal ateint
    /// </summary>
    private int levelMax;
    /// <summary>
    /// Liste des coffres ouverts dans le jeu
    /// </summary>
    private List<string> _chestOpenList;
    /// <summary>
    /// Représente le maximum d'énergie du personnage
    /// </summary>
    public const int MAX_ENERGIE = 4;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteEnergie;
    /// <summary>
    /// Permet d'identifier les actions sur le UI à réaliser
    /// lors de la perte d'énergie
    /// </summary>
    public System.Action UIPerteVie;
    /// <summary>
    /// Permet d'identifier les actions à réaliser lors d'un gameover
    /// </summary>
    public System.Action Gameover;

    public int Energie { get { return this._energie; } }
    public int Vie { get { return this._vie; } }
    public int Score { get { return this._score; } }
    public string[] ListeCoffreOuvert { get { return this._chestOpenList.ToArray(); } }
    public List<string> getListTrophe { get { return this.listTrophe; } }
    public int level { get { return this.levelMax; } }
    public void setlevel(int level) { this.levelMax = level; }

    public PlayerData()
    {
        this._vie = 0;
        this._energie = 0;
        this._score = 0;
        this._volumeGeneral = 0;
        this._volumeMusique = 0;
        this._volumeEffet = 0;
        this.UIPerteEnergie = null;
        this.UIPerteVie = null;
        this.Gameover = null;
        this._chestOpenList = new List<string>();
        this.listTrophe = new List<string>();
        this.levelMax = 1;
    }

    public PlayerData(int vie = 1, int energie = 2, int score = 0,
        float volumeGeneral = 0, float volumeMusique = 0, float volumeEffet = 0,
        System.Action uiPerteEnergie = null, System.Action uiPerteVie = null,
        System.Action gameOver = null, List<string> ChestList = null, List<string> listeTrophe = null, int niveauMaximal=0)
    {
        this._vie = vie;
        this._energie = energie;
        this._score = score;
        this._volumeGeneral = volumeGeneral;
        this._volumeMusique = volumeMusique;
        this._volumeEffet = volumeEffet;
        this.UIPerteEnergie += uiPerteEnergie;
        this.UIPerteVie += uiPerteVie;
        this.Gameover += gameOver;
        this._chestOpenList = new List<string>();
        this.listTrophe = new List<string>();
        this.levelMax = niveauMaximal;
        if (ChestList != null)
            this._chestOpenList = ChestList;

        if (listeTrophe != null)
            this.listTrophe = listeTrophe;
    }


    /// <summary>
    /// Diminue l'énergie du personnage
    /// </summary>
    /// <param name="perte">Niveau de perte (par défaut 1)</param>
    public void DecrEnergie(int perte = 1)
    {
        this._energie -= perte;
        this.UIPerteEnergie();
        if (this._energie <= 0)
        {
            this.DecrVie();
        }
    }

    /// <summary>
    /// Permet de réduire la vie d'un personnage
    /// </summary>
    public void DecrVie()
    {
        this._vie--;
        this.UIPerteVie();
        if (this._vie <= 0)
            this.Gameover();
        else
        {
            this.IncrEnergie(MAX_ENERGIE);
            GameManager.Instance.RechargerNiveau();
        }
    }

    /// <summary>
    /// Permet d'augmenter l'énergie jusqu'à MAX_ENERGIE
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrEnergie(int gain)
    {
        this._energie += gain;
        if (this._energie > MAX_ENERGIE)
        {
            this._energie = 1;
            this.IncrVie();
        }
        
        this.UIPerteEnergie();
    }

    /// <summary>
    /// Permet d'augmenter la vie
    /// </summary>
    /// <param name="gain">Gain d'augmentation</param>
    public void IncrVie(int gain = 1)
    {
        this._vie += gain;
        this.UIPerteVie();
    }

    /// <summary>
    /// Augmente le score du joueur
    /// </summary>
    /// <param name="gain">Point gagné</param>
    public void IncrScore(int gain = 1)
    {
        this._score += gain;
    }

    /// <summary>
    ///  ajoute dans la liste des trophés du joueur
    /// </summary>
    /// <param name="gain">Point gagné</param>
    public void ajouterTrophe(string nom)
    {
        listTrophe.Add(nom);
    }

    /// <summary>
    /// Verifie si le joueur a remporté le trophé designé
    /// </summary>
    /// <param name="gain">Point gagné</param>
    public bool checkTrophe(string nom)
    {
        return listTrophe.Contains(nom);
    }

    /// <summary>
    /// Ajoute le nom du coffre à la liste
    /// </summary>
    /// <param name="nom">Nom du coffre à ajouter</param>
    public void AjouterCoffreOuvert(string nom)
    {
        this._chestOpenList.Add(nom);
    }

    /// <summary>
    /// Détermine si le coffre est contenu dans la liste
    /// des coffres ouverts
    /// </summary>
    /// <param name="nom">Nom du coffre à vérifier</param>
    /// <returns>true si le coffre est ouvert, false sinon</returns>
    public bool AvoirOuvertureCoffre(string nom)
    {
        return this._chestOpenList.Contains(nom);
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private GameObject upgradesList;
    [SerializeField] int numberOfCardsOnScreen;
    List<Upgrade> chosenUpgrades;

    List<Upgrade> AllUpgrades = new List<Upgrade>()
    {
        new Upgrade {Name = "upgrade 1", Description = "Aumenta a aaa em _%", Increase = 20},
        new Upgrade {Name = "upgrade 2", Description = "Aumenta a aaa em _%", Increase = 20},
        new Upgrade {Name = "upgrade 3", Description = "Aumenta a aaa em _%", Increase = 20},
        new Upgrade {Name = "upgrade 4", Description = "Aumenta a aaa em _%", Increase = 20},
        new Upgrade {Name = "upgrade 5", Description = "Aumenta a aaa em _%", Increase = 20}
    };

    private void Start()
    {
        GameEvents.current.onPlayerUpgradeTrigger += UpgradeCardSetup;
        gameObject.SetActive(false);
    }

    public void UpgradeCardSetup()
    {
        if (AllUpgrades.Count <= 0)
        {
            return;
        }

        Time.timeScale = 0;
        gameObject.SetActive(true);

        //Embaralha as cartas (upgrades)
        Shuffle(AllUpgrades);
        if (AllUpgrades.Count < numberOfCardsOnScreen)
        {
            numberOfCardsOnScreen = AllUpgrades.Count;
        }

        //Escolhe um numero x (numberOfCardsOnScreen) de cartas aleatoreamente
        chosenUpgrades = new List<Upgrade>(numberOfCardsOnScreen);
        for (int i = 0; i < numberOfCardsOnScreen; i++)
        {
            chosenUpgrades.Add(AllUpgrades[i]);
        }

        for (int i = 0; i < numberOfCardsOnScreen; i++)
        {
            Instantiate(upgradePrefab, upgradesList.transform); //Instanciando as cartas escolhidas aleatoriamente
        }

        setupCardText();

    }

    private void setupCardText() //Coloca os textos e propriedades no cards escolhidos
    {
        for (int i = 0; i < numberOfCardsOnScreen; i++)
        {
            Upgrade card = chosenUpgrades[i];

            GameObject upgradeCard = upgradesList.transform.GetChild(i).gameObject;

            TMP_Text upgradeTextName = upgradeCard.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            upgradeTextName.text = card.Name;

            //SpriteRenderer upgradeImage = upgradeCard.transform.GetChild(0).transform.GetChild(1).GetComponent<>();

            TMP_Text upgradeTextDescription = upgradeCard.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>();
            upgradeTextDescription.text = card.Description;

            Button upgradeButton = upgradeCard.transform.GetChild(0).GetComponent<Button>();
            upgradeButton.onClick.AddListener(() => { ChoosedUpgrade(card); });
        }
    }

    private void ChoosedUpgrade(Upgrade choosedUpgrade) //Adiciona os upgrades nas propriedades devidas.
    {
        AllUpgrades.Remove(choosedUpgrade); //Remove o update escolhido da lista original para que ele não seja chamado novamente.

        switch (choosedUpgrade.Name)
        {
            case "upgrade 1":
                Debug.Log("UPGRADE:" + "upgrade 1");
                break;
            
            case "upgrade 2":
                Debug.Log("UPGRADE:" + "upgrade 2");
                break;
            
            case "upgrade 3":
                Debug.Log("UPGRADE:" + "upgrade 3");
                break;
            
            case "upgrade 4":
                Debug.Log("UPGRADE:" + "upgrade 4");
                break;

            case "upgrade 5":
                Debug.Log("UPGRADE:" + "upgrade 5");
                break;
        }
        gameObject.SetActive(false);
        CleanCardList();
        Time.timeScale = 1;
    }

    private void Shuffle(List<Upgrade> list) //Algoritmo Fisher-Yates
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public void SkipUpgrades()
    {
        gameObject.SetActive(false);
    }

    private void CleanCardList() //Limpa as instâncias de card na lista de upgrades para poder colocar novos cards.
    {
        for (int i = 0; i < upgradesList.transform.childCount; i++)
        {
            Destroy(upgradesList.transform.GetChild(i).gameObject);
        }
    }

}

//Upgrade proprieties
public class Upgrade
{
    public string Name { get; set; }
    //public Sprite Image { get; set; }
    public string Description { get; set; }
    public float Increase { get; set; }
}
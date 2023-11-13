using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private GameObject upgradesList;
    [SerializeField] private Sprite lifeIcon;
    [SerializeField] private Sprite defenseIcon;
    [SerializeField] private Sprite attackIcon;
    [SerializeField] private Sprite engineIcon;
    [SerializeField] int numberOfCardsOnScreen;
    List<Upgrade> chosenUpgrades;
    [SerializeField] private Player player;
    [SerializeField] private PlayerShield shield;
    [SerializeField] private UIImpulseController impulseUI;
    [SerializeField] private AudioSource upgradeSFX;

    public List<Upgrade> AllUpgrades = new List<Upgrade>()
    {
        //Life
        new Upgrade { Id = 1, Name = "LIFE", Description = "Recovers _ hit point", Increase = 1, Category = UpgradeCategory.Life },
        new Upgrade { Id = 2, Name = "LIFE", Description = "Recovers _ hit points", Increase = 2, Category = UpgradeCategory.Life },
        new Upgrade { Id = 3, Name = "LIFE", Description = "Recovers _ hit points", Increase = 4, Category = UpgradeCategory.Life },

        //Defense
        new Upgrade { Id = 4, Name = "DEFENSE", Description = "Decrease shild cooldown time in _%", Increase = 50, Category = UpgradeCategory.Defense },
        new Upgrade { Id = 5, Name = "DEFENSE", Description = "Increase shild active time in _%", Increase = 50, Category = UpgradeCategory.Defense },

        //Attack
        new Upgrade { Id = 6, Name = "ATTACK", Description = "Increase fire speed in _", Increase = 0.1F, Category = UpgradeCategory.Attack }, //cooldown de 0.4 -> 0.3
        new Upgrade { Id = 7, Name = "ATTACK", Description = "Increase fire speed in _", Increase = 0.1F, Category = UpgradeCategory.Attack }, //cooldown de 0.3 -> 0.2
        
        //Engine
        new Upgrade { Id = 8, Name = "ENGINE", Description = "Boost uses _% less gasoline", Increase = 50, Category = UpgradeCategory.Engine }, //Diminui a Decrease impulse variable
        new Upgrade { Id = 9, Name = "ENGINE", Description = "Increases boost recharge speed in _%", Increase = 400, Category = UpgradeCategory.Engine }, //Aumenta a Increase impulse variable
        new Upgrade { Id = 10, Name = "ENGINE", Description = "Increases impulse tank size in _%", Increase = 100, Category = UpgradeCategory.Engine } //Aumenta a Increase impulse variable
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

            Image upgradeImage = upgradeCard.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>();
            switch (card.Category)
            {
                case UpgradeCategory.Life:
                    upgradeImage.sprite = lifeIcon;
                    break;

                case UpgradeCategory.Attack:
                    upgradeImage.sprite = attackIcon;
                    break;

                case UpgradeCategory.Defense:
                    upgradeImage.sprite = defenseIcon;
                    break;

                case UpgradeCategory.Engine:
                    upgradeImage.sprite = engineIcon;
                    break;
            }

            TMP_Text upgradeTextDescription = upgradeCard.transform.GetChild(0).transform.GetChild(2).GetComponent<TMP_Text>();
            string cardDescription = card.Description.Replace("_", card.Increase.ToString());
            upgradeTextDescription.text = cardDescription;

            Button upgradeButton = upgradeCard.transform.GetChild(0).GetComponent<Button>();
            upgradeButton.onClick.AddListener(() => { ChoosedUpgrade(card); });
        }
    }

    private void ChoosedUpgrade(Upgrade choosedUpgrade) //Adiciona os upgrades nas propriedades devidas.
    {
        upgradeSFX.Play();
        switch (choosedUpgrade.Id)
        {
            case 1:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.currentHealth += (int)choosedUpgrade.Increase;
                player.healthBar.SetHealth(player.currentHealth, player.maxHealth);
                break;

            case 2:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.currentHealth += (int)choosedUpgrade.Increase;
                player.healthBar.SetHealth(player.currentHealth, player.maxHealth);
                break;

            case 3:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.currentHealth += (int) choosedUpgrade.Increase;
                player.healthBar.SetHealth(player.currentHealth, player.maxHealth);
                break;

            case 4:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                shield.shieldRechargeTime = shield.shieldRechargeTime * choosedUpgrade.Increase / 100;
                break;

            case 5:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                shield.shieldActiveTime *= (1 + (choosedUpgrade.Increase / 100)); //1,5 ou 150%
                break;

            case 6:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.shootCooldown -= choosedUpgrade.Increase;
                break;

            case 7:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.shootCooldown -= choosedUpgrade.Increase;
                break;

            case 8:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.decreaseImpulseVariantValue *= choosedUpgrade.Increase / 100;
                break;

            case 9:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.increaseImpulseVariantValue *= choosedUpgrade.Increase / 100;
                break;

            case 10:
                Debug.Log("UPGRADE - " + choosedUpgrade.Name + ": " + choosedUpgrade.Description);
                player.maxImpulse += choosedUpgrade.Increase;
                impulseUI.SetMaxImpulse(player.maxImpulse);
                break;

        }
        AllUpgrades.Remove(choosedUpgrade); //Remove o update escolhido da lista original para que ele não seja chamado novamente.
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
    public int Id { get; set; }
    public string Name { get; set; }
    //public Sprite Image { get; set; }
    public string Description { get; set; }
    public float Increase { get; set; }
    public UpgradeCategory Category { get; set; }
}

public enum UpgradeCategory
{
    Life,
    Attack,
    Defense,
    Engine
}
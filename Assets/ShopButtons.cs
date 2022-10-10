using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class ShopButtons : MonoBehaviour
{
    [System.Serializable]
    public class ShopItems
    {
        public Sprite Image;
        public int Price;
        public int PriceNewCoins;
        public bool IsPurchased = false;
        public bool IsEquipped = false;
    }

    [SerializeField] public List<ShopItems> ItemsList;
    [SerializeField] public List<ShopItems> ItemsListClone;
    [SerializeField] TextMeshProUGUI CoinsText;
    [SerializeField] TextMeshProUGUI GoldCoinsText;
    public TextMeshProUGUI NoCoinsText;
    public Game game;
    GameObject ItemTemplate;
    GameObject g;
    Button BuyButton;
    [SerializeField] Transform ShopScrollView;

    [Header("SaveData")]
    [SerializeField] string filename;


    [Header("«¿À”œ¿")]
    public Sprite GoldenCoinSprite;
    public Sprite CoinSprite;
    public GameObject SkinSelect;
    public TextMeshProUGUI PriceSelect;
    public Button SelectBuyButton;
    Button SelectButton;
    public GameObject ButtonPlacement;
    GameObject ga;
    GameObject buttontocopy;
    public GameObject Knife;
    Button EquipButton;
    AudioManager audioManager;
    public GameObject SkinMenu;
    private void Awake()
    {
        ItemsListClone = ItemsList;
    }
    private void Start()
    {
        SetCoins();
      //  FirstLoadShop();
        LoadShop();
        audioManager = FindObjectOfType<AudioManager>();
        NewItemsInShop();
        CreateItemShop();
        
    }

    void CreateItemShop()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int lenght = ItemsList.Count;
        for (int i = 0; i < lenght; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);
            g.transform.GetChild(0).GetComponent<Image>().sprite = ItemsList[i].Image;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemsList[i].Price.ToString();
            BuyButton = g.transform.GetChild(2).GetComponent<Button>();
            BuyButton.interactable = !ItemsList[i].IsPurchased;
            BuyButton.AddEventListener(i, OnShopItemBuyButtonClicked);
            SelectButton = g.transform.GetChild(0).GetChild(0).GetComponent<Button>();


            if (ItemsList[i].IsPurchased)
            {
                EquipButton = g.transform.GetChild(3).GetComponent<Button>();
                SelectButton.AddEventListener(i, SelectSkinToEquip);
            }
            else if(ItemsList[i].Price > ItemsList[i].PriceNewCoins)
            {
                SelectButton.AddEventListener(i, SelectSkinToBuy);
            }
            else
            {
                g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ItemsList[i].PriceNewCoins.ToString();
                SelectButton.AddEventListener(i, SelectSkinToBuyForGold);
            }


        }
        Destroy(ItemTemplate);
        SetCoins();
        EquipOnStart();
    }

    void ReloadItemShop()
    {
        PlayerPrefs.SetInt("Bought", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnShopItemBuyButtonClicked(int itemIndex)
    {
        if (game.HasEnoughCoins(ItemsList[itemIndex].Price))
        {
            game.UseCoins(ItemsList[itemIndex].Price);
            ItemsList[itemIndex].IsPurchased = true;
            SaveShop();

            ga.GetComponent<Button>().interactable = false;

            ga.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
            Destroy(ga.gameObject);


            SetCoins();

             ReloadItemShop();
        

        }
        else
        {
            NoCoinsText.gameObject.SetActive(true);
            NoCoinsText.GetComponent<Animation>().Play();
            audioManager.Play("Fisting");
        }

    }
    void OnShopItemBuyButtonClickedForGold(int itemIndex)
    {
        if (game.HasEnoughGoldCoins(ItemsList[itemIndex].Price))
        {
            game.UseGoldCoins(ItemsList[itemIndex].PriceNewCoins);
            ItemsList[itemIndex].IsPurchased = true;
            SaveShop();

            ga.GetComponent<Button>().interactable = false;

            ga.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Purchased";
            Destroy(ga.gameObject);


            SetCoins();

            ReloadItemShop();


        }
        else
        {
            NoCoinsText.gameObject.SetActive(true);
            NoCoinsText.GetComponent<Animation>().Play();
            audioManager.Play("Fisting");
        }

    }
    
    void SetCoins()
    {
        game = FindObjectOfType<Game>();
        CoinsText.text = game.Coins.ToString();
        GoldCoinsText.text = PlayerPrefs.GetInt("GoldApplesCount").ToString();
    }
    void SelectSkinToBuy(int itemIndex)
    {
        SkinSelect.GetComponent<Image>().sprite = ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite;
        SkinSelect.gameObject.SetActive(true);
        PriceSelect.text = ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<TextMeshProUGUI>().text;
        PriceSelect.gameObject.SetActive(true);
        PriceSelect.transform.GetChild(0).GetComponent<Image>().sprite = CoinSprite;
        //
        BuyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        buttontocopy = BuyButton.gameObject;
        ga = Instantiate(buttontocopy, ButtonPlacement.transform);
        ga.GetComponent<Button>().AddEventListener(itemIndex, OnShopItemBuyButtonClicked);
        ga.transform.localPosition = Vector3.zero;
        ga.gameObject.SetActive(true);
        Destroy(ga.GetComponent<DestroyButton>(), 0.1f);

    }
    void SelectSkinToBuyForGold(int itemIndex)
    {
        SkinSelect.GetComponent<Image>().sprite = ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite;
        SkinSelect.gameObject.SetActive(true);
        PriceSelect.text = ShopScrollView.GetChild(itemIndex).GetChild(1).GetComponent<TextMeshProUGUI>().text;
        PriceSelect.gameObject.SetActive(true);
        PriceSelect.transform.GetChild(0).GetComponent<Image>().sprite = GoldenCoinSprite;
        //
        BuyButton = ShopScrollView.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
        buttontocopy = BuyButton.gameObject;
        ga = Instantiate(buttontocopy, ButtonPlacement.transform);
        ga.GetComponent<Button>().AddEventListener(itemIndex, OnShopItemBuyButtonClickedForGold);
        ga.transform.localPosition = Vector3.zero;
        ga.gameObject.SetActive(true);
        Destroy(ga.GetComponent<DestroyButton>(), 0.1f);

    }
    void SelectSkinToEquip(int itemIndex)
    {
        SkinSelect.GetComponent<Image>().sprite = ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite;
        SkinSelect.gameObject.SetActive(true);
        PriceSelect.gameObject.SetActive(false);
        BuyButton = ShopScrollView.GetChild(itemIndex).GetChild(3).GetComponent<Button>();
        buttontocopy = BuyButton.gameObject;
        ga = Instantiate(buttontocopy, ButtonPlacement.transform);
        ga.transform.localPosition = Vector3.zero;
        ga.GetComponent<Button>().AddEventListener(itemIndex, EquipButtonWork);
        ga.gameObject.SetActive(true);
        Destroy(ga.GetComponent<DestroyButton>(), 0.1f);





    }
    public void EquipButtonWork(int itemIndex)
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int lenght = ItemsList.Count;
        for (int i = 0; i < lenght; i++)
        {
            if (ItemsList[i].IsEquipped)
            {
                ItemsList[i].IsEquipped = false;
            }

        }
        ItemsList[itemIndex].IsEquipped = true;
        Knife.GetComponent<SpriteRenderer>().sprite = ShopScrollView.GetChild(itemIndex).GetChild(0).GetComponent<Image>().sprite;
        SaveShop();
        Destroy(ga);
    }
    public void EquipOnStart()
    {
        ItemTemplate = ShopScrollView.GetChild(0).gameObject;
        int lenght = ItemsList.Count;
        for (int i = 0; i < lenght; i++)
        {
            if (ItemsList[i].IsEquipped)
            {
                Knife.GetComponent<SpriteRenderer>().sprite = ShopScrollView.GetChild(i + 1).GetChild(0).GetComponent<Image>().sprite;
            }
            SaveShop();
        }
    }
    public void SaveShop()
    {
        SaveData.SaveToJSON<ShopItems>(ItemsList, filename);
    }
    public void LoadShop() 
    {
        ItemsList = SaveData.ReadFromJSON<ShopItems>(filename);
    }
    public void NewItemsInShop()
    {
        if(ItemsListClone.Count > ItemsList.Count)
        {
            int i = ItemsListClone.Count - ItemsList.Count;
            int ToRemove = ItemsListClone.Count - i;
            Debug.Log(ToRemove);
            ItemsListClone.RemoveRange(0,ToRemove);
            ItemsList.AddRange(ItemsListClone);
            SaveShop();
        }
    }
    void FirstLoadShop()
    {
        if(PlayerPrefs.GetInt("FirstShopLoad") == 0)
        {
            SaveShop();
            PlayerPrefs.SetInt("FirstShopLoad", 1);
        }
    }
}


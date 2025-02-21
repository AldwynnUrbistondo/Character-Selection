using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.U2D.Animation;

public class CharacterData : MonoBehaviour
{

    public List<Character> GameCharacters = new List<Character>();

    [Header("Layout")]
    public Sprite[] elementBGSprites;
    /*
        0 - Pyro
        1 - Dendro
        2 - Hydro
        3 - Cryo
        4 - Anemo
        5 - Geo
        6 - Electro
    */
    public Sprite[] characterSprites;

    public Image elementBackground;
    public Image characterImage;
    public Image characterGhostImage;
    
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI genderText;
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI nationText;
    public TextMeshProUGUI visionText;

    public TextMeshProUGUI descriptionText;

    [Header("Button UI")]
    public GameObject itemPrefab;
    public Sprite[] visionImages;
    /*
        0 - Pyro
        1 - Dendro
        2 - Hydro
        3 - Cryo
        4 - Anemo
        5 - Geo
        6 - Electro
    */

    public Transform itemLayout;

    public Animator buttonLayouts;
    public Animator infoUI;


    [Header("Boolean")]
    public bool canClickButton = false;
    public bool canClickScreen = false;


    void Start()
    {
       StartCoroutine(SpawnButtons());
    }

    private void Update()
    {
        if (canClickScreen)
        {
            if (Input.GetMouseButtonDown(0))
            {
                buttonLayouts.SetTrigger("Show");
                infoUI.SetTrigger("Hide");

                canClickScreen = false;

                StartCoroutine(DelayButtonClickable());
            }
        }
    }

    public void SetGameCharacterListData()
    {
        Character yoimiya = new Character("Yoimiya", Gender.Female, Nation.Inazuma, Weapon.Bow, Vision.Pyro, "The Queen of Summer Festival", 0);
        Character keqing = new Character("Keqing", Gender.Female, Nation.Liyue, Weapon.Sword, Vision.Electro, "Yuheng of the Liyue Qixing", 1);
        Character nahida = new Character("Nahida", Gender.Female, Nation.Sumeru, Weapon.Catalyst, Vision.Dendro, "God of Wisdom", 2);
        Character furina = new Character("Furina", Gender.Female, Nation.Fontaine, Weapon.Sword, Vision.Hydro, "An Elegant and Delicate Performer", 3);
        Character childe = new Character("Childe", Gender.Male, Nation.Sneznhaya, Weapon.Bow, Vision.Hydro, "11th of the Fatui Harbingers", 4);
        Character venti = new Character("Venti", Gender.Male, Nation.Mondstadt, Weapon.Bow, Vision.Anemo, "God of Freedom", 5);
        Character itto = new Character("Itto", Gender.Male, Nation.Inazuma, Weapon.Claymore, Vision.Geo, "Leader of Arataki Gang", 6);
        Character citlali = new Character("Citlali", Gender.Female, Nation.Natlan, Weapon.Catalyst, Vision.Cryo, "A legendary shaman from the Masters of the Night-Wind", 7);

        GameCharacters.Add(yoimiya);
        GameCharacters.Add(keqing);
        GameCharacters.Add(nahida);
        GameCharacters.Add(furina);
        GameCharacters.Add(childe);
        GameCharacters.Add(venti);
        GameCharacters.Add(itto);
        GameCharacters.Add(citlali);
    }

    public void CreateCharacterButton(Character character)
    {
        GameObject newItem = Instantiate(itemPrefab, itemLayout); // Spawn a new button and create an object variable

        ButtonScript itemButton = newItem .GetComponent<ButtonScript>(); // Get the script from the object

        Button button = itemButton.GetComponent<Button>();  // Create a button variable form the object



        itemButton.SetData(character);

        button.onClick.AddListener(() => OnCharacterButtonClick(character));

    }

    public void OnCharacterButtonClick(Character character)
    {
        if (canClickButton)
        {
            SetLayout(character);
            buttonLayouts.SetTrigger("Hide");
            infoUI.SetTrigger("Show");

            canClickButton = false;

            StartCoroutine(DelayScreenClickable());
        }
    }

    public void SetLayout(Character character)
    {
        characterImage.sprite = characterSprites[character.imageValue];
        characterImage.SetNativeSize();

        characterGhostImage.sprite = characterSprites[character.imageValue];
        characterGhostImage.SetNativeSize();

        characterNameText.text = character.name;

        genderText.text = "Gender: " + character.gender.ToString();     weaponText.text = "Weapon: " + character.weapon.ToString();
        nationText.text = "Nation: " + character.nation.ToString();     visionText.text = "Vision: " + character.vision.ToString();

        descriptionText.text = '"' + character.description + '"';

        #region Element Background

        if (character.vision == Vision.Pyro)
        {
            elementBackground.sprite = elementBGSprites[0];
        }
        else if (character.vision == Vision.Dendro)
        {
            elementBackground.sprite = elementBGSprites[1];
        }
        else if (character.vision == Vision.Hydro)
        {
            elementBackground.sprite = elementBGSprites[2];
        }
        else if (character.vision == Vision.Cryo)
        {
            elementBackground.sprite = elementBGSprites[3];
        }
        else if (character.vision == Vision.Anemo)
        {
            elementBackground.sprite = elementBGSprites[4];
        }
        else if (character.vision == Vision.Geo)
        {
            elementBackground.sprite = elementBGSprites[5];
        }
        else if (character.vision == Vision.Electro)
        {
            elementBackground.sprite = elementBGSprites[6];
        }

        #endregion
    }

    IEnumerator SpawnButtons()
    {
        SetGameCharacterListData();

        foreach (Character c in GameCharacters)
        {
            CreateCharacterButton(c);

            yield return new WaitForSeconds(0.15f);
        }

        canClickButton = true;
    }

    IEnumerator DelayScreenClickable()
    {
        yield return new WaitForSeconds(2);

        canClickScreen = true;
    }

    IEnumerator DelayButtonClickable()
    {
        yield return new WaitForSeconds(2);

        canClickButton = true;
    }
}


#region Character Class

[System.Serializable]
public class Character
{
    // Character Info
    public string name;
    public Gender gender;
    public Nation nation;
    public Weapon weapon;
    public Vision vision;
    public string description;

    public int imageValue;

    public Character(string name, Gender gender, Nation nation, Weapon weapon, Vision vision, string description, int imageValue)
    {
        this.name = name;
        this.gender = gender;
        this.nation = nation;
        this.weapon = weapon;
        this.vision = vision;
        this.description = description;

        this.imageValue = imageValue;
    }
}

public enum Gender { Male, Female };
public enum Nation { Mondstadt, Liyue, Inazuma, Sumeru, Fontaine, Natlan, Sneznhaya };
public enum Weapon { Sword, Claymore, Bow, Polearm, Catalyst };
public enum Vision { Pyro, Hydro, Anemo, Electro, Dendro, Cryo, Geo };

#endregion

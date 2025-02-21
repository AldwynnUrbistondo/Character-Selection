using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public TextMeshProUGUI name;

    CharacterData characterData;

    public void SetData(Character character)
    {
        SetButtonVision(character);

        name.text = character.name;
    }


    void SetButtonVision(Character character)
    {
        characterData = FindObjectOfType<CharacterData>();
        Image buttomImage = GetComponent<Image>();

        if (character.vision == Vision.Pyro)
        {
            buttomImage.sprite = characterData.visionImages[0];
        }
        else if (character.vision == Vision.Dendro)
        {
            buttomImage.sprite = characterData.visionImages[1];
        }
        else if (character.vision == Vision.Hydro)
        {
            buttomImage.sprite = characterData.visionImages[2];
        }
        else if (character.vision == Vision.Cryo)
        {
            buttomImage.sprite = characterData.visionImages[3];
        }
        else if (character.vision == Vision.Anemo)
        {
            buttomImage.sprite = characterData.visionImages[4];
        }
        else if (character.vision == Vision.Geo)
        {
            buttomImage.sprite = characterData.visionImages[5];
        }
        else if (character.vision == Vision.Electro)
        {
            buttomImage.sprite = characterData.visionImages[6];
        }
    }
}

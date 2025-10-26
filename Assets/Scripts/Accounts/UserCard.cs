using TMPro;
using UnityEngine;

public class UserCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _realNameText;
    [SerializeField] private TMP_Text _displayNameText;
    [SerializeField] private TMP_Text _ageText;
    [SerializeField] private TMP_Text _emailText;
    [SerializeField] private TMP_Text _genderText;
    [SerializeField] private TMP_Text _birthDayText;
    
    public void Initialize(User user)
    {
        _realNameText.text = user.RealName;
        _displayNameText.text = user.DisplayName;
        _ageText.text = user.Age;
        _emailText.text = user.Email;
        _genderText.text = user.Gender;
        _birthDayText.text = user.Birthday;
    }
}

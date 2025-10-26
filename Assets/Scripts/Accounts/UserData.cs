using System;
using System.Collections.Generic;
using Firebase.Firestore;

[FirestoreData]
[System.Serializable]
public class UserData
{
    [FirestoreProperty]
    public BasicInfo basicInfo { get; set; }

    [FirestoreProperty]
    public Economy economy { get; set; }

    [FirestoreProperty]
    public Profile profile { get; set; }

    [FirestoreProperty]
    public Auth auth { get; set; }

    [FirestoreProperty("roles")]
    public List<string> Roles { get; set; }

    [FirestoreProperty("web3Wallet")]
    public string Web3Wallet { get; set; }

    public UserData()
    {
        basicInfo = new BasicInfo();
        economy = new Economy();
        profile = new Profile();
        auth = new Auth();
        Roles = new List<string> { "user" };
    }

    public string AvatarPath { get => basicInfo?.avatarPath; set { if (basicInfo != null) basicInfo.avatarPath = value; } }
    public string DisplayName { get => basicInfo?.displayName; set { if (basicInfo != null) basicInfo.displayName = value; } }
    public string RealName { get => basicInfo?.realName; set { if (basicInfo != null) basicInfo.realName = value; } }
    public string Email { get => basicInfo?.email; set { if (basicInfo != null) basicInfo.email = value; } }
    public string Uid { get => basicInfo?.uid; set { if (basicInfo != null) basicInfo.uid = value; } }

    public int FitCoins { get => economy?.fitCoins ?? 0; set { if (economy != null) economy.fitCoins = value; } }
    public int PremiumCurrency { get => economy?.premiumCurrency ?? 0; set { if (economy != null) economy.premiumCurrency = value; } }
    public List<string> NftInventory { get => economy?.nftInventory; set { if (economy != null) economy.nftInventory = value; } }
    public List<string> UnlockedSkins { get => economy?.unlockedSkins; set { if (economy != null) economy.unlockedSkins = value; } }

    public int Age { get => profile?.age ?? 0; set { if (profile != null) profile.age = value; } }
    public string FitnessLevel { get => profile?.fitnessLevel; set { if (profile != null) profile.fitnessLevel = value; } }
    public string Gender { get => profile?.gender; set { if (profile != null) profile.gender = value; } }

}

[FirestoreData]
[System.Serializable]
public class BasicInfo
{
    [FirestoreProperty("avatarPath")]
    public string avatarPath { get; set; }

    [FirestoreProperty("createdAt")]
    public Timestamp createdAt { get; set; }

    [FirestoreProperty("displayName")]
    public string displayName { get; set; }

    [FirestoreProperty("email")]
    public string email { get; set; }

    [FirestoreProperty("realName")]
    public string realName { get; set; }

    [FirestoreProperty("uid")]
    public string uid { get; set; }

    [FirestoreProperty("updatedAt")]
    public Timestamp updatedAt { get; set; }

    public BasicInfo()
    {
        var now = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime());
        createdAt = now;
        updatedAt = now;
    }
}

[FirestoreData]
[System.Serializable]
public class Economy
{
    [FirestoreProperty("achievements")]
    public string achievements { get; set; }

    [FirestoreProperty("fitCoins")]
    public int fitCoins { get; set; }

    [FirestoreProperty("premiumCurrency")]
    public int premiumCurrency { get; set; }

    [FirestoreProperty("nftInventory")]
    public List<string> nftInventory { get; set; }

    [FirestoreProperty("unlockedSkins")]
    public List<string> unlockedSkins { get; set; }

    public Economy()
    {
        nftInventory = new List<string>();
        unlockedSkins = new List<string>();
    }
}

[FirestoreData]
[System.Serializable]
public class Profile
{
    [FirestoreProperty("age")]
    public int age { get; set; }

    [FirestoreProperty("birthDate")]
    public Timestamp birthDate { get; set; }

    [FirestoreProperty("fitnessLevel")]
    public string fitnessLevel { get; set; }

    [FirestoreProperty("gender")]
    public string gender { get; set; }

    public Profile()
    {
        fitnessLevel = "Basic";
    }
}

[FirestoreData]
[System.Serializable]
public class Auth
{
    [FirestoreProperty("authMethod")]
    public string authMethod { get; set; }

    [FirestoreProperty("isGuest")]
    public bool isGuest { get; set; }

    [FirestoreProperty("parentConsent")]
    public bool parentConsent { get; set; }

    [FirestoreProperty("parentEmail")]
    public string parentEmail { get; set; }

    public Auth()
    {
        authMethod = "email";
        isGuest = false;
        parentConsent = true;
    }
}
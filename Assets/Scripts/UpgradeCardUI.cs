using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCardUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Button selectButton;

    private UpgradeInformation assignedUpgrade;
    private UpgradeManager upgradeManager;
    public GameObject tier3tooltip;

    public void Setup(UpgradeInformation upgrade, UpgradeManager manager)
    {
        assignedUpgrade = upgrade;
        upgradeManager = manager;
        
        iconImage.sprite = upgrade.icon;
        titleText.text = upgrade.upgradeName;
        descriptionText.text = upgrade.description;

        if (upgrade.tier >= 3){
            tier3tooltip.SetActive(true);
        }

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() => OnClickChoice());
    }

    private void OnClickChoice()
    {
        AudioManager.instance.PlaySFX(SoundType.UpgradeGet);
        upgradeManager.SelectUpgrade(assignedUpgrade);
    }
}
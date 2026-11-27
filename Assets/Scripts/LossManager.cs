using UnityEngine;
using TMPro;
public class LossManager : MonoBehaviour
{
    [SerializeField] private Animator lossAnim;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private GameObject noButton;
    [SerializeField] private BattleManager bm;
    public void StartLoss()
    {
        noButton.SetActive(true);
        subtitle.text = "Would you like to try again?";
        lossAnim.ResetTrigger("Lose");
        lossAnim.SetTrigger("Lose");
    }
    public void Respond(int choice)
    {
        if(choice == 0)
        {
            noButton.SetActive(false);
            subtitle.text = "I wasn't really asking.";
        } else
        {
            bm.AcceptWin();
        }
    }
}

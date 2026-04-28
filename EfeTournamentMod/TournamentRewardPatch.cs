using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;
using System.Linq;
using TaleWorlds.CampaignSystem.TournamentGames; // Sınıfı bulabilmesi için gereken en önemli satır

namespace EfeTournamentMod
{
    
    [HarmonyPatch(typeof(TournamentCampaignBehavior), "OnTournamentFinished")]
    public class TournamentRewardPatch
    {
        // Orijinal metot işini bitirir bitirmez bu çalışacak
        public static void Postfix(CharacterObject winner, MBReadOnlyList<CharacterObject> participants)
        {
            
            if (winner != null && winner.IsPlayerCharacter)
            {
                
                int lordCount = participants.Count(p => p.IsHero && !p.IsPlayerCharacter);

                
                if (lordCount >= 3)
                {
                    
                    int bonusGold = lordCount * 1000;

                    
                    GiveGoldAction.ApplyBetweenCharacters(null, Hero.MainHero, bonusGold, false);

                    
                    InformationManager.DisplayMessage(new InformationMessage(
                        $"Zorlu arenada {lordCount} soyluyu alt ettin! Yan bahislerden {bonusGold} altin kazandin.",
                        Colors.Green));
                }
            }
        }
    }
}
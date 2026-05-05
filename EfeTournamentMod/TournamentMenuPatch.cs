using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus; 
using TaleWorlds.CampaignSystem.TournamentGames;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.MountAndBlade;

namespace EfeTournamentMod
{
    // Bütün mod boyunca diğer sınıfların da okuyabileceği global bir flag .
    public static class ModSettings
    {
        // Varsayılan olarak false (Yani geleneksel tahta silahlar)
        public static bool KendiEsyamlaGir = false;
    }

    [HarmonyPatch(typeof(TournamentCampaignBehavior), "game_menu_tournament_join_current_game_on_consequence")]
    public class TournamentMenuPatch
    {
        
        public static bool Prefix()
        {
            
            InquiryData inquiryData = new InquiryData(
                "Tournament Equipment", // Başlık
                "What equipment would you like to bring to the arena?",
                true,
                true, 
                "My Own Equipment", 
                "Tournament Equipments", 

                
                () =>
                {
                    ModSettings.KendiEsyamlaGir = true; 
                    GirmeyeDevamEt(); 
                },

                //(Negative Action)
                () =>
                {
                    ModSettings.KendiEsyamlaGir = false; 
                    GirmeyeDevamEt(); 
                }
            );

            
            InformationManager.ShowInquiry(inquiryData, false);

            
            return false;
        }

        
       
        private static void GirmeyeDevamEt()
        {
            TournamentGame tournamentGame = Campaign.Current.TournamentManager.GetTournamentGame(Settlement.CurrentSettlement.Town);
            GameMenu.SwitchToMenu("town");
            tournamentGame.PrepareForTournamentGame(true);
            Campaign.Current.TournamentManager.OnPlayerJoinTournament(tournamentGame.GetType(), Settlement.CurrentSettlement);
        }
    }

        
    



}
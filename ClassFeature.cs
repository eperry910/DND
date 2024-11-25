
namespace DND.Models
{
    public class ClassFeature
    { 
        public string FeatureName { get; set; }
        public string ActionType { get; set; } // e.g., "AddProficiency", "ChooseSpell", etc.
        public string Description { get; set; }
        public string ActionEconomy { get; set; } // e.g., "Action", "BonusAction", "Reaction", "Passive" etc.
        public string Resource { get; set; } // e.g., "Channel Divinoty Charges", "Sorcery Points", etc.
    }
}
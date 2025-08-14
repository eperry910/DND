using DND.Models;

namespace DND.Services
{
    public class SpellSlotService
    {
        public static List<SpellSlot> CalculateMulticlassSpellSlots(int fullCasterLevels, int halfCasterLevels)
        {
            var spellSlots = new List<SpellSlot>();
            int effectiveLevel = fullCasterLevels + (halfCasterLevels / 2);
            
            // Spell slot table for levels 1-20
            var slotTable = new Dictionary<int, Dictionary<int, int>>
            {
                { 1, new Dictionary<int, int> { { 1, 2 } } },
                { 2, new Dictionary<int, int> { { 1, 3 } } },
                { 3, new Dictionary<int, int> { { 1, 4 }, { 2, 2 } } },
                { 4, new Dictionary<int, int> { { 1, 4 }, { 2, 3 } } },
                { 5, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 2 } } },
                { 6, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 } } },
                { 7, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 1 } } },
                { 8, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 2 } } },
                { 9, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 1 } } },
                { 10, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 } } },
                { 11, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 } } },
                { 12, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 } } },
                { 13, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 } } },
                { 14, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 } } },
                { 15, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 } } },
                { 16, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 } } },
                { 17, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 } } },
                { 18, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 } } },
                { 19, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 } } },
                { 20, new Dictionary<int, int> { { 1, 4 }, { 2, 3 }, { 3, 3 }, { 4, 3 }, { 5, 2 }, { 6, 1 }, { 7, 1 }, { 8, 1 }, { 9, 1 } } }
            };
            
            if (slotTable.ContainsKey(effectiveLevel))
            {
                foreach (var slot in slotTable[effectiveLevel])
                {
                    spellSlots.Add(new SpellSlot
                    {
                        Id = Guid.NewGuid().ToString(),
                        Level = slot.Key,
                        Total = slot.Value,
                        Used = 0,
                        Source = "Multiclass"
                    });
                }
            }
            
            return spellSlots;
        }
        
        public static List<SpellSlot> CalculateWarlockSpellSlots(int warlockLevel)
        {
            var spellSlots = new List<SpellSlot>();
            
            // Warlock spell slot progression (corrected for levels 1-20)
            var warlockSlots = warlockLevel switch
            {
                1 => new Dictionary<int, int> { { 1, 1 } },
                2 => new Dictionary<int, int> { { 1, 2 } },
                3 => new Dictionary<int, int> { { 2, 2 } },
                4 => new Dictionary<int, int> { { 2, 2 } },
                5 => new Dictionary<int, int> { { 3, 2 } },
                6 => new Dictionary<int, int> { { 3, 2 } },
                7 => new Dictionary<int, int> { { 4, 2 } },
                8 => new Dictionary<int, int> { { 4, 2 } },
                9 => new Dictionary<int, int> { { 5, 2 } },
                10 => new Dictionary<int, int> { { 5, 2 } },
                11 => new Dictionary<int, int> { { 5, 3 } },
                12 => new Dictionary<int, int> { { 5, 3 } },
                13 => new Dictionary<int, int> { { 5, 3 } },
                14 => new Dictionary<int, int> { { 5, 3 } },
                15 => new Dictionary<int, int> { { 5, 3 } },
                16 => new Dictionary<int, int> { { 5, 3 } },
                17 => new Dictionary<int, int> { { 5, 4 } },
                18 => new Dictionary<int, int> { { 5, 4 } },
                19 => new Dictionary<int, int> { { 5, 4 } },
                20 => new Dictionary<int, int> { { 5, 4 } },
                _ => new Dictionary<int, int>()
            };
            
            foreach (var slot in warlockSlots)
            {
                spellSlots.Add(new SpellSlot
                {
                    Id = Guid.NewGuid().ToString(),
                    Level = slot.Key,
                    Total = slot.Value,
                    Used = 0,
                    Source = "Warlock"
                });
            }
            
            return spellSlots;
        }
    }
} 
using System.Collections.Generic;

namespace _questionnaire.Models.DB
{
    public class Setting
    {
        public const int  AMOUNT_OG_STAGES = 3;
        public long Id { get; set; }
        public int StagesCount { get; set; }
        public ICollection<LevelsForStage> LevelsForStages { get; set; }
    }

    public class LevelsForStage
    {
        public long Id { get; set; }
        public int StageNumber { get; set; }
        public long SettingId { get; set; }
        public int? Lvl1Count { get; set; }
        public int? Lvl2Count { get; set; }
        public int? Lvl3Count { get; set; }
        public int? Lvl4Count { get; set; }
        public Setting Setting { get; set; }
    }

}
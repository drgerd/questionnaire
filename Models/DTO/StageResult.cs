using System.Collections.Generic;
using _questionnaire.Models.DB;

namespace _questionnaire.Models.DTO
{
    public class StageResult
    {
        // public long Id { get; set; }
        public long UserId { get; set; }
        public int Stage1DoneCount { get; set; }
        public int Stage1Total { get; set; }
        public int Stage2DoneCount { get; set; }
        public int Stage2Total { get; set; }
        public int Stage3DoneCount { get; set; }
        public int Stage3Total { get; set; }
        public int CurrentStage{get;set;}
        public bool IsWin{get;set;}

        public User User { get; set; }
    }
}
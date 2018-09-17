namespace _questionnaire.Models
{
    public class CSVQuestionAnswerLine
    {
        public const int ANSWER_AMOUNT=8;

        public string Question { get; set; }
        public string Level { get; set; }
        public string RightAnswers { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string Answer5 { get; set; }
        public string Answer6 { get; set; }
        public string Answer7 { get; set; }
        public string Answer8 { get; set; }
    }
}
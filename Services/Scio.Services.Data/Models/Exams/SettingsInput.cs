namespace Scio.Services.Data.Models.Exams
{
    public class SettingsInput
    {
        public bool AcceptAfterClosing { get; set; }

        public bool AcceptExpiredTime { get; set; }

        public bool GenerateRandomVariant { get; set; }

        public int QuestionsCount { get; set; }

        public int AnswersPerQuestion { get; set; }
    }
}

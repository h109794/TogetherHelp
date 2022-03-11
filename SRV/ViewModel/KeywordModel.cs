namespace SRV.ViewModel
{
    public class KeywordModel
    {
        public string Text { get; set; }
        public int UseCount { get; set; }
        public KeywordModel Belong { get; set; }
    }
}

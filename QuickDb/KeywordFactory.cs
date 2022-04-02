using BLL.Entity;

namespace QuickDb
{
    static class KeywordFactory
    {
        internal static Keyword DemoKeywordOne { get; set; }
        internal static Keyword DemoKeywordTwo { get; set; }

        internal static void GenerateKeywords()
        {
            DemoKeywordOne = new Keyword { Text = "关键字一号" };
            DemoKeywordTwo = new Keyword { Text = "关键字二号" };
        }
    }
}

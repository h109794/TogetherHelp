using SRV.ViewModel;
using System.Collections.Generic;

namespace SRV.ServiceInterface
{
    public interface IKeywordService
    {
        List<KeywordModel> Select(int count, int? lastKeywordId = null);
        int? GetKeywordId(string keywordText);
    }
}

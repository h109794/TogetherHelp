using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System.Collections.Generic;

namespace SRV.ProductionService
{
    public class KeywordService : BaseService, IKeywordService
    {
        private readonly KeywordRepository keywordRepository;

        public KeywordService() => keywordRepository = new KeywordRepository(DbContext);

        public List<KeywordModel> Select(int count, int? lastKeywordId = null)
        {
            List<Keyword> keywords = keywordRepository.GetGroupByUseCount(count, lastKeywordId);
            return Mapper.Map<List<KeywordModel>>(keywords);
        }

        public int? GetKeywordId(string keywordText)
        {
            return keywordRepository.GetByText(keywordText)?.Id;
        }
    }
}

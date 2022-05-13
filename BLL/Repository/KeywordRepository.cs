using BLL.Entity;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Repository
{
    public class KeywordRepository : BaseRepository<Keyword>
    {
        public KeywordRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Keyword GetByText(string text)
        {
            return sqlDbContext.Keywords.Where(k => k.Text == text).SingleOrDefault();
        }

        public List<Keyword> GetGroupByUseCount(int count, int? lastKeywordId)
        {
            List<Keyword> keywords;
            var query = sqlDbContext.Keywords.OrderByDescending(k => k.UseCount);

            if (lastKeywordId is null)
            {
                keywords = query.Take(count).ToList();
            }
            else
            {
                keywords = query.ToArray().SkipWhile(k => k.Id != lastKeywordId)
                            .Skip(1).Take(count).ToList();

                if (keywords.Count < count)
                    keywords = keywords.Concat(query.Take(count - keywords.Count)).ToList();
            }
            return keywords;
        }
    }
}

using AutoMapper;
using BLL.Entity;
using BLL.Repository;
using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System.Web;

namespace SRV.ProductionService
{
    public class BaseService
    {
        // 较为消耗性能，只在静态构造器中创建一个实例
        protected static readonly MapperConfiguration config;

        static BaseService()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RegisterModel, User>().ReverseMap()
                    .ForMember(dest => dest.Inviter, opt => opt.Ignore())
                    .ForMember(dest => dest.InvitationCode, opt => opt.Ignore());
                cfg.CreateMap<User, LoginModel>();
                cfg.CreateMap<Article, ArticleModel>();
                cfg.CreateMap<Keyword, KeywordModel>();
            });
        }

        // 每个Action使用同一个DbContext
        protected SqlDbContext dbContext
        {
            get
            {
                if (HttpContext.Current.Items[Key.DbContext] is null)
                {
                    HttpContext.Current.Items.Add(Key.DbContext, new SqlDbContext());
                    ((SqlDbContext)HttpContext.Current.Items[Key.DbContext]).Database.BeginTransaction();
                }
                return (SqlDbContext)HttpContext.Current.Items[Key.DbContext];
            }
        }

        protected IMapper mapper { get { return config.CreateMapper(); } }
    }
}

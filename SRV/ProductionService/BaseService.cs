using AutoMapper;
using BLL.Entity;
using BLL.Repository;
using Global;
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
                cfg.CreateMap<RegisterModel, User>()
                    .ForMember(dest => dest.Inviter, opt => opt.Ignore())
                    .ForMember(dest => dest.InvitationCode, opt => opt.Ignore())
                    .ReverseMap();
                cfg.CreateMap<User, LoginModel>()
                    .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.PersonalData.Nickname));
                cfg.CreateMap<Article, ArticleModel>()
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.PersonalData.Nickname))
                    .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.PersonalData.Nickname))
                    .ReverseMap();
                cfg.CreateMap<Keyword, KeywordModel>();
                cfg.CreateMap<Comment, CommentModel>()
                    .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Author.PersonalData.Nickname))
                    .ForMember(dest => dest.ReplyUsername, opt => opt.MapFrom(src => src.ReplyUser.PersonalData.Nickname))
                    .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Body));
                cfg.CreateMap<PersonalData, PersonalDataModel>().ReverseMap();
            });
        }

        // 每个Action使用同一个DbContext
        protected SqlDbContext DbContext
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

        protected IMapper Mapper { get { return config.CreateMapper(); } }
    }
}

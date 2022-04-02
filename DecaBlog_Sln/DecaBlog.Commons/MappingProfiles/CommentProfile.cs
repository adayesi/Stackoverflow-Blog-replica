using AutoMapper;
using DecaBlog.Models;
using DecaBlog.Models.DTO;

namespace DecaBlog.Commons.MappingProfiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<UserComment, CommentDto>()
                .ForMember("CreatedAt", dest => dest.MapFrom(src => src.DateCreated))
                .ForMember("UpdatedAt", dest => dest.MapFrom(src => src.DateUpdated))
                .ForMember("CommentId", dest => dest.MapFrom(src => src.Id))
                .ForMember("Votes", dest => dest.MapFrom(src => src.Votes.Count))
                .ForMember("Commentator", dest => dest.MapFrom(src => src.User));
            CreateMap<User, Commentator>()
                .ForMember("FullName",
                    dest => dest.MapFrom(src => string.Join(src.FirstName, src.LastName)))
                .ForMember("CommentatorId", dest => dest.MapFrom(src => src.Id));

            CreateMap<UserComment, AddedCommentDto>()
                .ForMember(dest => dest.UserCommentId, opt => opt.MapFrom(u => u.Id));
            CreateMap<CommentToAddDto, UserComment>();
            CreateMap<CommentToReturnDto, UserComment>();

        }
    }
}
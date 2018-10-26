using System;
using AutoMapper;
using BSBingoApi.Model;

namespace BSBingoApi.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BSWordEntity, BSWord>()
                .ForMember(dest => dest.Self, opt =>
                           opt.MapFrom(src =>
                                       Link.To(nameof(Controllers.WordsController.GetWordById), new { wordId = src.Id })));
            //.ForMember(dest => dest.Create, opt => opt.MapFrom(src =>
            //FormMetadata.FromModel(
            //new AddWordForm(),
            //Link.ToForm(
            // nameof(Controllers.WordsController.Created),
            //null,
            //Link.PostMethod,
            //Form.CreateRelation))));


            CreateMap<UserEntity, User>()
             .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                 Link.To(nameof(Controllers.UsersController.GetUserById),
                 new { userId = src.Id })));
        }
    }
}

using AutoMapper;
using Mottrist.Domain.Entities;
using Mottrist.Service.Features.Messages.DTOs;

namespace Mottrist.Service.Features.Messages.Mappers
{
    /// <summary>
    /// AutoMapper profile for mapping message-related entities and DTOs.
    /// </summary>
    public class MessageProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageProfile"/> class.
        /// Configures mapping between <see cref="Message"/> and <see cref="MessageDto"/>.
        /// </summary>
        public MessageProfile()
        {
            /// <summary>
            /// Maps <see cref="Message"/> to <see cref="MessageDto"/>.
            /// Includes mapping for FullName and Email from the <see cref="ApplicationUser"/>.
            /// </summary>
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap();

            /// <summary>
            /// Maps <see cref="AddMessageDto"/> to <see cref="Message"/>.
            /// Automatically sets <see cref="CreatedAt"/> to the current date and time.
            /// </summary>
            CreateMap<AddMessageDto, Message>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            /// <summary>
            /// Maps <see cref="UpdateMessageDto"/> to <see cref="Message"/>.
            /// </summary>
            CreateMap<UpdateMessageDto, Message>();
        }
    }
}
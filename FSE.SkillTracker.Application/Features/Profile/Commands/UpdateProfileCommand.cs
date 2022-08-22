using AutoMapper;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Application.Interfaces.Messaging;
using FSE.SkillTracker.Domain.Exceptions;

namespace FSE.SkillTracker.Application.Features.Profile.Commands
{
    public class UpdateProfileCommand : ICommand<Domain.Entities.Profile>
    {
        public Guid UserId { get; set; }
        public int ExpertiseLevel { get; set; }
        public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, Domain.Entities.Profile>
        {
            private readonly IProfileRepository _profileRepository;
            private readonly IMapper _mapper;

            public UpdateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper)
            {
                _profileRepository = profileRepository;
                _mapper = mapper;
            }

            public async Task<Domain.Entities.Profile> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
            {
                var profile = await _profileRepository.GetItemAsync(request.UserId.ToString(), "Sudheer");
                if (profile == null)
                {
                    throw new UserNotFoundException(request.UserId);
                }

                await _profileRepository.UpdateItemAsync(profile);
                return profile;
            }
        }
    }
}

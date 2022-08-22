using AutoMapper;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Application.Interfaces.Messaging;
using FSE.SkillTracker.Domain.Entities;

namespace FSE.SkillTracker.Application.Features.Profile.Commands
{
    public class CreateProfileCommand : ICommand<Domain.Entities.Profile>
    {
        public string Name { get; set; }
        public string AssociateId { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public List<SkillExpertise> SkillExpertise { get; set; }
        public class CreateProfileCommandHandler : ICommandHandler<CreateProfileCommand, Domain.Entities.Profile>
        {
            private readonly IProfileRepository _profileRepository;
            private readonly IMapper _mapper;

            public CreateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper)
            {
                _profileRepository = profileRepository;
                _mapper = mapper;
            }

            public async Task<Domain.Entities.Profile> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
            {
                var LNewGuid = Guid.NewGuid();
                Domain.Entities.Profile newProfile = new Domain.Entities.Profile
                {
                    Id = LNewGuid,
                    AssociateId = request.AssociateId,
                    Email = request.Email,
                    Mobile = request.Mobile,
                    Name = request.Name,
                    SkillExpertise = request.SkillExpertise,
                    CreatedOn = DateTime.Now
                };

                await _profileRepository.AddItemAsync(newProfile);
                return newProfile;
            }
        }
    }
}

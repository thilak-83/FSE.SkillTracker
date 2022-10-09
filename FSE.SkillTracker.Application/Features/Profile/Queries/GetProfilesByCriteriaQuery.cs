using AutoMapper;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Application.Interfaces.Messaging;

namespace FSE.SkillTracker.Application.Features.Profile.Queries
{
    public class GetProfilesByCriteriaQuery : ICommand<List<Domain.Entities.Profile>>
    {
        public string Criteria { get; set; }
        public string CriteriaValue { get; set; }

        public class CreateProfileCommandHandler : ICommandHandler<GetProfilesByCriteriaQuery, List<Domain.Entities.Profile>>
        {
            private readonly IProfileRepository _profileRepository;
            private readonly IMapper _mapper;

            public CreateProfileCommandHandler(IProfileRepository profileRepository, IMapper mapper)
            {
                _profileRepository = profileRepository;
                _mapper = mapper;
            }

            public async Task<List<Domain.Entities.Profile>> Handle(GetProfilesByCriteriaQuery request, CancellationToken cancellationToken)
            {
                var profiles = await _profileRepository.GetItemsAsync(new Specifications.ProfileSpecification(request));
                return profiles.ToList();
            }
        }
    }
}

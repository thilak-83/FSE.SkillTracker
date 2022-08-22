using MediatR;

namespace FSE.SkillTracker.Application.Interfaces.Messaging
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}

namespace Nexus.Abstractions;

public interface ICommandHandler<in TCommand, TResult> where TCommand : IRequest<TResult>
{
    public Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}
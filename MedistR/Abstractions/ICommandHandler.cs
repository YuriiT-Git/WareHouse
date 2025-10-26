namespace MedistR.Abstractions;

public interface ICommandHandler<in TRequest> where TRequest : IRequest<TRequest>
{
    public Task Handle(TRequest command, CancellationToken cancellationToken);
}


public interface ICommandHandler<in TRequest, TResult> where TRequest : IRequest<TRequest, TResult>
{
    public Task<TResult> Handle(TRequest command, CancellationToken cancellationToken);
}
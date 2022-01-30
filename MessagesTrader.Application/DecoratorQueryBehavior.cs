using MTrading;

namespace MessagesTrader.Application;

public class DecoratorQueryBehavior<TQuery, TResult> : IQueryPipelineBehavior<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public async Task<TResult> Handle(TQuery query, QueryPipelineHandler<TResult> nextHandler)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentNullException.ThrowIfNull(nextHandler);

        Console.WriteLine("Before decorated query");

        var result = await nextHandler().ConfigureAwait(false);

        Console.WriteLine("After decorated query");

        return result;
    }
}
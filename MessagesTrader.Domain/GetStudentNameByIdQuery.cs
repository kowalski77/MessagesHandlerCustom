using MTrading;

namespace MessagesTrader.Domain;

public record GetStudentNameByIdQuery(Guid Id) : IQuery<string>;
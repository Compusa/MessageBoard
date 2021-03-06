﻿using MediatR;
using MessageBoard.Domain;
using MessageBoard.Domain.AggregateModels.MessageAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBoard.Application.Messages.Queries
{
    public class GetMessageQueryHandler : IRequestHandler<GetMessageQuery, MessageDto>
    {
        private readonly IReadOnlyMessageBoardContext _messageBoardContext;

        public GetMessageQueryHandler(IReadOnlyMessageBoardContext messageBoardContext)
        {
            _messageBoardContext = messageBoardContext;
        }

        public async Task<MessageDto> Handle(GetMessageQuery request, CancellationToken cancellationToken)
        {
            var message = await _messageBoardContext.FindAsync<BoardMessage>(request.Id);

            return message == null ? null : MessageDto.Create(message);
        }
    }
}

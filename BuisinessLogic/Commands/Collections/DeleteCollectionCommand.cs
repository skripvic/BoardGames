using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Collections
{
    public class DeleteCollectionCommand : IRequest
    {
        public int Id { get; init; }

        public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUser _currentUser;

            public DeleteCollectionCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }

            public async Task Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
            {
                var collection = await _context.Collections.FindAsync(request.Id);

                if (collection == null)
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                var user = await _context.Users.FindAsync(_currentUser.Id, cancellationToken);

                if ( collection.User != user)
                {
                    throw new UnauthorizedAccessException("Коллекция не принадлежит текущему пользователю");
                }

                _context.Collections.Remove(collection);

                await _context.SaveChangesAsync();
            }
        }
    }
}

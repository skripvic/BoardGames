using BuisinessLogic.Auth.CurrentUser;
using BuisinessLogic.Commands.Collections.Validation;
using BuisinessLogic.Exceptions;
using DataAccess;
using MediatR;

namespace BuisinessLogic.Commands.Collections
{
    public class UpdateCollectionCommand : IRequest, ICollectionCommand
    {
        public int Id { get; init; }

        public string Name { get; init; } = string.Empty;

        public class UpdateCollectionCommandHandler : IRequestHandler<UpdateCollectionCommand>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUser _currentUser;
            private readonly CollectionCommandValidator _validator;

            public UpdateCollectionCommandHandler(IApplicationDbContext context,
                ICurrentUser currentUser, CollectionCommandValidator validator)
            {
                _context = context;
                _currentUser = currentUser;
                _validator = validator;
            }

            public async Task Handle(UpdateCollectionCommand request, CancellationToken cancellationToken)
            {
                var collection = await _context.Collections.FindAsync(request.Id);

                if (collection == null) 
                {
                    throw new EntityNotFoundException("Коллекция не найдена");
                }

                _validator.ValidateOrThrow(request);

                var user = await _context.Users.FindAsync(_currentUser.Id, cancellationToken);

                if (collection.User != user)
                {
                    throw new UnauthorizedAccessException("Коллекция не принадлежит текущему пользователю");
                }

                collection.UpdateCollection(request.Name);

                await _context.SaveChangesAsync();
            }
        }
    }
}

using BuisinessLogic.Commands.Games.Validation;
using BuisinessLogic.Dto.Games;
using DataAccess;
using MediatR;
using System.IO;

namespace BuisinessLogic.Commands.Games
{
    public class AddGamePicture : IRequest<Unit>
    {
        public string alias { get; init; }

        public IFormFile GamePicture { get; init; }

        public AddGamePicture(string alias)
        {
            this.alias = alias;
        }

        public class AddGamePictureHandler : IRequestHandler<AddGamePicture, Unit>
        {
            private readonly IWebHostEnvironment _appEnvironment;

            public AddGamePictureHandler(IWebHostEnvironment appEnvironment)
            {
                _appEnvironment = appEnvironment;
            }

            public async Task<Unit> Handle(AddGamePicture request, CancellationToken cancellationToken)
            {

                var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "GamePictures");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, request.alias);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.GamePicture.CopyToAsync(fileStream, cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}

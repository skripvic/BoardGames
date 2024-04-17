using MediatR;

namespace BuisinessLogic.Commands.Games
{
    public class AddGamePicture : IRequest<Unit>
    {
        public string Alias { get; init; }

        public IFormFile GamePicture { get; init; }

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

                var filePath = Path.Combine(uploadsFolder, request.Alias);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await request.GamePicture.CopyToAsync(fileStream, cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}

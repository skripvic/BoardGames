using BuisinessLogic.Dto.Games;
using DataAccess;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuisinessLogic.Queries.Games
{
    public class GetGamePicture : IRequest<FileStreamResult>
    {
        public string FileName { get; init; }

        public GetGamePicture(string fileName) 
        {
            FileName = fileName;
        }

        public class GetGamePictureHandler : IRequestHandler<GetGamePicture, FileStreamResult>
        {
            private readonly IApplicationDbContext _context;
            private readonly IWebHostEnvironment _appEnvironment;
            public GetGamePictureHandler(IApplicationDbContext context, IWebHostEnvironment appEnvironment)
            {
                _context = context;
                _appEnvironment = appEnvironment;
            }

            public async Task<FileStreamResult> Handle(GetGamePicture request, CancellationToken cancellationToken)
            {
                var filePath = Path.Combine(_appEnvironment.WebRootPath, request.FileName);

                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found", filePath);
                }

                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                return new FileStreamResult(fileStream, "application/octet-stream");

            }
        }
    }
}

using CaseStudyAppServer.Dtos.Figures;
using CaseStudyAppServer.Models;

namespace CaseStudyAppServer.Mappers
{
    public static class FigureMappers
    {
        public static Figure ToFigureFromRequestDto(this FigureRequestDto figureRequestDto)
        {
            return new Figure
            {
                Name = figureRequestDto.Name,
                Description = figureRequestDto.Description,
                UploadId = figureRequestDto.UploadId,
            };
        }

        public static FigureResponseDto ToResponseDto(this Figure figure)
        {
            return new FigureResponseDto
            {
                Id = figure.Id,
                Name = figure.Name,
                Description = figure.Description,
                Upload = figure.Upload,
                CreatedOn = figure.CreatedOn,
                UpdatedOn = figure.UpdatedOn,
                DeletedOn = figure.DeletedOn,
            };
        }
    }
}
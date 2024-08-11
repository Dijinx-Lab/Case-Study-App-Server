using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CaseStudyAppServer.Constants;
using CaseStudyAppServer.Helpers;
using CaseStudyAppServer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaseStudyAppServer.Controllers
{
    [ApiController]
    [Route("api/v1/admin/upload")]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] string folder)
        {
            if (file == null || file.Length == 0)
                return ResponseUtility.ReturnBadRequest(MessageConstants.FileIsRequired);
            const long maxFileSize = 10 * 1024 * 1024; // 10MB
            if (file.Length > maxFileSize)
                return ResponseUtility.ReturnBadRequest(MessageConstants.FileSizeLimit);

            try
            {

                var uploadedItem = await _uploadService.CreateUploadAsync(file, folder);
                if (uploadedItem == null) return ResponseUtility.ReturnServerError(null);

                return ResponseUtility.ReturnOk(new { upload = uploadedItem });
            }
            catch (Exception e)
            {
                return ResponseUtility.ReturnServerError(e.ToString());
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var upload = await _uploadService.DeleteUploadAsync(id);

            if (upload == null) return ResponseUtility.ReturnOk(null, MessageConstants.ItemNotFound);

            return ResponseUtility.ReturnOk(null, MessageConstants.ItemDeleted);
        }
    }
}
using EducationApp.Application.Auth;
using EducationApp.Application.Repositories.FileRepository;
using EducationApp.Application.Service.FileStorageServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController(IFileStorageService fileStorageService, IFileRepository repo)
    : ControllerBase
{
    
    [HttpPost("upload")]
	[Consumes("multipart/form-data")]
	[PermissionAuthorize(Core.Permission.UploadFilePermission)]
	public async Task<IActionResult> UploadFile(IFormFile file, [FromQuery] string bucketName = "my-test-bucket")
	{
		if (file == null || file.Length == 0)
		{
			return BadRequest("Fayl tanlanmagan yoki bo'sh.");
		}

		var fileExtension = Path.GetExtension(file.FileName);
		var objectName = $"{Guid.NewGuid()}{fileExtension}"; 

		using (var stream = file.OpenReadStream()) 
		{
			var fileUrl = await fileStorageService.UploadFileAsync(bucketName, objectName, stream, file.ContentType);
			var fileOy = Path.GetFileName(fileUrl);
			var fileBek = new Core.Entities.File
			{
				Name = bucketName,
				Url = fileOy
			};
			await repo.AddAsync(fileBek);
			await repo.SaveChangesAsync();
			return Ok(new { Message = "Fayl muvaffaqiyatli yuklandi.", FileUrl = fileUrl });
		}
	}

	[HttpGet("download")]
    [PermissionAuthorize(Core.Permission.DownloadFilePermission)]

    public async Task<IActionResult> DownloadFile([FromQuery] string bucketName, [FromQuery] string objectName)
	{
		if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(objectName))
		{
			return BadRequest("Bucket nomi va obyekt nomi talab qilinadi.");
		}

		try
		{
			var stream = await fileStorageService.DownloadFileAsync(bucketName, objectName);

			var contentType = "application/octet-stream"; 
			return File(stream, contentType, objectName); 
		}
		catch (Minio.Exceptions.MinioException e) when (e.Message.Contains("Object does not exist"))
		{
			return NotFound("Fayl topilmadi.");
		}
		catch (Exception)
		{
			return StatusCode(500, "Faylni yuklab olishda kutilmagan xatolik yuz berdi.");
		}
	}

	[HttpDelete("delete")]
    [PermissionAuthorize(Core.Permission.DeleteFilePermission)]
    public async Task<IActionResult> DeleteFile([FromQuery] string bucketName, [FromQuery] string objectName)
	{
		if (string.IsNullOrEmpty(bucketName) || string.IsNullOrEmpty(objectName))
		{
			return BadRequest("Bucket nomi va obyekt nomi talab qilinadi.");
		}

		try
		{
			bool removed = await fileStorageService.RemoveFileAsync(bucketName, objectName);
			if (removed)
			{
				return Ok("Fayl muvaffaqiyatli o'chirildi.");
			}
			return NotFound("Fayl topilmadi yoki o'chirishda muammo yuz berdi.");
		}
		catch (Exception)
		{
			return StatusCode(500, "Faylni o'chirishda kutilmagan xatolik yuz berdi.");
		}
	}

	[HttpGet("get-all-files")]
	public async Task<IActionResult> GetAllFiles()
	{
		var files = await repo.GetAll().ToListAsync();
		return Ok(files);
	}
}

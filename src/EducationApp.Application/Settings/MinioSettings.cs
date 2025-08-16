namespace EducationApp.Application.Settings;

public class MinioSettings
{
	public string Endpoint { get; set; } = null;
	public string AccessKey { get; set; } = null;
	public string SecretKey { get; set; } = null;
	public bool UseSSL { get; set; }
}


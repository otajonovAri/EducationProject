﻿namespace EducationApp.Application.Common;

public class EmailConfiguration
{
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string DefaultFromEmail { get; set; }
    public string DefaultFromName { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
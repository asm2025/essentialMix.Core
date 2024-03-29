﻿using System.Threading.Tasks;
using JetBrains.Annotations;
using MSIEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;

namespace essentialMix.Core.Mail;

public interface IEmailSender : MSIEmailSender
{
	[NotNull]
	Task SendEmailAsync([NotNull] BasicEmail email);
}
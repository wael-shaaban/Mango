﻿using Mango.Services.EmailApi.Data;
using Mango.Services.EmailApi.Dtos;
using Mango.Services.EmailApi.Models;
using Mango.Services.EmailApi.Services.IServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Mango.Services.EmailApi.Services
{
    public class EmailService : IEmailService
    {
        private DbContextOptions<AppDbContext> options;

        public EmailService(DbContextOptions<AppDbContext> options)
        {
            this.options = options;
        }


        public async Task EmailCartAndLog(CartDto cartDto)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine("<br/>Cart Email Requested ");
            message.AppendLine("<br/>Total " + cartDto?.CartHeader?.CartTotal);
            message.Append("<br/>");
            message.Append("<ul>");
            foreach (var item in cartDto?.CartDetails)
            {
                message.Append("<li>");
                message.Append(item?.Product?.Name + " x " + item?.Count);
                message.Append("</li>");
            }
            message.Append("</ul>");
            await LogAndEmail(message.ToString(), cartDto?.CartHeader?.Email);
        }
        private async Task<bool> LogAndEmail(string? message, string? email)
        {
            try
            {
                EmailLogger emailLog = new()
                {
                    Email = email,
                    EmailSent = DateTime.Now,
                    Message = message
                };
                await using var _db = new AppDbContext(options);
                await _db.Emails.AddAsync(emailLog);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
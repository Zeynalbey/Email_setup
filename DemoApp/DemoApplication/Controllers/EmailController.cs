using DemoApplication.Contracts.Email;
using DemoApplication.Database;
using DemoApplication.Database.NotificationModel;
using DemoApplication.Options;
using DemoApplication.Services.Abstracts;
using DemoEmailApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace DemoEmailApp.Controllers
{
    public class EmailController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IEmailService _emailService;
        private readonly EmailConfigOptions _emailConfig;
        public EmailController(DataContext dataContext, IEmailService emailService, EmailConfigOptions emailConfig)
        {
            _dataContext = dataContext;
            _emailService = emailService;
            _emailConfig = emailConfig;
        }

        public IActionResult List()
        {
            var model = _dataContext.Notifications
                .Select(e => new ListViewModel(e.From, $"{e.TargetEmail.TargetEmail}", e.Tittle, e.Message))
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add([FromForm] AddViewModel model)
        {
            var not = new Notification
            {
                From = _emailConfig.From,
                TargetEmail = model.TargetEmail,
                Tittle = model.Tittle,
                Message = model.Message,
            };

            List<string> emails = new List<string>();

            if (model.Status == false)
            {
                var newemails = not.TargetEmail.TargetEmail.Split(',');

                foreach (var e in newemails)
                {
                    emails.Add(e);
                }
            }
            else
            {
                emails.Add(model.TargetEmail.TargetEmail);
            }

            var message = new MessageDto(emails, not.Tittle, not.Message);

            _dataContext.Notifications.Add(not);
            _emailService.Send(message);
            _dataContext.Emails.Add(not.TargetEmail);
            _dataContext.SaveChanges();

            return RedirectToAction(nameof(List));
        }
    }
}

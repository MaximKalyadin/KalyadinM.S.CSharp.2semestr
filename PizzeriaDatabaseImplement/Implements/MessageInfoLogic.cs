using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaDatabaseImplement.Models;

namespace PizzeriaDatabaseImplement.Implements
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        public void Create(MessageInfoBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                MessageInfo element = context.MessageInfos.FirstOrDefault(rec => rec.MessageId == model.MessageId);
                if (element != null)
                {
                    throw new Exception("Уже есть письмо с таким идентификатором");
                }
                int? clientId = context.Clients.FirstOrDefault(rec => rec.Login == model.FromMailAddress)?.Id;
                context.MessageInfos.Add(new MessageInfo
                {
                    MessageId = model.MessageId,
                    ClientId = clientId,
                    SenderName = model.FromMailAddress,
                    DateDelivery = model.DateDelivery,
                    Subject = model.Subject,
                    Body = model.Body
                });
                context.SaveChanges();
            }
        }
        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            using (var context = new PizzeriaDatabase())
            {
                return context.MessageInfos
                .Where(rec => model == null || (model.ClientId.HasValue && rec.ClientId == model.ClientId))
                .Select(rec => new MessageInfoViewModel
                {
                    MessageId = rec.MessageId,
                    SenderName = rec.SenderName,
                    DateDelivery = rec.DateDelivery,
                    Subject = rec.Subject,
                    Body = rec.Body
                })
                .ToList();
            }
        }
    }
}

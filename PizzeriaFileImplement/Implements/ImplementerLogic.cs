using System;
using System.Collections.Generic;
using System.Text;
using PizzeriaBusinessLogic.BindingModels;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaFileImplement.Models;
using System.Linq;

namespace PizzeriaFileImplement.Implements
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly FileDataListSingleton source;

        public ImplementerLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            Implementer tmp = source.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO && rec.Id != model.Id);
            if (tmp != null)
            {
                throw new Exception("Уже есть такой рабочий");
            }
            if (model.Id.HasValue)
            {
                tmp = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (tmp == null)
                {
                    throw new Exception("Рабочий не найден");
                }
            }
            else
            {
                int maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
                tmp = new Implementer { Id = maxId + 1 };
                source.Implementers.Add(tmp);
            }
            tmp.ImplementerFIO = model.ImplementerFIO;
            tmp.PauseTime = model.PauseTime;
            tmp.WorkTime = model.WorkingTime;
        }

        public void Delete(ImplementerBindingModel model)
        {
            Implementer implementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer != null)
            {
                source.Implementers.Remove(implementer);
            }
            else
            {
                throw new Exception("Рабочий не найден");
            }
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            return source.Implementers
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new ImplementerViewModel
            {
                Id = rec.Id,
                ImplementerFIO = rec.ImplementerFIO,
                WorkingTime = rec.WorkTime,
                PauseTime = rec.PauseTime
            })
            .ToList();
        }
    }
}

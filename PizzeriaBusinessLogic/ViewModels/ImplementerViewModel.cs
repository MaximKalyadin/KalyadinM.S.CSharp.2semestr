﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using PizzeriaBusinessLogic.Attributes;

namespace PizzeriaBusinessLogic.ViewModels
{
    public class ImplementerViewModel : BaseViewModel
    {
        [Column(title: "ФИО Исполнителя", width: 150)]
        public string ImplementerFIO { get; set; }
        [Column(title: "Время на заказ", width: 100)]
        public int WorkingTime { get; set; }
        [Column(title: "Время на перерыв", width: 100)]
        public int PauseTime { get; set; }
        public override List<string> Properties() => new List<string> { "Id", "ImplementerFIO", "WorkingTime", "PauseTime" };
    }
}

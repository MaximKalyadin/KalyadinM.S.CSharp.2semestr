using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzeriaBusinessLogic.ViewModels;
using PizzeriaBusinessLogic.Interfaces;
using PizzeriaBusinessLogic.BindingModels;

namespace PizzeriaRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SkladController : ControllerBase
    {
        private readonly ISkladLogic skladLogic;

        public SkladController(ISkladLogic skladLogic)
        {
            this.skladLogic = skladLogic;
        }

        [HttpPost]
        public void CreateOrUpdateSklad(SkladBindingModel model) => skladLogic.CreateOrUpdate(model);

        [HttpPost]
        public void AddIngredientToSklad(AddIngredientBindingModels model) => skladLogic.AddIngredientToSklad(model);

        [HttpPost]
        public void DeleteSklad(SkladBindingModel model) => skladLogic.Delete(model);

        [HttpGet]
        public List<SkladViewModel> GetSklads() => skladLogic.Read(null);
    }
}

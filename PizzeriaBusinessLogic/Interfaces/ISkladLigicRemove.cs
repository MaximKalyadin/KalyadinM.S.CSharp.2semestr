using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaBusinessLogic.Interfaces
{
    public interface ISkladLigicRemove : ISkladLogic
    {
        void RemoveIngredients(int PizzaId, int Count);
    }
}

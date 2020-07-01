﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzeriyListImplement.Models;

namespace PizzeriyListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizza { get; set; }
        public List<PizzaIngredient> PizzaIngredients { get; set; }
        public List<Sklad> Sklads { get; set; }
        public List<SkladIngredients> SkladIngredients { get; set; }
        public List<Client> Clients { set; get; }
        private DataListSingleton()
        {
            Ingredients = new List<Ingredient>();
            Orders = new List<Order>();
            Pizza = new List<Pizza>();
            PizzaIngredients = new List<PizzaIngredient>();
            Sklads = new List<Sklad>();
            SkladIngredients = new List<SkladIngredients>();
            Clients = new List<Client>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PizzeriaDatabaseImplement.Models;

namespace PizzeriaDatabaseImplement
{
    public class PizzeriaDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-GDNE8QD\MYSQLDATABASE;Initial Catalog=PizzeriaDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Ingredient> Ingredients { set; get; }
        public virtual DbSet<Pizza> Pizzas { set; get; }
        public virtual DbSet<PizzaIngredient> PizzaIngredients { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Implementer> Implementers { set; get; }
        public virtual DbSet<MessageInfo> MessageInfos { set; get; }
    }
}

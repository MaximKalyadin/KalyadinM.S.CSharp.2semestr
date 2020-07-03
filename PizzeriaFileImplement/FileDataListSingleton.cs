using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using PizzeriaBusinessLogic.Enums;
using PizzeriaFileImplement.Models;
using System.IO;

namespace PizzeriaFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string IngredientFileName = "Ingredient.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string PizzaFileName = "Pizza.xml";
        private readonly string PizzaIngredientFileName = "PizzaIngredient.xml";
        private readonly string SkladFileName = "Sklad.xml";
        private readonly string SkladIngredientFileName = "SkladIngredient.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";
        public List<Ingredient> Ingredients { get; set; }
        public List<Order> Orders { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public List<PizzaIngredient> PizzaIngredients { get; set; }
        public List<Client> Clients { set; get; }
        public List<Sklad> Sklads { set; get; }
        public List<SkladIngredient> SkladIngredients { set; get; }
        public List<Implementer> Implementers { set; get; }
        public List<MessageInfo> MessageInfoes { get; set; }
        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Pizzas = LoadPizza();
            PizzaIngredients = LoadPizzaIngredients();
            Sklads = LoadSklads();
            SkladIngredients = LoadSkladIngredients();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessageInfoes = LoadMessageInfoes();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveIngredients();
            SaveOrders();
            SavePizza();
            SavePizzaIngredients();
            SaveClients();
            SaveImplementers();
            SaveMessageInfoes();
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementor").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value
                    });
                }
            }
            return list;
        }
        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();
            if (File.Exists(IngredientFileName))
            {
                XDocument xDocument = XDocument.Load(IngredientFileName);
                var xElements = xDocument.Root.Elements("Ingredient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Ingredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngredientName = elem.Element("IngredientName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        TimeCreate = Convert.ToDateTime(elem.Element("TimeCreate").Value),
                        TimeImplement = string.IsNullOrEmpty(elem.Element("TimeImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("TimeImplement").Value),
                    });
                }
            }
            return list;
        }
        private List<Pizza> LoadPizza()
        {
            var list = new List<Pizza>();
            if (File.Exists(PizzaFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaFileName);
                var xElements = xDocument.Root.Elements("Pizza").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Pizza
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaName = elem.Element("PizzaName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<PizzaIngredient> LoadPizzaIngredients()
        {
            var list = new List<PizzaIngredient>();
            if (File.Exists(PizzaIngredientFileName))
            {
                XDocument xDocument = XDocument.Load(PizzaIngredientFileName);
                var xElements = xDocument.Root.Elements("PizzaIngredient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new PizzaIngredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                        IngredientId = Convert.ToInt32(elem.Element("IngredientId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<Sklad> LoadSklads()
        {
            var list = new List<Sklad>();
            if (File.Exists(SkladFileName))
            {
                XDocument xDocument = XDocument.Load(SkladFileName);
                var xElements = xDocument.Root.Elements("Sklad").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Sklad()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        SkladName = elem.Element("SkladName").Value.ToString()
                    });
                }
            }
            return list;
        }
        private List<SkladIngredient> LoadSkladIngredients()
        {
            var list = new List<SkladIngredient>();
            if (File.Exists(SkladIngredientFileName))
            {
                XDocument xDocument = XDocument.Load(SkladIngredientFileName);
                var xElements = xDocument.Root.Elements("SkladIngredient").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new SkladIngredient()
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngredientId = Convert.ToInt32(elem.Element("IngredientId").Value),
                        SkladId = Convert.ToInt32(elem.Element("SkladId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<MessageInfo> LoadMessageInfoes()
        {
            var list = new List<MessageInfo>();

            if (File.Exists(MessageInfoFileName))
            {
                XDocument xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("MessageInfo").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value
                    });
                }
            }

            return list;
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Login", client.Login),
                    new XElement("Password", client.Password)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }
        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");
                foreach (var component in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                    new XAttribute("Id", component.Id),
                    new XElement("IngredientName", component.IngredientName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(IngredientFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("PizzaId", order.PizzaId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("TimeCreate", order.TimeCreate),
                    new XElement("TimeImplement", order.TimeImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SavePizza()
        {
            if (Pizzas != null)
            {
                var xElement = new XElement("Pizzas");
                foreach (var product in Pizzas)
                {
                    xElement.Add(new XElement("Pizza",
                    new XAttribute("Id", product.Id),
                    new XElement("PizzaName", product.PizzaName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PizzaFileName);
            }
        }
        private void SavePizzaIngredients()
        {
            if (PizzaIngredients != null)
            {
                var xElement = new XElement("PizzaIngredients");
                foreach (var productComponent in PizzaIngredients)
                {
                    xElement.Add(new XElement("PizzaIngredient",
                    new XAttribute("Id", productComponent.Id),
                    new XElement("PizzaId", productComponent.PizzaId),
                    new XElement("IngredientId", productComponent.IngredientId),
                    new XElement("Count", productComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(PizzaIngredientFileName);
            }
        }
        private void SaveSklads()
        {
            if (Sklads != null)
            {
                var xElement = new XElement("Sklads");
                foreach (var elem in Sklads)
                {
                    xElement.Add(new XElement("Sklad",
                        new XAttribute("Id", elem.Id),
                        new XElement("SkladName", elem.SkladName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SkladFileName);
            }
        }
        private void SaveSkladIngredients()
        {
            if (SkladIngredients != null)
            {
                var xElement = new XElement("SkladIngredients");
                foreach (var elem in SkladIngredients)
                {
                    xElement.Add(new XElement("SkladIngredient",
                        new XAttribute("Id", elem.Id),
                        new XElement("IngredientId", elem.IngredientId),
                        new XElement("SkladId", elem.SkladId),
                        new XElement("Count", elem.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(SkladIngredientFileName);
            }
        }

        private void SaveMessageInfoes()
        {
            if (MessageInfoes != null)
            {
                var xElement = new XElement("MessageInfoes");

                foreach (var messageInfo in MessageInfoes)
                {
                    xElement.Add(new XElement("MessageInfo",
                    new XAttribute("Id", messageInfo.MessageId),
                    new XElement("ClientId", messageInfo.ClientId),
                    new XElement("SenderName", messageInfo.SenderName),
                    new XElement("DateDelivery", messageInfo.DateDelivery),
                    new XElement("Subject", messageInfo.Subject),
                    new XElement("Body", messageInfo.Body)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MessageInfoFileName);
            }
        }
    }
}

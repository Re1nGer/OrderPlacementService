using NUnit.Framework;
using OrderPlacementService.Interfaces;
using OrderPlacementService.Models;
using OrderPlacementService.Service;
using System;
using System.Collections.Generic;

namespace OrderPlacementTest
{
    [TestFixture]
    public class OrderPlacementServiceTests
    {
        private IOrderPlacement OrderPlacementService;
        private List<Order> Orders;

        [SetUp]
        public void Setup()
        {
            //Arrange part could have been encapsulated with separate methods 
            Orders = new List<Order>();
            OrderPlacementService = new OrderPlacement(Orders);
        }

        [Test]
        public void PlaceOrderWith5Items()
        {
            int customerId = 1;
            int desiredAmount = 10;
            Guid kitId = Guid.NewGuid();
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            DNAKit kit = new(kitId, 98.99);

            OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);

            Order order = Orders[0];

            Assert.AreEqual(1, Orders.Count);
            Assert.AreEqual(customerId, order.CustomerId);
            Assert.AreEqual(desiredAmount, order.DesiredAmount);
            Assert.AreEqual(desiredAmount * kit.BasePrice * (1 - 0.05), order.TotalPrice);
            Assert.AreEqual(deliveryDate, order.DeliveryDate);
            Assert.AreEqual(kit, order.Kit);
        }

        [Test]
        public void PlaceOrderWith10ItemsAndDifferentBasePrice()
        {
            int customerId = 1;
            int desiredAmount = 10;
            Guid kitId = Guid.NewGuid();
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            DNAKit kit = new(kitId, 45);

            OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);

            Order order = Orders[0];
            Assert.AreEqual(1, Orders.Count);
            Assert.AreEqual(customerId, order.CustomerId);
            Assert.AreEqual(desiredAmount, order.DesiredAmount);
            Assert.AreEqual(desiredAmount * kit.BasePrice * (1 - 0.05), order.TotalPrice);
            Assert.AreEqual(deliveryDate, order.DeliveryDate);
            Assert.AreEqual(kit, order.Kit);
        }

        [Test]
        public void PlaceOrderWithNoDiscount()
        {
            int customerId = 1;
            int desiredAmount = 4;
            Guid kitId = Guid.NewGuid();
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            DNAKit kit = new(kitId, 45);

            OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);

            Order order = Orders[0];

            Assert.AreEqual(1, Orders.Count);
            Assert.AreEqual(customerId, order.CustomerId);
            Assert.AreEqual(desiredAmount, order.DesiredAmount);
            Assert.AreEqual(desiredAmount * kit.BasePrice, order.TotalPrice);
            Assert.AreEqual(deliveryDate, order.DeliveryDate);
            Assert.AreEqual(kit, order.Kit);
        }

        [Test]
        public void PlaceOrderWith999Items()
        {
            // Arrange
            int customerId = 1;
            int desiredAmount = 999;
            Guid kitId = Guid.NewGuid();
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            DNAKit kit = new(kitId, 98.99);

            // Act
            OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            Order order = Orders[0];

            // Assert
            Assert.AreEqual(1, Orders.Count);
            Assert.AreEqual(customerId, order.CustomerId);
            Assert.AreEqual(desiredAmount, order.DesiredAmount);
            Assert.AreEqual(desiredAmount * kit.BasePrice * (1 - 0.15), order.TotalPrice);
            Assert.AreEqual(deliveryDate, order.DeliveryDate);
            Assert.AreEqual(kit, order.Kit);
        }


        [Test]
        public void PlaceOrderWithInvalidDesiredAmount()
        {
            int customerId = 1;
            int desiredAmount = 1000;
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            Guid kitId = Guid.NewGuid();
            DNAKit kit = new(kitId, 98.99);

            Assert.Throws<ArgumentException>(() =>
            {
                OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            });
        }

        [Test]
        public void PlaceOrderWithInvalidNegativeDesiredAmount()
        {
            int customerId = 1;
            int desiredAmount = -4;
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            Guid kitId = Guid.NewGuid();
            DNAKit kit = new(kitId, 98.99);

            Assert.Throws<ArgumentException>(() =>
            {
                OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            });
        }

        [Test]
        public void PlaceOrderWithInvalidZeroDesiredAmount()
        {
            int customerId = 1;
            int desiredAmount = 0;
            DateTime deliveryDate = DateTime.Now.AddDays(7);
            Guid kitId = Guid.NewGuid();
            DNAKit kit = new(kitId, 98.99);

            Assert.Throws<ArgumentException>(() =>
            {
                OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            });
        }

        [Test]
        public void PlaceOrderWithInvalidDeliveryDateNow()
        {
            int customerId = 1;
            int desiredAmount = 10;
            DateTime deliveryDate = DateTime.Now;
            Guid kitId = Guid.NewGuid();
            DNAKit kit = new(kitId, 98.99);

            Assert.Throws<ArgumentException>(() =>
            {
                OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            });
        }

        [Test]
        public void PlaceOrderWithInvalidDeliveryDatePast()
        {
            int customerId = 1;
            int desiredAmount = 10;
            Guid kitId = Guid.NewGuid();
            DateTime deliveryDate = DateTime.Now.AddDays(-1);
            DNAKit kit = new(kitId, 98.99);

            Assert.Throws<ArgumentException>(() =>
            {
                OrderPlacementService.PlaceOrder(customerId, desiredAmount, deliveryDate, kit);
            });
        }

        [Test]
        public void GetCustomerOrders()
        {

            int customerId1 = 1;
            int customerId2 = 2;
            int desiredAmount = 10;
            DateTime deliveryDate = DateTime.Now.AddDays(7);

            Guid kitId1 = Guid.NewGuid();
            Guid kitId2 = Guid.NewGuid();

            DNAKit kit1 = new(kitId1, 98.99);
            DNAKit kit2 = new(kitId2, 98.99);

            OrderPlacementService.PlaceOrder(customerId1, desiredAmount, deliveryDate, kit1);

            OrderPlacementService.PlaceOrder(customerId2, desiredAmount, deliveryDate, kit2);

            List<Order> result = OrderPlacementService.GetCustomerOrders();

            CollectionAssert.AreEqual(Orders, result);
        }

        [Test]
        public void CalculateDiscountWithLessThan10ItemsReturnZeroDiscount()
        {
            int desiredAmount = 5;

            double discount = OrderPlacementService.CalculateDiscount(desiredAmount);

            Assert.AreEqual(0, discount);
        }

        [Test]
        public void CalculateDiscountWith10ItemsReturn5PercentDiscount()
        {
            int desiredAmount = 10;

            double discount = OrderPlacementService.CalculateDiscount(desiredAmount);

            Assert.AreEqual(0.05, discount);
        }

        [Test]
        public void CalculateDiscountWith50ItemsReturn15PercentDiscount()
        {
            int desiredAmount = 50;

            double discount = OrderPlacementService.CalculateDiscount(desiredAmount);

            Assert.AreEqual(0.15, discount);
        }

        [Test]
        public void CalculateDiscountWith100ItemsReturn15PercentDiscount()
        {
            int desiredAmount = 100;

            double discount = OrderPlacementService.CalculateDiscount(desiredAmount);

            Assert.AreEqual(0.15, discount);
        }

        [Test]
        public void CalculateDiscountWithLessThan1ItemReturnZeroDiscount()
        {
            int desiredAmount = -5;

            double discount = OrderPlacementService.CalculateDiscount(desiredAmount);

            Assert.AreEqual(0, discount);
        }
    }
}
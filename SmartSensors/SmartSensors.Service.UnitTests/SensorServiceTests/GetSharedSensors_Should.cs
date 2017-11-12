﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SmartSensors.Data;
using SmartSensors.Data.Models;
using SmartSensors.Data.Models.Sensors;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSensors.Service.UnitTests.SensorServiceTests
{
    [TestClass]
    public class GetSharedSensors_Should
    {
        [TestMethod]
        public void ReturnAllSharedSensors_OfTheCurrentlyLoggedUser()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            string userId = "userId";
            string username = "username";
            string sensorName = "sensorName";
            string sensorDescription = "sensorDescription";
            
            var sensors = new List<Sensor>()
            {
                new Sensor() {Name = sensorName, Description = sensorDescription},
                new Sensor() {Name = sensorName, Description = sensorDescription}
            };

            var users = new List<User>()
            {
                new User() { UserName = username, Id = userId, SharedSensors = new List<Sensor>(){ sensors[0] } }
            };

            var usersSetMock = new Mock<DbSet<User>>().SetupData(users);
            var sensorsSetMock = new Mock<DbSet<Sensor>>().SetupData(sensors);

            dbContextMock.SetupGet(x => x.Users).Returns(usersSetMock.Object);
            dbContextMock.SetupGet(x => x.Sensors).Returns(sensorsSetMock.Object);

            var sensorService = new SensorService(dbContextMock.Object);

            //Act
            var sharedSensorList = sensorService.GetSharedSensors(username);

            //Assert
            var sharedSensor = sharedSensorList.Single();
            Assert.AreEqual(sharedSensor.SensorName, sensors[0].Name);
        }
    }
}
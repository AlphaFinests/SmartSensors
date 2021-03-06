﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartSensors.Service.Contracts;
using Moq;
using SmartSensors.Data;
using SmartSensors.Controllers;
using TestStack.FluentMVCTesting;
using SmartSensors.Service.ViewModels;
using SmartSensors.Data.Models;
using System.Linq;
using System.Data.Entity;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using SmartSensors.Data.Models.Sensors;
using SmartSensors.Tests.Helpers;

namespace SmartSensors.Tests.Controllers.SensorControllerTests
{
    [TestClass]
    public class MySensors_Should
    {
        [TestMethod]
        public void ReturnTheCorrectViewModel()
        {
            //Arrange
            var sensorServiceMock = new Mock<ISensorService>();
            var urlProviderMock = new Mock<IUrlProvider>();
            
            var sensors = new List<FullSensorViewModel>()
            {
                new FullSensorViewModel(){ Name = "Sensor1" },
                new FullSensorViewModel(){ Name = "Sensor2" }
            };

            var user = new User() { UserName = "FirstUser"};

            sensorServiceMock.Setup(s => s.GetMySensors(user.UserName)).Returns(sensors);

            var controller = new SensorController(urlProviderMock.Object, sensorServiceMock.Object);

            controller.UserMocking(user.UserName);

            //Act & Assert
            controller
                .WithCallTo(c => c.MySensors())
                .ShouldRenderDefaultView()
                .WithModel<List<FullSensorViewModel>>
            (viewModel =>
            {
                for (int i = 0; i < viewModel.Count; i++)
                {
                    Assert.AreEqual(viewModel[i].Name, sensors[i].Name);
                }
            });
        }
    }
}

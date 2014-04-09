﻿using System;
using System.Net;
using System.Linq;
using JustGiving.Api.Data.Sdk.Model.CustomCodes;
using JustGiving.Api.Data.Sdk.Test.Integration.TestExtensions;
using JustGiving.Api.Sdk;
using NUnit.Framework;

namespace JustGiving.Api.Data.Sdk.Test.Integration.ApiClients
{
    [TestFixture, Category("Slow")]
    public class UpdateEventCustomCodesTests : ApiTestFixture
    {
        [Test]
        public void CanSetCustomCode()
        {

            var clientConfiguration = XmlDataClientConfiguration();
            
            var client = new JustGivingDataClient(clientConfiguration);
            var response = client.CustomCodes.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = "foo" });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private static DataClientConfiguration XmlDataClientConfiguration()
        {
            var clientConfiguration = GetDefaultDataClientConfiguration()
                .With((clientConfig) => clientConfig.WireDataFormat = WireDataFormat.Xml);
            return clientConfiguration;
        }

        [Test]
        public void CanGetCustomCode()
        {
            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            var val = Guid.NewGuid().ToString().Substring(0, 5);
            client.CustomCodes.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = val });
            var response = client.CustomCodes.GetEventCustomCodes(TestContext.KnownEventId);
            Assert.That(response.CustomCode1, Is.EqualTo(val));
        }

        [Test]
        public void CanSetMultipleCustomCodes()
        {
            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            var response = client.CustomCodes.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "foo" }, new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CanSetMultipleCustomCodesWithCsvData()
        {
            var csvString = string.Format("EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value2,value3",
                    TestContext.KnownEventId, TestContext.KnownEventId + 1);

            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            var response = client.CustomCodes.SetEventCustomCodes(csvString);
            Assert.That(response.Count(r => r.Status == 200), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Single()
        {
            var clientConfiguration = XmlDataClientConfiguration();

            var client = new JustGivingDataClient(clientConfiguration);
            var response = client.CustomCodes.SetEventCustomCodes(TestContext.KnownEventId, new EventCustomCodes { CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void CustomCodesAreValidated_Multiple()
        {
            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);

            var response = client.CustomCodes.SetEventCustomCodes(new[] { new EventCustomCodesListItem { EventId = TestContext.KnownEventId, CustomCode1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" }, 
                new EventCustomCodesListItem { EventId = TestContext.KnownEventId + 1, CustomCode1 = "bar" } });
            
            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }

        [Test]
        public void CustomCodesAreValidated_Csv()
        {
            // Arrange

            var csvString =
                string.Format("EventId,CustomCode1,CustomCode2,CustomCode3\r\n{0},value1,value2,value3\r\n{1},value1,value222222222222222222222222222222222222222,value3",TestContext.KnownEventId, TestContext.KnownEventId + 1);

            var clientConfiguration = XmlDataClientConfiguration();
            var client = new JustGivingDataClient(clientConfiguration);
            
            var response = client.CustomCodes.SetEventCustomCodes(csvString);

            Assert.That(response.Count(r => r.Status == (int)HttpStatusCode.BadRequest), Is.GreaterThanOrEqualTo(1));
        }
    }
}

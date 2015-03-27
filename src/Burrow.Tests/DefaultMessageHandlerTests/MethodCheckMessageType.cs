﻿using System;
using Burrow.Internal;
using Burrow.Tests.Extras.RabbitSetupTests;
using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client;

// ReSharper disable InconsistentNaming
namespace Burrow.Tests.DefaultMessageHandlerTests
{
    [TestFixture]
    public class MethodCheckMessageType
    {
        [Test, ExpectedException(typeof(Exception))]
        public void Should_throw_exeception_if_Property_Type_is_not_equal_to_deserialized_message_type_string()
        {
            // Arrange
            Global.DefaultTypeNameSerializer = new TypeNameSerializer();
            var property = Substitute.For<IBasicProperties>();
            var errorHanlder = Substitute.For<IConsumerErrorHandler>();
            var handler = new DefaultMessageHandlerForTest<Customer>("SubscriptionName", Substitute.For<Action<Customer, MessageDeliverEventArgs>>(), errorHanlder, Substitute.For<ISerializer>(), Substitute.For<IRabbitWatcher>());

            // Action
            handler.PublicCheckMessageType(property);
        }
    }
}
// ReSharper restore InconsistentNaming
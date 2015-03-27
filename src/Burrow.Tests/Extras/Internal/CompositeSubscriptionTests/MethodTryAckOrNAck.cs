﻿using Burrow.Extras;
using Burrow.Extras.Internal;
using NSubstitute;
using NUnit.Framework;
using RabbitMQ.Client;

// ReSharper disable InconsistentNaming
namespace Burrow.Tests.Extras.Internal.CompositeSubscriptionTests
{
    [TestFixture]
    public class MethodTryAckOrNAck
    {
        [Test, ExpectedException(typeof(SubscriptionNotFoundException))]
        public void Should_throw_SubscriptionNotFoundException_if_subscription_not_found()
        {
            // Arrange
            var channel = Substitute.For<IModel>();
            channel.IsOpen.Returns(true);
            var subs = new CompositeSubscription();
            var subscription = new Subscription
            {
                ConsumerTag = "ConsumerTag",
                QueueName = "QueueName",
                SubscriptionName = "SubscriptionName"
            };
            subscription.SetChannel(channel);
            subs.AddSubscription(subscription);

            // Action
            subs.NackAllOutstandingMessages("ConsumerTagNotFound", true);
        }
    }
}
// ReSharper restore InconsistentNaming
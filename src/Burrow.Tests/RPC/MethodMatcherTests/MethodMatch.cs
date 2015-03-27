﻿using System.Reflection;
using System.Reflection.Emit;
using Burrow.RPC;
using NUnit.Framework;


// ReSharper disable InconsistentNaming
namespace Burrow.Tests.RPC.MethodMatcherTests
{
    [TestFixture]
    public class MethodMatch
    {
        [Test]
        public void Should_return_method_info_if_match()
        {
            // Arrange
            var matcher = new MethodMatcher();
            var request = new RpcRequest
            {
                DeclaringType = typeof(ISomeService).FullName,
                MethodName = "TryParse",
                MemberType = MemberTypes.Method,
                MethodSignature = matcher.GetMethodSignature(typeof(ISomeService).GetMethod("TryParse"))
            };

            // Action
            var method = matcher.Match<ISomeService>(request);

            // Assert
            Assert.IsNotNull(method);
        }

        [Test]
        public void Should_return_null_if_not_match_anything()
        {
            // Arrange
            var matcher = new MethodMatcher();
            var request = new RpcRequest
            {
                DeclaringType = typeof(ISomeService).FullName,
                MethodName = "MethodNotFound",
                MemberType = MemberTypes.Method,
                MethodSignature = matcher.GetMethodSignature(new DynamicMethod("MethodNotFound", typeof(void), null, true))
            };

            // Action
            var method = matcher.Match<ISomeService>(request);

            // Assert
            Assert.IsNull(method);
        }
    }
}
// ReSharper restore InconsistentNaming
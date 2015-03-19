using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using NoMatterWebTests.Stubs;
using PrettyDamnThriftyWeb.Controllers;
using Rhino.Mocks;
using SharedLibrary.Models;
using SharedLibrary.ViewModels;

namespace NoMatterWebTests
{
	[TestFixture]
	class ShoppingCartControllerTests
	{
		private ICurrentUser _currentUserMock;

		[SetUp]
		public void Setup()
		{
			_currentUserMock = MockRepository.GenerateStrictMock<ICurrentUser>();
			_currentUserMock.Stub(s => s.UserId()).Return(0);
			_currentUserMock.Stub(s => s.CartId()).Return(Guid.NewGuid().ToString());
			_currentUserMock.Stub(s => s.ClearCartSession());
		}

		[Test]
		public void GetCartSmokeTest()
		{
			//arrange
			var controller = new ShoppingCartController(_currentUserMock);

			// Act
			var result = controller.GetCart() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual("Cart", result.ViewName);
		
		}

		[Test]
		public void AddItemToCartSmokeTest()
		{
			//arrange
			var controller = new ShoppingCartController(_currentUserMock);

			// Add an item to the cart that does not exist
			var result = controller.AddItemToCart(999999, 1);

			// Assert
			Assert.AreEqual("False", result);

		}

		[Test]
		public void RemoveItemFromCartSmokeTest()
		{
			//arrange
			var controller = new ShoppingCartController(_currentUserMock);

			// Add an item to the cart that does not exist
			JsonResult result = controller.RemoveItemFromCart(999999);

			var shoppingCartRemoveVm = (ShoppingCartRemoveVm)result.Data;

			Assert.AreEqual(0, shoppingCartRemoveVm.ItemCount);

			// Assert
			//Assert.AreEqual("False", result.);

		}
	}
}

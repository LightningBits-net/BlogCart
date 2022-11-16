using System;
using ECommerce_Client.ViewModels;

namespace ECommerce_Client.Service
{
	public interface ICartService
	{
		Task DecrementCart(ShoppingCart shoppingCart);

		Task IncrementCart(ShoppingCart shoppingCart);
	}
}


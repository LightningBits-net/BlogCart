using System;
using ECommerce_Client.ViewModels;

namespace ECommerce_Client.Service
{
	public interface ICartService
	{
		public event Action OnChange;

		Task DecrementCart(ShoppingCart shoppingCart);

		Task IncrementCart(ShoppingCart shoppingCart);
	}
}


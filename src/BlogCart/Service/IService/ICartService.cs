using System;
using BlogCart.ViewModels;

namespace BlogCart.Service
{
	public interface ICartService
	{
		public event Action OnChange;

		Task DecrementCart(ShoppingCart shoppingCart);

		Task IncrementCart(ShoppingCart shoppingCart);
	}
}


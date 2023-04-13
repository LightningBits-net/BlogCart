﻿using System;
using Blazored.LocalStorage;
using BlogCart.ViewModels;
using SharedServices.Commons;

namespace BlogCart.Service
{
	public class CartService : ICartService
	{
        private readonly ILocalStorageService _localStorage;

        public event Action OnChange;

        public CartService(ILocalStorageService localStorageService)
		{
            _localStorage = localStorageService;
		}

      

        public async Task DecrementCart(ShoppingCart cartToDecrement)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);

            for(int i=0; i<cart.Count; i++) 
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count==1 || cart[i].Count==0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            //OnChange.Invoke();
        }

        public async Task IncrementCart(ShoppingCart cartToAdd)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
            bool itemInCart = false;

            if(cart == null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach(var obj in cart)
            {
                if(obj.ProductId==cartToAdd.ProductId && obj.ProductPriceId==cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    obj.Count += cartToAdd.Count;
                }
            }
            if(!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            //OnChange.Invoke();
        }
    }
}


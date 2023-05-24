using EfrashBatek.Models;
using System.Collections.Generic;

namespace EfrashBatek.service
{
    public interface ICartRepository
    {
        void Create(Cart brand);
        int Delete(int Id);
        List<Cart> GetAll();
        Cart GetById(int Id);
        int Update(int id, Cart cart);
    }
}
using ContosoPizza.Models;
using ContosoPizza.Data;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services;

public class PizzaService
{
    private readonly PizzaContext _context;
    public PizzaService(PizzaContext context)
    {
        _context = context;
    }

    public IEnumerable<Pizza> GetAll()
    {
        return _context.Pizzas
            .AsNoTracking()
            .ToList();
    }

    public Pizza? GetById(int id)
{
    return _context.Pizzas
        .Include(p => p.Toppings)
        .Include(p => p.Sauce)
        .AsNoTracking()
        .SingleOrDefault(p => p.Id == id);
}

   public Pizza Create(Pizza newPizza)
{
    _context.Pizzas.Add(newPizza);
    _context.SaveChanges();

    return newPizza;
}

    public void AddTopping(int pizzaId, int toppingId)
{
    var pizzaToUpdate = _context.Pizzas.Find(pizzaId);
    var toppingToAdd = _context.Toppings.Find(toppingId);

    if (pizzaToUpdate is null || toppingToAdd is null)
    {
        throw new InvalidOperationException("Pizza or topping does not exist");
    }

    if(pizzaToUpdate.Toppings is null)
    {
        pizzaToUpdate.Toppings = new List<Topping>();
    }

    pizzaToUpdate.Toppings.Add(toppingToAdd);

    _context.SaveChanges();
}

    public void UpdateSauce(int PizzaId, int SauceId)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }
}
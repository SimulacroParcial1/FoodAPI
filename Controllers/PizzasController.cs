using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodAPI.Data;
using FoodAPI.Models;

namespace FoodAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PizzasController : ControllerBase
{
    private readonly AppDbContext _context;

    public PizzasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/pizzas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
    {
        return await _context.Pizzas.AsNoTracking().ToListAsync();
    }

    // GET: api/pizzas/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pizza>> GetPizza(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza == null) return NotFound();
        return pizza;
    }

    // POST: api/pizzas
    [HttpPost]
    public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);
        _context.Pizzas.Add(pizza);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPizza), new { id = pizza.Id }, pizza);
    }

    // PUT: api/pizzas/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutPizza(int id, Pizza pizza)
    {
        if (id != pizza.Id) return BadRequest();

        _context.Entry(pizza).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PizzaExists(id)) return NotFound();
            throw;
        }

        return NoContent();
    }

    // DELETE: api/pizzas/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePizza(int id)
    {
        var pizza = await _context.Pizzas.FindAsync(id);
        if (pizza == null) return NotFound();

        _context.Pizzas.Remove(pizza);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool PizzaExists(int id) => _context.Pizzas.Any(p => p.Id == id);
}

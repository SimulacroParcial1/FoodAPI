using Microsoft.EntityFrameworkCore;
using FoodAPI.Models;

namespace FoodAPI.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Pizza> Pizzas { get; set; } = null!;
}
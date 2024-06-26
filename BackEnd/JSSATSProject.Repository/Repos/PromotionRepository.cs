﻿using JSSATSProject.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace JSSATSProject.Repository.Repos;

public class PromotionRepository : GenericRepository<Promotion>
{
    public PromotionRepository(DBContext context) : base(context)
    {
    }

    public async Task<Promotion> GetPromotionByCategoryAsync(int productCategoryId)
    {
        var promotions = await context.Promotions
            .Include(p => p.Categories)
            .Where(p => p.Categories.Any(c => c.Id == productCategoryId) && p.Status.Equals("active"))
            .OrderByDescending(p => p.DiscountRate)
            .ToListAsync();

        return promotions.FirstOrDefault();
    }
}
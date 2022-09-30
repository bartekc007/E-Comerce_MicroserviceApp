using Discount.API.Entities;

namespace Discount.API.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon entity);
    Task<bool> UpdateDiscount(Coupon entity);
    Task<bool> DeleteDiscount(string productName);
}
using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon entity);
    Task<bool> UpdateDiscount(Coupon entity);
    Task<bool> DeleteDiscount(string productName);
}
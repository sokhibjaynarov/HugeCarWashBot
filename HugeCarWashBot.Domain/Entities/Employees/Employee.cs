using HugeCarWashBot.Domain.Commons;
using HugeCarWashBot.Domain.Entities.Orders;
using HugeCarWashBot.Domain.Enums;

namespace HugeCarWashBot.Domain.Entities.Employees
{
    public class Employee : IAuditable
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string? TelegramId { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public ItemState State { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public Employee()
        {
            Orders = new List<Order>();
        }

        public void Update()
        {
            //UpdatedAt = DateTime.Now;
            State = ItemState.Updated;
        }

        public void Create()
        {
            //CreatedAt = DateTime.Now;
            State = ItemState.Created;
        }

        public void Delete()
        {
            State = ItemState.Deleted;
        }
    }
}

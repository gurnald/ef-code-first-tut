
using ef_code_first_tut;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

var _context = new SalesDbContext();

var order = _context.Orders
                                     .Include(x => x.OrderLines)
                                          .ThenInclude(x => x.Item)
                                     .Include(x => x.Customer)
                                     .Single(x => x.Id == 1);

Console.WriteLine($"ORDER: DESCRIPTION: {order.Description}");
foreach(var ol in order.OrderLines) { 
    Console.WriteLine($"ORDERLINE: PRODUCT: {ol.Item.Name}, QUANTITY: {ol.Quantity}, " +
                                    $"PRICE: {ol.Item.Price:C}, LINE TOTAL: {ol.Quantity * ol.Item.Price:C}");
}
var orderTotal = order.OrderLines.Sum(ol => ol.Item.Price * ol.Quantity);
Console.WriteLine($"TOTAL: {orderTotal:C}");


//_context.Customers.ToList().ForEach(c => Console.WriteLine(c.Name));
using Microsoft.AspNetCore.Mvc;
using Project2.Data.Enum;
using Project2.Data;
using Project2.Models;

namespace Project2.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : Controller
        {
            private readonly AppDbContext _dbContext;

            public OrderController(AppDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            [HttpGet("getorders")]
            public IActionResult GetOrders()
            {
                try
                {
                    var orders = _dbContext.Orders.ToList();
                    return Ok(orders);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء جلب طلبات الشراء.");
                }
            }

            [HttpPost("addorder")]
            public IActionResult AddOrder(int postId, int customerId, int deliverId, PayWay payWay)
            {
                try
                {
                    // إنشاء طلب شراء جديد
                    var order = new Order
                    {
                        postId = postId,
                        customerId = customerId,
                        deliverId = deliverId,
                        payWay = payWay,
                        Status = false,
                        CreatedDate = DateTime.Now
                    };

                    // إضافة الطلب إلى قاعدة البيانات
                    _dbContext.Orders.Add(order);
                    _dbContext.SaveChanges();

                    return Ok("تم إضافة الطلب بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء إضافة الطلب.");
                }
            }

            [HttpPut("updateorder/{id}")]
            public IActionResult UpdateOrder(int id, [FromBody] Order updatedOrder)
            {
                try
                {
                    var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);

                    if (order == null)
                    {
                        return NotFound("الطلب غير موجود.");
                    }

                    // تحديث بيانات الطلب
                    order.postId = updatedOrder.postId;
                    order.customerId = updatedOrder.customerId;
                    order.deliverId = updatedOrder.deliverId;
                    order.payWay = updatedOrder.payWay;
                    order.Status = updatedOrder.Status;

                    _dbContext.SaveChanges();

                    return Ok("تم تحديث الطلب بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء تحديث الطلب.");
                }
            }

            [HttpDelete("deletorder/{id}")]
            public IActionResult DeleteOrder(int id)
            {
                try
                {
                    var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);

                    if (order == null)
                    {
                        return NotFound("الطلب غير موجود.");
                    }

                    _dbContext.Orders.Remove(order);
                    _dbContext.SaveChanges();

                    return Ok("تم حذف الطلب بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء حذف الطلب.");
                }
            }

            [HttpGet("customerorders")]
            public IActionResult GetCustomerOrders()
            {
                try
                {
                    var custOrders = _dbContext.CustOrders.ToList();
                    return Ok(custOrders);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء جلب طلبات الزبون.");
                }
            }

            [HttpPost("addcustomerorders/{customerId,orderId}")]
            public IActionResult AddCustOrder(int customerId, int orderId)
            {
                try
                {
                    var custOrder = new CustOrder
                    {
                        CustomerId = customerId,
                        OrderId = orderId,
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.CustOrders.Add(custOrder);
                    _dbContext.SaveChanges();

                    return Ok("تم إضافة الطلب للزبون بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء إضافة الطلب للزبون.");
                }
            }

            [HttpDelete("deletecustomerorders/{id}")]
            public IActionResult DeleteCustOrder(int id)
            {
                try
                {
                    var custOrder = _dbContext.CustOrders.FirstOrDefault(co => co.Id == id);

                    if (custOrder == null)
                    {
                        return NotFound("الطلب غير موجود.");
                    }

                    _dbContext.CustOrders.Remove(custOrder);
                    _dbContext.SaveChanges();

                    return Ok("تم حذف الطلب للزبون بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء حذف الطلب للزبون.");
                }
            }

            [HttpGet("deliveryorders")]
            public IActionResult GetDeliveryOrders()
            {
                try
                {
                    var delivOrders = _dbContext.DelivOrders.ToList();
                    return Ok(delivOrders);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء جلب طلبات التوصيل.");
                }
            }

            [HttpPost("adddeliverorders/{deliverId,orderId}")]
            public IActionResult AddDelivOrder(int deliverId, int orderId)
            {
                try
                {
                    var delivOrder = new DelivOrder
                    {
                        DeliverId = deliverId,
                        OrderId = orderId,
                        CreatedDate = DateTime.Now
                    };

                    _dbContext.DelivOrders.Add(delivOrder);
                    _dbContext.SaveChanges();

                    return Ok("تمت إضافة الطلب لشركة التوصيل بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء إضافة الطلب لشركة التوصيل.");
                }
            }

            [HttpDelete("deletedeliverorders/{id}")]
            public IActionResult DeleteDelivOrder(int id)
            {
                try
                {
                var delivOrder = _dbContext.DelivOrders.FirstOrDefault(d => d.Id == id );
                    if (delivOrder == null)
                    {
                        return NotFound("الطلب غير موجود.");
                    }

                    _dbContext.DelivOrders.Remove(delivOrder);
                    _dbContext.SaveChanges();

                    return Ok("تم حذف الطلب لشركة التوصيل بنجاح.");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "حدث خطأ أثناء حذف الطلب لشركة التوصيل.");
                }
            }
        }

}

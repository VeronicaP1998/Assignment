using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace IMS.Controllers
{
    [RoutePrefix("api/Order")]

    public class OrderProcessingController : ApiController
    {

        [HttpPost]
        [Route("OrderCreation")]
        public IHttpActionResult OrderCreation([FromBody] Order orders)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    tblOrder tblOrder = new tblOrder();
                    tblOrder.UserID = orders.UserID;
                    tblOrder.Total_Price = orders.Total_Price;
                    tblOrder.Created_At = orders.Created_At;
                    db.tblOrders.Add(tblOrder);
                    db.SaveChanges();
                    return Ok("Order Created Successfully");
                }
                    
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("OrderItemCreation")]
        public IHttpActionResult OrderItemCreation([FromBody]List<OrderItem> orders)
        {
            try
            {
                using (TestEntities db = new TestEntities())

                {
                    if (orders.Count() > 0)
                    {
                        decimal totalval = 0;
                        int orderID = 0;
                        foreach (var item in orders)
                        {
                            orderID = item.OrderID;
                            if (db.tblOrder_Items.Where(i => i.OrderID == item.OrderID && i.ProductID == item.ProductID).Count() > 0)
                            {

                                return Ok("ProductID" + " " + item.ProductID + " " + "Already Exists");
                            }
                            tblOrder_Items orderItem = new tblOrder_Items();
                            orderItem.OrderID = item.OrderID;
                            orderItem.ProductID = item.ProductID;
                            orderItem.Quantity = item.Quantity;
                            orderItem.Price = item.Price;
                            totalval = totalval + (item.Quantity.Value * item.Price.Value);
                            db.tblOrder_Items.Add(orderItem);
                            db.SaveChanges();

                        }

                        var order = db.tblOrders.Where(i => i.ID == orderID).FirstOrDefault();
                        order.Total_Price = totalval;
                        db.SaveChanges();


                        return Ok("Item Added");
                    }
                    else
                    {
                        return Ok(HttpStatusCode.NoContent);
                    }

                }
                
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        [Route("GetOrderByID/{ID}")]
        public IHttpActionResult GetOrderByID(int ID)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {
                    var result = (from i in db.tblOrders.AsNoTracking()
                                  where i.ID == ID
                                  select new Order
                                  {
                                      ID = i.ID,
                                      Total_Price = i.Total_Price,
                                  }).FirstOrDefault();

                    return Ok(result);
                }
                
            }

            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}
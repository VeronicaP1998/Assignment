using IMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace IMS.Controllers
{
    [RoutePrefix("api/Product")]

    public class ProductController : ApiController
    {

        TestEntities db = new TestEntities();
        [HttpGet]
        [Route("GetAllProducts")]
        public IHttpActionResult GetAllProducts()
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    var result = (from i in db.tblProducts.AsNoTracking()
                                  select new Product
                                  {
                                      ID = i.ID,
                                      Name = i.Name,
                                      Quantity = i.Quantity.Value,
                                      Price = i.Price.Value,
                                      total_inventory_value = (i.Quantity * i.Price),
                                  }).ToList();
                    Inventory inventory = new Inventory();

                    inventory.total_inventory_value = result.Sum(i => i.total_inventory_value);

                    return Ok(inventory);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public IHttpActionResult AddProduct([FromBody] Product product)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    tblProduct tblProduct = new tblProduct();
                    tblProduct.Name = product.Name;
                    tblProduct.Quantity = product.Quantity;
                    tblProduct.Price = product.Price;
                    db.tblProducts.Add(tblProduct);
                    db.SaveChanges();
                    return Ok("Product Added Successfully");
                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("UpdateProductQTY")]
        public IHttpActionResult UpdateProductQTY([FromBody] Product product)
        {
            try
            {
                using (TestEntities db = new TestEntities())
                {


                    tblProduct tblProduct = new tblProduct();
                    tblProduct = db.tblProducts.Where(i => i.ID == product.ID).FirstOrDefault();
                    if (tblProduct != null)
                    {
                        tblProduct.Quantity = product.Quantity;
                        db.SaveChanges();
                        return Ok("Updated Successfully");
                    }
                    else
                    {
                        return Ok(HttpStatusCode.NoContent);
                    }

                }
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ContactList.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class contactItem
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mail { get; set; }
    }

    public partial class ContactItemController : ControllerBase
    {
        private static readonly List<contactItem> items =
            new List<contactItem> {
                new contactItem { id=140024, firstName="Max", lastName="Huber", mail="m.huber@gmail.com" },
                new contactItem { id=140067, firstName="Michael", lastName="Bauer", mail="m.bauer@gmail.com" },
                new contactItem { id=140089, firstName="Alexandra", lastName="Leiter", mail="a.leiter@gmail.com" }
            };

        //get al persons from the list
        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok("Hello");
        }

        //Get specific Person by ID
        [HttpGet]
        [Route("{index}", Name = "GetSpecificItem")]
        public IActionResult GetItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                return Ok(items[index]);
            }

            return BadRequest("Invalid index");
        }

        //Get specific Person by name
        [HttpGet]
        [Route("findByName", Name = "nameFilter")]
        public IActionResult GetItem(string nameFilter)
        {
            for(int i = 0; i <= items.Count; i++)
            {
                if ( nameFilter.Equals(items[i].firstName) || nameFilter.Equals(items[i].lastName) ){
                    return Ok(items[i]);
                }
            }

            return BadRequest("Invalid index");
        }

        //Add a new person to the list
        [HttpPost]
        public IActionResult AddItem([FromBody] contactItem newItem)
        {
            items.Add(newItem);
            return CreatedAtRoute("GetSpecificItem", new { index = items.IndexOf(newItem) }, newItem);
        }

        //Delete a person
        [HttpDelete]
        [Route("{index}")]
        public IActionResult DeleteItem(int index)
        {
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                return NoContent();
            }

            return BadRequest("Invalid index");
        }



    }
}

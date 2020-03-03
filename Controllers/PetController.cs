using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tamagotchi.Models;

namespace Tamagotchi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PetController : ControllerBase
  {
    public DatabaseContext db { get; set; } = new DatabaseContext();

    [HttpGet]
    public List<Pet> GetAllPets()
    {
      var pets = db.Pets.OrderBy(p => p.Name);
      return pets.ToList();
    }
    [HttpGet("{id}")]
    public Pet GetOnePet(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      return item;
    }
    [HttpPost]
    public Pet CreateAPet(Pet item)
    {
      db.Pets.Add(item);
      db.SaveChanges();
      return item;
    }
    [HttpPut("{id}/play")]
    public Pet Play(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel += 5;
      item.HungerLevel += 3;
      db.SaveChanges();
      return item;
    }
    [HttpPut("{id}/feed")]
    public Pet Feed(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HungerLevel += 5;
      item.HappinessLevel += 3;
      db.SaveChanges();
      return item;
    }
    [HttpPut("{id}/scold")]
    public Pet Scold(int id)
    {
      var item = db.Pets.FirstOrDefault(i => i.Id == id);
      item.HappinessLevel -= 5;
      db.SaveChanges();
      return item;
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteOne(int id)
    {
      var item = db.Pets.FirstOrDefault(f => f.Id == id);
      if (item == null)
      {
        return NotFound();
      }
      db.Pets.Remove(item);
      db.SaveChanges();
      return Ok();
    }
  }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Base;
using WebApplication3.Data.ViewModels;
using WebApplication3.Models;

namespace WebApplication3.Data.Services
{
    public class PartiesService:EntityBaseRepository<Party>,IPartiesService
    {
        private readonly AppDbContext _context;
        public PartiesService(AppDbContext context):base(context)
        {
            _context = context;
        }

        // _context.Parties.Where(n => n.RoomId == data.RoomId).Select(n => n.PartyRoom.Price).Sum();

        public async Task AddNewPartyAsync(NewPartyVM data)
        {
            //var db = new YourDbContext();
            // var orders = db.Orders.Include(o => o.OrderItems).ToList();

            // Pobierz informacje o wartościach z powiązanej tabeli
            //foreach (var order in orders)
            // {
            //foreach (var item in order.OrderItems)
            //{
            //  decimal price = item.Price;
            // Wykonaj operację na cenie
            // }
            //}
            // kod tworzy nowy obiekt typu Movie i dodaje go do bazy danych. Następnie wykonuje asynchroniczne zapisywanie zmian w bazie danych.
            var newParty = new Party()
            {
                Name = data.Name,
                Guests = data.Guests,
                Description = data.Description,
                Picture = data.Picture,
                RoomId = data.RoomId,
                ThemeId = data.ThemeId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                AdditionalDecorationsCost= (double)data.AdditionalCost,
                OrgId = data.OrgId
               
             };
            await _context.Parties.AddAsync(newParty);
            await _context.SaveChangesAsync();

            //Add Movie Parties
            foreach (var featureId in data.FeatureIds)
            {
                var newPartyFeature = new Party_Feature()
                {
                    PartyId = newParty.Id,
                    FeatureId = featureId
                };
                await _context.Party_Feature.AddAsync(newPartyFeature);
            }
            await _context.SaveChangesAsync();

            var allParties = await GetAllAsync(n => n.PartyRoom, m => m.PartyTheme, o => o.PartyOrganizator, p => p.Party_Feature);
            var dbParty = await GetPartyByIdAsync(newParty.Id);
            
            var orders = _context.Parties
            .Include(o => o.PartyRoom)
            .Include(o => o.PartyTheme)
            .Include(pf => pf.Party_Feature).ThenInclude(f => f.Feature)
            .ToList();

            List<Party_Feature> featureid = dbParty.Party_Feature.Where(pf => pf.PartyId == newParty.Id).ToList();
            var chosenFeatures = featureid.Select(i => i.Feature).ToList();
            var priceOnly = chosenFeatures.Select(i => i.Price).ToArray();
            var totalFeatureCost = CalculateCost(priceOnly);


            if (orders != null)
            {

                dbParty.Price = totalFeatureCost + dbParty.AdditionalDecorationsCost + dbParty.PartyRoom.Price + dbParty.Guests * dbParty.PartyTheme.Price;
                await _context.SaveChangesAsync();
            }

            double CalculateCost (double[] prices)
            {
                double sum = prices.Sum();
                return sum;
            }


            // _context.Party_Feature.RemoveRange(existingPartiesDb);
            // await _context.SaveChangesAsync();

            //var partyDetail = await GetPartyByIdAsync(newParty.Id);

           // var orders = _context.Parties
            //    .Include(o => o.PartyRoom)
             //   .Include(o => o.PartyTheme)
              //  .ToList();

        
            // Pobierz informacje o wartościach z powiązanej tabeli
           // foreach (var order in orders)
           // {
           //     
             //  double price = (double)order.PartyRoom.Price;
              // double price2 = (double)order.PartyTheme.Price;
             //   double totalPrice = price + price2;
            //    var newPartyPrice = new Party()
               // {
                //    Id = newParty.Id,
                //    Price = totalPrice
                    
                
           // }; await _context.Parties.AddAsync(newPartyPrice);
               
           // };
           // await _context.SaveChangesAsync();


            // Oblicz sumę cen




            //var existingPartiesDb = _context.Party_Feature.Where(n => n.PartyId == data.Id).ToList();
            //  _context.Party_Feature.RemoveRange(existingPartiesDb);
            //await _context.SaveChangesAsync();

            
        }


        
        public async Task<NewPartyDropdownsVM> GetNewPartyDropdownsValues()
        {
            var response = new NewPartyDropdownsVM() {
            Features = await _context.Features.OrderBy(n => n.Name).ToListAsync(),
            Organizators = await _context.Organizators.OrderBy(n => n.FirstName).ToListAsync(),
            Themes = await _context.Themes.OrderBy(n => n.ThemeName).ToListAsync(),
            Rooms = await _context.Rooms.OrderBy(n => n.Name).ToListAsync()
        };
            return response;
        }

        public async Task<Party> GetPartyByIdAsync(int id)
        {
            var partyDetails =await _context.Parties
                .Include(r => r.PartyRoom)
                .Include(o => o.PartyOrganizator)
                .Include(t => t.PartyTheme)
                .Include(pf => pf.Party_Feature).ThenInclude(f => f.Feature)
                .FirstOrDefaultAsync(n => n.Id == id);
            return partyDetails;
        }

        public async Task UpdatePartyAsync(NewPartyVM data)
        {
            var dbParty = await _context.Parties.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbParty != null)
            {

                dbParty.Name = data.Name;
                dbParty.Guests = data.Guests;
                dbParty.Description = data.Description;
                dbParty.Price = data.Price;
                dbParty.Picture = data.Picture;
                dbParty.RoomId = data.RoomId;
                dbParty.ThemeId = data.ThemeId;
                dbParty.StartDate = data.StartDate;
                dbParty.EndDate = data.EndDate;
                dbParty.AdditionalDecorationsCost = (double)data.AdditionalCost;
                dbParty.OrgId = data.OrgId;
              
                await _context.SaveChangesAsync();
            }


            //Remove existing parties
            var existingPartiesDb = _context.Party_Feature.Where(n => n.PartyId == data.Id).ToList();
            _context.Party_Feature.RemoveRange(existingPartiesDb);
            await _context.SaveChangesAsync();

            //Add Party Feature
            foreach (var featureId in data.FeatureIds)
            {
                var newPartyFeature = new Party_Feature()
                {
                    PartyId = data.Id,
                    FeatureId = featureId
                };
                await _context.Party_Feature.AddAsync(newPartyFeature);
            }
            await _context.SaveChangesAsync();
        }

       // public decimal CalculateCost(NewPartyVM data)
      //  {
            
            // Pobierz cenę wybranej opcji z bazy danych lub innego źródła danych
            
            //var option = _context.Parties.Find(data);
          //  decimal price = (decimal)option.PartyRoom.Price;
           // decimal price2 = (decimal)option.PartyTheme.Price;
            // Oblicz sumę cen
          //  decimal totalPrice = price + price2;
            

            // Przekazujemy cenę do widoku
          //  return totalPrice;
      //  }
    }
}

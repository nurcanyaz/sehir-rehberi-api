﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SehirRehberii.API.Models;

namespace SehirRehberii.API.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }
        public List<City> GetCities()
        {
            
            var cities = _context.Cities.Include(c => c.Photos).ToList();
            return cities;
        }
       


        public City GetCityById(int cityId)
        {
            var city = _context.Cities.Include(c => c.Photos).FirstOrDefault(c => c.Id == cityId);
            return city;
        }

        public Photo GetPhoto(int id)
        {
            var photo = _context.Photos.FirstOrDefault(p => p.Id == id);
            return photo;
        }

        public List<Photo> GetPhotosByCity(int cityId)
        {
            var photos = _context.Photos.Where(p => p.CityId == cityId).ToList();
            return photos;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
        public IEnumerable<Hotel> GetHotelsByCityId(int cityId)
        {
            return _context.Hotels.Where(h => h.CityId == cityId).ToList();
        }

        public Hotel GetHotelById(int hotelId)
        {
            return _context.Hotels.FirstOrDefault(h => h.Id == hotelId);
        }

        public IEnumerable<string> GetHotelPhotos(int hotelId)
        {
            return _context.Photos
                .Where(p => p.HotelId == hotelId)
                .Select(p => p.Url)
                .ToList();
        }

        public List<Hotel> GetAllHotels()
        {
            var hotels = _context.Hotels.Include(h => h.Photos).ToList();
            return hotels;
        }
    }
}

